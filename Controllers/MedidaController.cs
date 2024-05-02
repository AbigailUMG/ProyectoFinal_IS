using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

using Microsoft.AspNetCore.Cors;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]

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
        [Route("Lista-Activados")]


        public IActionResult ListaMedida()
        {
            List<UnidadesMedida> medidas = new List<UnidadesMedida>();

            try
            {
                medidas = _DBLaSurtidora.UnidadesMedidas.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = medidas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = medidas});
            }

        }

        [HttpGet]
        [Route("Lista-Desactivados")]


        public IActionResult ListaMedidaNulos()
        {
            List<UnidadesMedida> medidas = new List<UnidadesMedida>();

            try
            {
                medidas = _DBLaSurtidora.UnidadesMedidas.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = medidas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = medidas });
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

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Omedida });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Omedida });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] UnidadesMedida medida)
        {

            try
            {
                medida.Estado = true;
                _DBLaSurtidora.UnidadesMedidas.Add(medida);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Dato Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] UnidadesMedida medida)
        {

            UnidadesMedida OMedida = _DBLaSurtidora.UnidadesMedidas.Find(medida.IdMedicion);


            if (OMedida == null)
            {
                return BadRequest("Medida no encontrado");
            }
            try
            {

                OMedida.Descripcion = medida.Descripcion;
                OMedida.Prefijo = medida.Prefijo;

                _DBLaSurtidora.UnidadesMedidas.Update(OMedida);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Unidad de Medidas Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(UnidadesMedida unidadesMedida)
        {
            UnidadesMedida Ounidades = _DBLaSurtidora.UnidadesMedidas.Find(unidadesMedida.IdMedicion);
            if (Ounidades == null)
            {
                return BadRequest("Unidades de medida no encontrado");
            }
            try
            {
                Ounidades.Estado = unidadesMedida.Estado;
                _DBLaSurtidora.UnidadesMedidas.Update(Ounidades);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Unidades cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }







        //[HttpDelete]
        //[Route("Eliminar/{IdMedida:int}")]

        //public IActionResult Eliminar(int IdMedida)
        //{
        //    UnidadesMedida OMedida = _DBLaSurtidora.UnidadesMedidas.Find(IdMedida);

        //    if (OMedida == null)
        //    {
        //        return BadRequest("Unidad de Medida no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.UnidadesMedidas.Remove(OMedida);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Unidad Medida eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //    }

        //}









    }
}
