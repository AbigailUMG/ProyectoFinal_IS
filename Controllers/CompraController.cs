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
    public class CompraController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public CompraController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaCompra()
        {
            List<Compra> Ocompras = new List<Compra>();

            try
            {
                Ocompras = _DBLaSurtidora.Compras.ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ocompras });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Ocompras });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdCompras:int}")]
        public IActionResult Obtener(int IdCompras)
        {
            Compra Ocompras = _DBLaSurtidora.Compras.Find(IdCompras);

            if (Ocompras == null)
            {
                return BadRequest("Compra no encontrado");
            }

            try
            {
                Ocompras = _DBLaSurtidora.Compras.Where(p => p.IdCompra == IdCompras).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ocompras });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok= false, mensaje = ex.Message, response = Ocompras });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Compra compra)
        {

            try
            {
                _DBLaSurtidora.Compras.Add(compra);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Compra Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Compra compra)
        {

            Compra Ocompra = _DBLaSurtidora.Compras.Find(compra.IdCompra);

            if (Ocompra == null)
            {
                return BadRequest("Compra no encontrado");
            }
            try
            {

                Ocompra.FechaCompra = compra.FechaCompra is null ? Ocompra.FechaCompra : compra.FechaCompra;
                Ocompra.Observaciones = compra.Observaciones is null ? Ocompra.Observaciones : compra.Observaciones;
                Ocompra.Total = compra.Total is null ? Ocompra.Total : compra.Total;

                _DBLaSurtidora.Compras.Update(Ocompra);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true,  mensaje = "Compra Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdCompra:int}")]

        public IActionResult Eliminar(int IdCompra)
        {
            Compra Ocompra = _DBLaSurtidora.Compras.Find(IdCompra);

            if (Ocompra == null)
            {
                return BadRequest("Compra no encontrado");
            }

            try
            {
                _DBLaSurtidora.Compras.Remove(Ocompra);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Compra eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }
    }
}
