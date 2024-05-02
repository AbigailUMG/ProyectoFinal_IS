using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public ProductoController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]


        public IActionResult ListaProducto()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                productos = _DBLaSurtidora.Productos.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = productos });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = productos });
            }

        }

        [HttpGet]
        [Route("Lista-Desactivados")]


        public IActionResult ListaProductoNulos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                productos = _DBLaSurtidora.Productos.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = productos });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = productos });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdProducto:int}")]
        public IActionResult Obtener(int IdProducto)
        {
            Producto Oproducto = _DBLaSurtidora.Productos.Find(IdProducto);

            if (Oproducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                Oproducto = _DBLaSurtidora.Productos.Where(p => p.IdProductos == IdProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Oproducto });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Oproducto });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto producto)
        {

            try
            {
                producto.Estado = true;
                _DBLaSurtidora.Productos.Add(producto);
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
        public IActionResult Editar([FromBody] Producto producto)
        {

            Producto Oproducto = _DBLaSurtidora.Productos.Find(producto.IdProductos);

            if (Oproducto == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {

                Oproducto.NombreProducto = producto.NombreProducto;
                Oproducto.Descripcion = producto.Descripcion is null ? Oproducto.Descripcion : producto.Descripcion;
                Oproducto.CodigoBarras = producto.CodigoBarras is null ? Oproducto.CodigoBarras : producto.CodigoBarras;
                Oproducto.Tamanio = producto.Tamanio;
                Oproducto.Estado = producto.Estado;


                _DBLaSurtidora.Productos.Update(Oproducto);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Producto Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Producto producto)
        {
            Producto Oproducto = _DBLaSurtidora.Productos.Find(producto.IdProductos);
            if (Oproducto == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                Oproducto.Estado = producto.Estado;
                _DBLaSurtidora.Productos.Update(Oproducto);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }




        //[HttpDelete]
        //[Route("Eliminar/{IdProducto:int}")]

        //public IActionResult Eliminar(int IdProducto)
        //{
        //    Producto Oproducto = _DBLaSurtidora.Productos.Find(IdProducto);

        //    if (Oproducto == null)
        //    {
        //        return BadRequest("Producto no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.Productos.Remove(Oproducto);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Producto eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //    }

        //}
    }
}
