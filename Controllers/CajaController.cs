﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

using Microsoft.AspNetCore.Cors;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public CajaController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaCaja()
        {
            List<Caja> cajas = new List<Caja>();

            try
            {
                cajas = _DBLaSurtidora.Cajas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = cajas });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = cajas });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdCaja:int}")]
        public IActionResult Obtener(int IdCaja)
        {
            Caja Ocaja = _DBLaSurtidora.Cajas.Find(IdCaja);

            if (Ocaja == null)
            {
                return BadRequest("Caja no encontrado");
            }

            try
            {
                Ocaja = _DBLaSurtidora.Cajas.Where(p => p.IdCaja == IdCaja).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ocaja });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Ocaja });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Caja cajas)
        {

            try
            {
                _DBLaSurtidora.Cajas.Add(cajas);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Dato Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok= false, mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Caja cajas)
        {

            Caja OCaja = _DBLaSurtidora.Cajas.Find(cajas.IdCaja);

            if (OCaja == null)
            {
                return BadRequest("Caja no encontrado");
            }
            try
            {

                OCaja.Anio = cajas.Anio;
                OCaja.Mes = cajas.Mes;
                OCaja.TotalGastos = cajas.TotalGastos;
                OCaja.TotalIngresos = cajas.TotalIngresos;
                OCaja.TotalCaja = cajas.TotalCaja;



                _DBLaSurtidora.Cajas.Update(OCaja);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Categoria Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdCaja:int}")]

        public IActionResult Eliminar(int IdCaja)
        {
            Caja Ocaja = _DBLaSurtidora.Cajas.Find(IdCaja);

            if (Ocaja == null)
            {
                return BadRequest("Categoria no encontrado");
            }

            try
            {
                _DBLaSurtidora.Cajas.Remove(Ocaja);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Categoria eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }
    }
}
