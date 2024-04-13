﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;


namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentacionController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public PresentacionController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaPresentacion()
        {
            List<Presentacion> presentacion = new List<Presentacion>();

            try
            {
                presentacion = _DBLaSurtidora.Presentacions.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato correcto", response = presentacion });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = presentacion });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdPresentacion:int}")]
        public IActionResult Obtener(int IdPresentacion)
        {
            Presentacion Opresentacion = _DBLaSurtidora.Presentacions.Find(IdPresentacion);

            if (Opresentacion == null)
            {
                return BadRequest("Presentacion no encontrado");
            }

            try
            {
                Opresentacion = _DBLaSurtidora.Presentacions.Where(p => p.IdPresentacion == IdPresentacion).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Opresentacion });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = Opresentacion});
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Presentacion presentacion)
        {

            try
            {
                _DBLaSurtidora.Presentacions.Add(presentacion);
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
        public IActionResult Editar([FromBody] Presentacion presentacion)
        {

            Presentacion Opresentacion = _DBLaSurtidora.Presentacions.Find(presentacion.IdPresentacion);

            if (Opresentacion == null)
            {
                return BadRequest("Presentacion no encontrado");
            }
            try
            {
                Opresentacion.NombrePresentacion = presentacion.NombrePresentacion is null ? Opresentacion.NombrePresentacion : presentacion.NombrePresentacion;

                _DBLaSurtidora.Presentacions.Update(Opresentacion);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Presentacion Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdPresentacion:int}")]

        public IActionResult Eliminar(int IdPresentacion)
        {
            Presentacion Opresentacion = _DBLaSurtidora.Presentacions.Find(IdPresentacion);

            if (Opresentacion == null)
            {
                return BadRequest("Presentacion no encontrado");
            }

            try
            {
                _DBLaSurtidora.Presentacions.Remove(Opresentacion);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Presentacion eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }
    }
}
