using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public InventarioController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaInventario()
        {
            List<Inventario> inventarios = new List<Inventario>();

            try
            {
                inventarios = _DBLaSurtidora.Inventarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = inventarios });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = inventarios });
            }

        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Inventario inventario)
        {

            try
            {
                _DBLaSurtidora.Inventarios.Add(inventario);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Iventario Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }

        [HttpGet]
        [Route("FiltrarPorFecha")]
        public IActionResult FiltrarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Inventario> inventarios = new List<Inventario>();

            try
            {
                inventarios = _DBLaSurtidora.Inventarios
                    .Where(inv => inv.FechaUltimaEntrada >= fechaInicio && inv.FechaUltimaEntrada <= fechaFin)
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos filtrados correctamente", response = inventarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = inventarios });
            }
        }

        [HttpGet]
        [Route("FiltrarPorDia")]
        public IActionResult FiltrarPorDia(DateTime fecha)
        {
            DateTime fechaInicio = fecha.Date;
            DateTime fechaFin = fecha.Date.AddDays(1).AddSeconds(-1);

            return FiltrarPorFecha(fechaInicio, fechaFin);
        }


        [HttpGet]
        [Route("FiltrarPorMes")]
        public IActionResult FiltrarPorMes(DateTime fecha)
        {
            DateTime fechaInicio = new DateTime(fecha.Year, fecha.Month, 1);//Omitir el año
            DateTime fechaFin = fechaInicio.AddMonths(1).AddSeconds(-1);

            return FiltrarPorFecha(fechaInicio, fechaFin);
        }

        [HttpGet]
        [Route("FiltrarPorAnio")]
        public IActionResult FiltrarPorAnio(int anio)
        {
            DateTime fechaInicio = new DateTime(anio, 1, 1);
            DateTime fechaFin = fechaInicio.AddYears(1).AddSeconds(-1);

            return FiltrarPorFecha(fechaInicio, fechaFin);

        }



    }
}
