using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogPessoal.Controllers{

    [ApiController]
    [Route("api/postagens")]
    public class PostagemController : ControllerBase{

        private readonly IPostagemService _postagemService;

        public PostagemController(IPostagemService postagemService){
            _postagemService = postagemService;
        }

        // Lista todas as postagens cadastradas.
        [HttpGet]
        public async Task<ActionResult<List<Postagem>>> ListarTodos(){
            List<Postagem> postagens = await _postagemService.ListarTodosAsync();

            return Ok(postagens);
        }

        // Busca uma postagem pelo Id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Postagem>> BuscarPorId(long id){
            Postagem? postagem = await _postagemService.BuscarPorIdAsync(id);

            if(postagem == null){
                return NotFound(new{
                    mensagem = "Postagem não encontrada."
                });
            }

            return Ok(postagem);
        }

        // Busca postagens pelo Id do usuário.
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<Postagem>>> BuscarPorUsuarioId(long usuarioId){
            try{
                List<Postagem> postagens = await _postagemService.BuscarPorUsuarioIdAsync(usuarioId);

                return Ok(postagens);
            }
            catch(ArgumentException erro){
                return NotFound(new{
                    mensagem = erro.Message
                });
            }
        }


        private long? ObterUsuarioIdDoToken(){
            string? usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrWhiteSpace(usuarioId)){
                return null;
            }

            return long.Parse(usuarioId);
        }


        // Busca postagens pelo Id do tema.
        [HttpGet("tema/{temaId}")]
        public async Task<ActionResult<List<Postagem>>> BuscarPorTemaId(long temaId){
            try{
                List<Postagem> postagens = await _postagemService.BuscarPorTemaIdAsync(temaId);

                return Ok(postagens);
            }
            catch(ArgumentException erro){
                return NotFound(new{
                    mensagem = erro.Message
                });
            }
        }

        // Filtra postagens por usuarioId e tema.
        [HttpGet("filtro")]
        public async Task<ActionResult<List<Postagem>>> Filtrar(
            [FromQuery] long? usuarioId,
            [FromQuery] long? tema
        ){
            try{
                List<Postagem> postagens = await _postagemService.FiltrarAsync(usuarioId, tema);

                return Ok(postagens);
            }
            catch(ArgumentException erro){
                return NotFound(new{
                    mensagem = erro.Message
                });
            }
        }

        // Cadastra uma nova postagem vinculada a um usuário e a um tema.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Postagem>> Cadastrar([FromBody] PostagemDTO postagemDTO){
            try{
                long? usuarioId = ObterUsuarioIdDoToken();

                if(usuarioId == null){
                    return Unauthorized(new{
                        mensagem = "Token inválido."
                    });
                }

                Postagem postagem = await _postagemService.CadastrarAsync(postagemDTO, usuarioId.Value);

                return CreatedAtAction(nameof(BuscarPorId), new{ id = postagem.Id }, postagem);
            }
            catch(ArgumentException erro){
                return BadRequest(new{
                    mensagem = erro.Message
                });
            }
        }

        // Atualiza uma postagem existente.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Postagem>> Atualizar(long id, [FromBody] PostagemDTO postagemDTO){
            try{
                long? usuarioId = ObterUsuarioIdDoToken();

                if(usuarioId == null){
                    return Unauthorized(new{
                        mensagem = "Token inválido."
                    });
                }

                Postagem? postagem = await _postagemService.AtualizarAsync(id, postagemDTO, usuarioId.Value);

                if(postagem == null){
                    return NotFound(new{
                        mensagem = "Postagem não encontrada."
                    });
                }

                return Ok(postagem);
            }
            catch(ArgumentException erro){
                return BadRequest(new{
                    mensagem = erro.Message
                });
            }
            catch(UnauthorizedAccessException erro){
                return Forbid(erro.Message);
            }
        }

        // Remove uma postagem pelo Id.
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(long id){
            try{
                long? usuarioId = ObterUsuarioIdDoToken();

                if(usuarioId == null){
                    return Unauthorized(new{
                        mensagem = "Token inválido."
                    });
                }

                bool deletado = await _postagemService.DeletarAsync(id, usuarioId.Value);

                if(!deletado){
                    return NotFound(new{
                        mensagem = "Postagem não encontrada."
                    });
                }

                return NoContent();
            }
            catch(UnauthorizedAccessException erro){
                return Forbid(erro.Message);
            }
        }
    }
}