using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;


        public RolController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]

       public IActionResult ListaRol()
        {
            List<Rol> rol = new List<Rol>();

            try
            {
                rol = _DBLaSurtidora.Rols.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato correcto", response = rol });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = rol });
            }

        }

        [HttpGet]
        [Route("Obtener/{IdRol:int}")]
        public IActionResult Obtener(int IdRol)
        {
            Rol Orol = _DBLaSurtidora.Rols.Find(IdRol); 

            if (Orol == null)
            {
                return BadRequest("Rol no encontrado");
            }

            try
            {
                Orol = _DBLaSurtidora.Rols.Where(p => p.IdRol == IdRol).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Orol });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = Orol });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Rol rol)
        {
            try
            {
                _DBLaSurtidora.Rols.Add(rol);
                _DBLaSurtidora.SaveChanges();   

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Rol guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Rol rol)
        {

            Rol ORol = _DBLaSurtidora.Rols.Find(rol.IdRol);

         

            if (ORol == null)
            {
                return BadRequest("Rol no encontrado");
            }
            try

                // Nombre_puesto Detalles
            {

                ORol.NombrePuesto = rol.NombrePuesto;
                ORol.Detalles = rol.Detalles is null ? ORol.Detalles : rol.Detalles;

               

                _DBLaSurtidora.Rols.Update(ORol);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = " Rol Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdRol:int}")]

        public IActionResult Eliminar(int IdRol)
        {
            Rol ORol = _DBLaSurtidora.Rols.Find(IdRol);

            if (ORol == null)
            {
                return BadRequest("Rol no encontrado");
            }

            try
            {
                _DBLaSurtidora.Rols.Remove(ORol);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Rol eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }



    }

    
}
