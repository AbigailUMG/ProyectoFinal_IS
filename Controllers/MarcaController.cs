﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    // [Authorize(Roles = "vendedor, administrador")]
    [ApiController]
    
    public class MarcaController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public MarcaController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]

        public IActionResult ListaMarca()
        {
            List<Marca> marcas = new List<Marca>();

            try
            {
                marcas = _DBLaSurtidora.Marcas.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = marcas });

            }

            catch (Exception ex)
            { 
                    return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = marcas });
            }
        }

        [HttpGet]
        [Route("Lista-desactivados")]
        public IActionResult ListaMarcaNulos()
        {
            List<Marca> marcas = new List<Marca>();

            try
            {
                marcas = _DBLaSurtidora.Marcas.Where(m => m.Estado == false).ToList();
                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Dato correcto", response = marcas });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = marcas });
            }
        }

        [HttpGet]
        [Route("Obtener/{IdMarca:int}")]

        public IActionResult Obtener(int IdMarca)
        {
            Marca marca = _DBLaSurtidora.Marcas.Find(IdMarca);

            if (marca == null)
            {
                return BadRequest("marca no encontrado");
            }

            try
            {
                marca = _DBLaSurtidora.Marcas.Where(p => p.IdMarcas == IdMarca).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = marca });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = marca });


            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Marca marca)
        {

            try
            {
                marca.Estado = true;

                _DBLaSurtidora.Marcas.Add(marca);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos Guardado Exitosamente "});
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Marca marca)
        {
            Marca Omarca = _DBLaSurtidora.Marcas.Find(marca.IdMarcas);

            if (Omarca == null)
            {
                return BadRequest("Marca no encontrado");
            }

            try
            {

                Omarca.NombreMarca = marca.NombreMarca;
                Omarca.Estado = marca.Estado;
                Omarca.Descripcion = marca.Descripcion is null ? Omarca.Descripcion : marca.Descripcion;

                _DBLaSurtidora.Marcas.Update(Omarca);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Marca Editado Exitosamente" });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }


        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Marca marca)
        {
            Marca Omarca = _DBLaSurtidora.Marcas.Find(marca.IdMarcas);
            if (Omarca == null)
            {
                return BadRequest("Marca no encontrado");
            }
            try
            {
                Omarca.Estado = marca.Estado;
                _DBLaSurtidora.Marcas.Update(Omarca);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Marca cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }

        //[HttpDelete]
        //[Route("Eliminar/{IdMarca:int}")]

        //public IActionResult Eliminar(int IdMarca)
        //{
        //    Marca Omarca = _DBLaSurtidora.Marcas.Find(IdMarca);

        //    if (Omarca == null)
        //    {
        //        return BadRequest("Marca no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.Marcas.Remove(Omarca);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Marca eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //    }

        //}


    }

}

