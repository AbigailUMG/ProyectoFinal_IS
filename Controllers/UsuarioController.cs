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
    public class UsuarioController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public UsuarioController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista")]


        public IActionResult ListaUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                usuarios = _DBLaSurtidora.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = usuarios });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = usuarios });
            }

        }

        [HttpGet]
        [Route("Obtener/{IdUsuario:int}")]
        public IActionResult Obtener(int IdUsuario)
        {
            Usuario Ousuario = _DBLaSurtidora.Usuarios.Find(IdUsuario);

            if (Ousuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {
                Ousuario = _DBLaSurtidora.Usuarios.Where(p => p.IdUsuario == IdUsuario).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ousuario });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Ousuario });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Usuario usuario)
        {

            try
            {
                _DBLaSurtidora.Usuarios.Add(usuario);
                _DBLaSurtidora.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Usuario Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Usuario usuario)
        {

            Usuario OUsuario = _DBLaSurtidora.Usuarios.Find(usuario.IdUsuario);

            if (OUsuario == null)
            {
                return BadRequest("Categoria no encontrado");
            }
            try
            {
                OUsuario.Email = usuario.Email is null ? OUsuario.Email: usuario.Email;
                OUsuario.PrimerNombre = usuario.PrimerNombre;
                OUsuario.SegundoNombre = usuario.SegundoNombre is null ? OUsuario.SegundoNombre : usuario.SegundoNombre;
                OUsuario.OtrosNombres = usuario.OtrosNombres is null ? OUsuario.OtrosNombres : usuario.OtrosNombres;
                OUsuario.PrimerApellido = usuario.PrimerApellido is null ? OUsuario.PrimerApellido : usuario.PrimerApellido;
                OUsuario.SegundoApellido = usuario.SegundoApellido is null ? OUsuario.SegundoApellido : usuario.SegundoApellido;
                OUsuario.FechaRegistro = usuario.FechaRegistro is null ? OUsuario.FechaRegistro : usuario.FechaRegistro;

                _DBLaSurtidora.Usuarios.Update(OUsuario);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Usuario Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdUsuario:int}")]

        public IActionResult Eliminar(int IdUsuario)
        {
            Usuario Ousuario = _DBLaSurtidora.Usuarios.Find(IdUsuario);

            if (Ousuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            try
            {
                _DBLaSurtidora.Usuarios.Remove(Ousuario);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Usuario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }

        }



    }
}
