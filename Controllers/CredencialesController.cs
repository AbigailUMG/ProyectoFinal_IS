﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using BackendApi.Services;

using Microsoft.AspNetCore.Cors;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public CredencialesController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaCredenciales()
        {
            List<Credenciale> credenciales = new List<Credenciale>();

            Console.WriteLine("Abiestuvoaqui");
            try
            {
                credenciales = _DBLaSurtidora.Credenciales.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato correcto", response = credenciales });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = credenciales });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdCredencial:int}")]
        public IActionResult Obtener(int IdCredencial)
        {
            Credenciale Ocredencial = _DBLaSurtidora.Credenciales.Find(IdCredencial);

            if (Ocredencial == null)
            {
                return BadRequest("credencial no encontrado");
            }

            try
            {
                Ocredencial = _DBLaSurtidora.Credenciales.Where(p => p.IdCredenciales == IdCredencial).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Ocredencial });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = Ocredencial });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Credenciale credenciales)
        {


            try
            {

                string ContraEncrip = Encriptacion.EncripContra(credenciales.Password);
                credenciales.Password = ContraEncrip;


                _DBLaSurtidora.Credenciales.Add(credenciales);
                _DBLaSurtidora.SaveChanges();

                Console.WriteLine(credenciales);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credenciales Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                // Agregar registros de depuración para imprimir la excepción interna
                Console.WriteLine("Error al guardar credenciales: " + ex.ToString());

                // Devolver un código de estado 500 para indicar un error interno del servidor
                //return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor al guardar credenciales" });
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Error al guardar los datos", detalle = ex.InnerException.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Credenciale credenciale)
        {

            Credenciale Ocredenciale = _DBLaSurtidora.Credenciales.Find(credenciale.IdCredenciales);

            if (Ocredenciale == null)
            {
                return BadRequest("Credencial no encontrado");
            }
            try
            {
                Ocredenciale.Username = credenciale.Username is null ? Ocredenciale.Username : credenciale.Username;
                Ocredenciale.Password = credenciale.Password is null ? Ocredenciale.Password : credenciale.Password;
                Ocredenciale.Estado = credenciale.Estado is null ? Ocredenciale.Estado : credenciale.Estado;

                _DBLaSurtidora.Credenciales.Update(Ocredenciale);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credenciales Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdCredenciales:int}")]

        public IActionResult Eliminar(int Idcredenciales)
        {
            Credenciale Ocredencial = _DBLaSurtidora.Credenciales.Find(Idcredenciales);

            if (Ocredencial == null)
            {
                return BadRequest("Credencial no encontrado");
            }

            try
            {
                _DBLaSurtidora.Credenciales.Remove(Ocredencial);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credencial eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }

 [HttpPost]
 [Route("Validar")]
 public IActionResult Validar([FromBody] Credenciale validacion )
 {
    
     Console.WriteLine(validacion.Username);

     if (validacion.Username == null && validacion.Password == null)
     {
         return BadRequest("datos no encontrados");
     }
     try
     {
         var credenciales = this._DBLaSurtidora.Credenciales.FirstOrDefault(c => c.Username == validacion.Username);
         var PassEntrada = Encriptacion.EncripContra(validacion.Password);

         Console.WriteLine(PassEntrada);

         Console.WriteLine( validacion.Password);

         if (credenciales!= null && credenciales.Password == PassEntrada)
         {
             return StatusCode(StatusCodes.Status200OK, new { mensaje = "Inicio de Sesion Correcto" });
         }
         else
         {
             return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credenciales no existentes " });
         }
      
     }
     catch (Exception ex)
     {
         //return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
         return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Error al guardar los datos", detalle = ex.InnerException.Message });
     }



 }





    }

}
