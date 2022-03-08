using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaletrantesController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        public PaletrantesController(IPalestranteService palestranteService)
        {
            _palestranteService = palestranteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var palestrantes = await _palestranteService.GetAllPalestrantesByAsync(true);

                if(palestrantes == null) return NotFound("Nenhum palestrantes encontrado");

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar palestrantes. Erro:{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                 var palestrantes = await _palestranteService.GetAllPalestranteByIdAsync(id, true);

                if(palestrantes == null) return NotFound("Nenhum palestrante encontrado");

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o palestrante. Erro:{ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetBytema(string tema)
        {
            try
            {
                 var palestrantes = await _palestranteService.GetAllPalestranteByNomeAsync(tema, true);

                if(palestrantes == null) return NotFound("Nenhum palestrante por tema não encontrados");

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o palestrante. Erro:{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante palestrante)
        {
            try
            {
                var palestrantes = await _palestranteService.AddPalestrante(palestrante);

                if(palestrantes == null) return BadRequest("Erro ao inserir o palestrante.");

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao inserir o palestrante. Erro:{ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Palestrante palestrante)
        {
            try
            {
                var palestrantes = await _palestranteService.UpdatePalestrante(id, palestrante);

                if(palestrantes == null) return NotFound("Erro ao atualizar o palestrante");

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar o palestrante. Erro:{ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if( await _palestranteService.DeletePalestrante(id))
                    return Ok("Palestrante deletado");
                else
                    return BadRequest("Palestrante não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o paletrante. Erro:{ex.Message}");
            }
        }
    }
}