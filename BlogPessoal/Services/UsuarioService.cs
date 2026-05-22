using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPessoal.Services{

    public class UsuarioService : IUsuarioService{

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration){
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<UsuarioRespostaDTO?> BuscarPorIdAsync(long id){
            Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if(usuario == null){
                return null;
            }

            return ConverterParaResponseDTO(usuario);
        }

        public async Task<UsuarioRespostaDTO> CadastrarAsync(UsuarioDTO usuarioDTO){
            ValidarUsuario(usuarioDTO);

            Usuario? usuarioExistente = await _usuarioRepository.BuscarPorEmailAsync(usuarioDTO.Email);

            if(usuarioExistente != null){
                throw new ArgumentException("Já existe um usuário cadastrado com este email.");
            }

            Usuario usuario = new Usuario(
                usuarioDTO.Nome,
                usuarioDTO.Email,
                usuarioDTO.Senha
            );

            PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
            usuario.Senha = passwordHasher.HashPassword(usuario, usuarioDTO.Senha);

            Usuario usuarioCadastrado = await _usuarioRepository.CadastrarAsync(usuario);

            return ConverterParaResponseDTO(usuarioCadastrado);
        }

        public async Task<UsuarioRespostaDTO?> AtualizarAsync(long id, UsuarioDTO usuarioDTO){
            ValidarUsuario(usuarioDTO);

            Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if(usuario == null){
                return null;
            }

            Usuario? usuarioComMesmoEmail = await _usuarioRepository.BuscarPorEmailAsync(usuarioDTO.Email);

            if(usuarioComMesmoEmail != null && usuarioComMesmoEmail.Id != id){
                throw new ArgumentException("Já existe outro usuário cadastrado com este email.");
            }

            usuario.Nome = usuarioDTO.Nome;
            usuario.Email = usuarioDTO.Email;

            PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
            usuario.Senha = passwordHasher.HashPassword(usuario, usuarioDTO.Senha);

            Usuario usuarioAtualizado = await _usuarioRepository.AtualizarAsync(usuario);

            return ConverterParaResponseDTO(usuarioAtualizado);
        }

        public async Task<bool> DeletarAsync(long id){
            return await _usuarioRepository.DeletarAsync(id);
        }

        public async Task<UsuarioLogin?> LoginAsync(UsuarioLogin usuarioLogin){
            Usuario? usuario = await _usuarioRepository.BuscarPorEmailAsync(usuarioLogin.Email);

            if(usuario == null){
                return null;
            }

            PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

            PasswordVerificationResult resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Senha,
                usuarioLogin.Senha
            );

            if(resultado == PasswordVerificationResult.Failed){
                return null;
            }

            usuarioLogin.Senha = string.Empty;
            usuarioLogin.Token = GerarToken(usuario);

            return usuarioLogin;
        }

        private string GerarToken(Usuario usuario){
            string? chaveJwt = _configuration["Jwt:Key"];

            if(string.IsNullOrWhiteSpace(chaveJwt)){
                throw new Exception("Chave JWT não configurada.");
            }

            Claim[] claims = new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            SymmetricSecurityKey chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt));

            SigningCredentials credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void ValidarUsuario(UsuarioDTO usuarioDTO){
            if(string.IsNullOrWhiteSpace(usuarioDTO.Nome)){
                throw new ArgumentException("O nome do usuário é obrigatório.");
            }

            if(string.IsNullOrWhiteSpace(usuarioDTO.Email)){
                throw new ArgumentException("O email do usuário é obrigatório.");
            }

            if(string.IsNullOrWhiteSpace(usuarioDTO.Senha)){
                throw new ArgumentException("A senha do usuário é obrigatória.");
            }

            if(usuarioDTO.Senha.Length < 6){
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
            }
        }

        private static UsuarioRespostaDTO ConverterParaResponseDTO(Usuario usuario){
            return new UsuarioRespostaDTO(
                usuario.Id,
                usuario.Nome,
                usuario.Email
            );
        }
    }
}