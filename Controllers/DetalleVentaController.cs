using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public DetalleVentaController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaDetalleVenta()
        {
            List<DetalleVenta> detalleVentas = new List<DetalleVenta>();

            try
            {
                detalleVentas = _DBLaSurtidora.DetalleVentas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = detalleVentas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = detalleVentas });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdDetalleVenta:int}")]
        public IActionResult Obtener(int IdDetalleVenta)
        {
            DetalleVenta OdetalleVenta = _DBLaSurtidora.DetalleVentas.Find(IdDetalleVenta);

            if (OdetalleVenta == null)
            {
                return BadRequest("Detalle de venta no encontrado");
            }

            try
            {
                OdetalleVenta = _DBLaSurtidora.DetalleVentas.Where(p => p.IdDetalleVenta == IdDetalleVenta).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = OdetalleVenta });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = OdetalleVenta });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] DetalleVenta detalleVenta)
        {

            try
            {
                _DBLaSurtidora.DetalleVentas.Add(detalleVenta);
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
        public IActionResult Editar([FromBody] DetalleVenta detalleVenta)
        {

            DetalleVenta OdetalleVenta = _DBLaSurtidora.DetalleVentas.Find(detalleVenta.IdDetalleVenta);

            if (OdetalleVenta == null)
            {
                return BadRequest("Detalle de venta no encontrado");
            }
            try
            {
                OdetalleVenta.CantidadProducto = detalleVenta.CantidadProducto;
                OdetalleVenta.PrecioUnitario = detalleVenta.PrecioUnitario;
                OdetalleVenta.Descuento = detalleVenta.Descuento is null ? OdetalleVenta.Descuento : detalleVenta.Descuento;

                _DBLaSurtidora.DetalleVentas.Update(OdetalleVenta);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Detalle de venta Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdDetalleVenta:int}")]

        public IActionResult Eliminar(int IdDetalleVenta)
        {
            DetalleVenta OdetalleVenta = _DBLaSurtidora.DetalleVentas.Find(IdDetalleVenta);

            if (OdetalleVenta == null)
            {
                return BadRequest("Detalle de venta no encontrado");
            }

            try
            {
                _DBLaSurtidora.DetalleVentas.Remove(OdetalleVenta);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Detalle de venta eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }
    }
}
