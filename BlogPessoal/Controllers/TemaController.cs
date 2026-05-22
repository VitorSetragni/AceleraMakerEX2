using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BlogPessoal.Controllers{

    [ApiController]
    [Route("api/temas")]
    public class TemaController : ControllerBase{

        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService){
            _temaService = temaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tema>>> ListarTodos(){
            List<Tema> temas = await _temaService.ListarTodosAsync();

            return Ok(temas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tema>> BuscarPorId(long id){
            Tema? tema = await _temaService.BuscarPorIdAsync(id);

            if(tema == null){
                return NotFound(new{
                    mensagem = "Tema não encontrado."
                });
            }

            return Ok(tema);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Tema>> Cadastrar([FromBody] TemaDTO temaDTO){
            try{
                Tema tema = await _temaService.CadastrarAsync(temaDTO);

                return CreatedAtAction(nameof(BuscarPorId), new{ id = tema.Id }, tema);
            }
            catch(ArgumentException erro){
                return BadRequest(new{
                    mensagem = erro.Message
                });
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Tema>> Atualizar(long id, [FromBody] TemaDTO temaDTO){
            try{
                Tema? tema = await _temaService.AtualizarAsync(id, temaDTO);

                if(tema == null){
                    return NotFound(new{
                        mensagem = "Tema não encontrado."
                    });
                }

                return Ok(tema);
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
            bool deletado = await _temaService.DeletarAsync(id);

            if(!deletado){
                return NotFound(new{
                    mensagem = "Tema não encontrado."
                });
            }

            return NoContent();
        }
    }
}