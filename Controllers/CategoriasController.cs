using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;


namespace BackendApi.Controllers
{
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
        [Route("Obtener/{IdCategoria:int}")]
        public IActionResult Obtener(int IdCategoria)
        {
            Categoria Ocategoria = _DBLaSurtidora.Categorias.Find(IdCategoria);

            if (Ocategoria == null)
            {
                return BadRequest("categoria no encontrado");
            }

            try
            {
                Ocategoria = _DBLaSurtidora.Categorias.Where(p => p.IdCategoria == IdCategoria).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Ocategoria });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = Ocategoria });
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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
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
                
                OCategoria.Descripcion = categoria.Descripcion is null ? OCategoria.Descripcion: categoria.Descripcion;
                OCategoria.NombreCategoria = categoria.NombreCategoria is null ? OCategoria.NombreCategoria : categoria.NombreCategoria;

                _DBLaSurtidora.Categorias.Update(OCategoria);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdCategoria:int}")]

        public IActionResult Eliminar(int IdCategoria)
        {
            Categoria Ocategoria = _DBLaSurtidora.Categorias.Find(IdCategoria);

            if (Ocategoria == null)
            {
                return BadRequest("Categoria no encontrado");
            }

            try
            {
                _DBLaSurtidora.Categorias.Remove(Ocategoria);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }

    }
}
