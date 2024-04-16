using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using System.Numerics;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public DetalleCompraController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaDetalleCompra()
        {
            List<DetalleCompra> detalleCompras = new List<DetalleCompra>();

            try
            {
                detalleCompras = _DBLaSurtidora.DetalleCompras.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato correcto", response = detalleCompras });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = detalleCompras });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdDetalleCompra:int}")]
        public IActionResult Obtener(long IdDetalleCompra)
        {
            DetalleCompra detalleCompra = _DBLaSurtidora.DetalleCompras.Find(IdDetalleCompra);

            if (detalleCompra == null)
            {
                return BadRequest("Detalle de compra no encontrado");
            }

            try
            {
                detalleCompra = _DBLaSurtidora.DetalleCompras.Where(p => p.IdDetalleCompra == IdDetalleCompra).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = detalleCompra });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = detalleCompra });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] DetalleCompra detalleCompra)
        {

            try
            {
                _DBLaSurtidora.DetalleCompras.Add(detalleCompra);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] DetalleCompra detalleCompra)
        {

            DetalleCompra OdetalleCompra = _DBLaSurtidora.DetalleCompras.Find(detalleCompra.IdDetalleCompra);

            if (OdetalleCompra == null)
            {
                return BadRequest("Detalle Compra no encontrado");
            }
            try
            {
                OdetalleCompra.Descuentos = detalleCompra.Descuentos is null ? OdetalleCompra.Descuentos : detalleCompra.Descuentos;
                OdetalleCompra.CantidadCompra = detalleCompra.CantidadCompra;
                OdetalleCompra.PrecioUnitarioCompra = detalleCompra.PrecioUnitarioCompra;
                OdetalleCompra.PrecioSugeridoVenta = detalleCompra.PrecioSugeridoVenta;
                OdetalleCompra.NoLote = detalleCompra.NoLote;

                _DBLaSurtidora.DetalleCompras.Update(OdetalleCompra);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Detalle Compra Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdDetalleCompra:int}")]

        public IActionResult Eliminar(int IdDetalleCompra)
        {
            DetalleCompra detalleCompra = _DBLaSurtidora.DetalleCompras.Find(IdDetalleCompra);

            if (detalleCompra == null)
            {
                return BadRequest("Detalle de Compra no encontrado");
            }

            try
            {
                _DBLaSurtidora.DetalleCompras.Remove(detalleCompra);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Detalle de Compra eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }
    }
}
