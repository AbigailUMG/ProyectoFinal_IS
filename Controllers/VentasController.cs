using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public VentasController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]


        public IActionResult ListaVentas()
        {
            List<Venta> ventas = new List<Venta>();

            try
            {
                ventas = _DBLaSurtidora.Ventas.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Ventas enviados correctamente", response = ventas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = ventas });
            }

        }

        [HttpGet]
        [Route("Lista-Desactivados")]


        public IActionResult ListaVentasNulos()
        {
            List<Venta> ventas = new List<Venta>();

            try
            {
                ventas = _DBLaSurtidora.Ventas.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Ventas enviados correctamente", response = ventas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = ventas });
            }

        }



        [HttpGet]
        [Route("Obtener/{IdVenta:int}")]
        public IActionResult Obtener(int IdVenta)
        {
            Venta Oventa = _DBLaSurtidora.Ventas.Find(IdVenta);

            if (Oventa == null)
            {
                return BadRequest(new { ok = false, mensaje = "Venta no encontrado" });
            }

            try
            {
                Oventa = _DBLaSurtidora.Ventas.Where(p => p.IdVenta == IdVenta).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Oventa });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Oventa });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Venta venta)
        {

            try
            {
                venta.Estado = true;
                _DBLaSurtidora.Ventas.Add(venta);
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
        public IActionResult Editar([FromBody] Venta venta)
        {

            Venta Oventa = _DBLaSurtidora.Ventas.Find(venta.IdVenta);

            if (Oventa == null)
            {
                return BadRequest("Venta no encontrado");
            }
            try
            {
                Oventa.FechaVenta = venta.FechaVenta;
                Oventa.Total = venta.Total;
                Oventa.Estado = venta.Estado is null ? Oventa.Estado : venta.Estado;
               

                _DBLaSurtidora.Ventas.Update(Oventa);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Venta Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Venta venta)
        {
            Venta Oventa = _DBLaSurtidora.Ventas.Find(venta.IdVenta);
            if (Oventa == null)
            {
                return BadRequest("Venta no encontrado");
            }
            try
            {
                Oventa.Estado = venta.Estado;
                _DBLaSurtidora.Ventas.Update(Oventa);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Venta cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }
    }
}
