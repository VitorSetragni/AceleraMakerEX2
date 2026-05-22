using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BlogPessoal.Controllers{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase{

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService){
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRespostaDTO>> BuscarPorId(long id){
            UsuarioRespostaDTO? usuario = await _usuarioService.BuscarPorIdAsync(id);

            if(usuario == null){
                return NotFound(new{
                    mensagem = "Usuário não encontrado."
                });
            }

            return Ok(usuario);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioRespostaDTO>> Cadastrar([FromBody] UsuarioDTO usuarioDTO){
            try{
                UsuarioRespostaDTO usuario = await _usuarioService.CadastrarAsync(usuarioDTO);

                return CreatedAtAction(nameof(BuscarPorId), new{ id = usuario.Id }, usuario);
            }
            catch(ArgumentException erro){
                return BadRequest(new{
                    mensagem = erro.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLogin>> Login([FromBody] UsuarioLogin usuarioLogin){
            UsuarioLogin? usuarioAutenticado = await _usuarioService.LoginAsync(usuarioLogin);

            if(usuarioAutenticado == null){
                return Unauthorized(new{
                    mensagem = "Email ou senha inválidos."
                });
            }

            return Ok(usuarioAutenticado);
        }

        [HttpPost("logout")]
        public ActionResult Logout(){
            return Ok(new{
                mensagem = "Logout realizado."
            });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioRespostaDTO>> Atualizar(long id, [FromBody] UsuarioDTO usuarioDTO){
            try{
                long? usuarioIdToken = ObterUsuarioIdDoToken();

                if(usuarioIdToken == null){
                    return Unauthorized(new{
                        mensagem = "Token inválido."
                    });
                }

                if(usuarioIdToken.Value != id){
                    return Forbid("Você não pode alterar outro usuário.");
                }
                UsuarioRespostaDTO? usuario = await _usuarioService.AtualizarAsync(id, usuarioDTO);

                if(usuario == null){
                    return NotFound(new{
                        mensagem = "Usuário não encontrado."
                    });
                }

                return Ok(usuario);
            }
            catch(ArgumentException erro){
                return BadRequest(new{
                    mensagem = erro.Message
                });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(long id){

            long? usuarioIdToken = ObterUsuarioIdDoToken();

            if(usuarioIdToken == null){
                return Unauthorized(new{
                    mensagem = "Token inválido."
                });
            }

            if(usuarioIdToken.Value != id){
                return Forbid("Você não pode alterar outro usuário.");
            }
            bool deletado = await _usuarioService.DeletarAsync(id);

            if(!deletado){
                return NotFound(new{
                    mensagem = "Usuário não encontrado."
                });
            }

            return NoContent();
        }

        private long? ObterUsuarioIdDoToken(){
            string? usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrWhiteSpace(usuarioId)){
                return null;
            }

            return long.Parse(usuarioId);
        }
    }
}