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
    public class CategoriasController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public CategoriasController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]
       

        public IActionResult ListaCategoria(){
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                categorias = _DBLaSurtidora.Categorias.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = categorias });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = categorias });
            }

        }
        [HttpGet]
        [Route("Lista-Desactivados")]
       

        public IActionResult ListaCategoriaNulos(){
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                categorias = _DBLaSurtidora.Categorias.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = categorias });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = categorias });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdCategoria:int}")]
        public IActionResult Obtener(int IdCategoria)
        {
            Categoria Ocategoria = _DBLaSurtidora.Categorias.Find(IdCategoria);

            if (Ocategoria == null)
            {
                return BadRequest(new { ok = false, mensaje = "categoria no encontrado" });
            }

            try
            {
                Ocategoria = _DBLaSurtidora.Categorias.Where(p => p.IdCategoria == IdCategoria).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ocategoria });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje =  ex.Message, response = Ocategoria });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Categoria categoria)
        {

            try
            {
                _DBLaSurtidora.Categorias.Add(categoria);
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
        public IActionResult Editar([FromBody] Categoria categoria)
        {

            Categoria OCategoria = _DBLaSurtidora.Categorias.Find(categoria.IdCategoria);

            if (OCategoria == null)
            {
                return BadRequest("Categoria no encontrado");
            }
            try
            {
                OCategoria.NombreCategoria = categoria.NombreCategoria;
                OCategoria.Descripcion = categoria.Descripcion is null ? OCategoria.Descripcion: categoria.Descripcion;
                OCategoria.Estado = categoria.Estado;

                _DBLaSurtidora.Categorias.Update(OCategoria);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Categoria Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Categoria categoria)
        {
            Categoria Ocategoria = _DBLaSurtidora.Categorias.Find(categoria.IdCategoria);
            if (Ocategoria == null)
            {
                return BadRequest("Categoria no encontrado");
            }
            try
            {
                Ocategoria.Estado = categoria.Estado;
                _DBLaSurtidora.Categorias.Update(Ocategoria);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }


        //[HttpDelete]
        //[Route("Eliminar/{IdCategoria:int}")]

        //public IActionResult Eliminar(int IdCategoria)
        //{
        //    Categoria Ocategoria = _DBLaSurtidora.Categorias.Find(IdCategoria);

        //    if (Ocategoria == null)
        //    {
        //        return BadRequest("Categoria no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.Categorias.Remove(Ocategoria);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok= true, mensaje = "Categoria eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok= false, mensaje = ex.Message });
        //    }

        //}



    }
}
