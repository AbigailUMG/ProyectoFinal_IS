using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;


namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public MedidaController(LaSurtidoraBuenPrecioIsContext _LaSurtidora)
        {
            _DBLaSurtidora = _LaSurtidora;

        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaMedida()
        {
            List<UnidadesMedida> medidas = new List<UnidadesMedida>();

            try
            {
                medidas = _DBLaSurtidora.UnidadesMedidas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Medida correcto", response = medidas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = medidas});
            }

        }

        [HttpGet]
        [Route("Obtener/{IdMedida:int}")]
        public IActionResult Obtener(int IdMedida)
        {
            UnidadesMedida Omedida = _DBLaSurtidora.UnidadesMedidas.Find(IdMedida);

            if (Omedida == null)
            {
                return BadRequest("Medida no encontrado");
            }

            try
            {
                Omedida = _DBLaSurtidora.UnidadesMedidas.Where(p => p.IdMedicion == IdMedida).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Omedida });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = Omedida });
            }
        }







    }
}
