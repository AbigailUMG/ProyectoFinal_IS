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
    public class ProveedoresController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public ProveedoresController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]


        public IActionResult ListaProveedor()
        {
            List<Proveedore> proveedores = new List<Proveedore>();

            try
            {
                proveedores = _DBLaSurtidora.Proveedores.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = proveedores });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = proveedores });
            }

        }

        [HttpGet]
        [Route("Lista-Desactivados")]


        public IActionResult ListaProveedorNulos()
        {
            List<Proveedore> proveedores = new List<Proveedore>();

            try
            {
                proveedores = _DBLaSurtidora.Proveedores.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = proveedores });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = proveedores });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdProveedor:int}")]
        public IActionResult Obtener(int IdProveedor)
        {
            Proveedore proveedore = _DBLaSurtidora.Proveedores.Find(IdProveedor);

            if (proveedore == null)
            {
                return BadRequest("categoria no encontrado");
            }

            try
            {
                proveedore = _DBLaSurtidora.Proveedores.Where(p => p.IdProveedor == IdProveedor).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = proveedore });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = proveedore });
            }
        }





        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Proveedore proveedor)
        {
            try
            {

                proveedor.Estado = true;
                _DBLaSurtidora.Proveedores.Add(proveedor);
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
        public IActionResult Editar([FromBody] Proveedore proveedor)
        {

            Proveedore Oproveedor = _DBLaSurtidora.Proveedores.Find(proveedor.IdProveedor);

            if (Oproveedor == null)
            {
                return BadRequest("Proveedor no encontrado");
            }
            try
            {

                Oproveedor.NombreProveedor = proveedor.NombreProveedor is null ? Oproveedor.NombreProveedor : proveedor.NombreProveedor;
                Oproveedor.Telefono = proveedor.Telefono is null ? Oproveedor.Telefono : proveedor.Telefono;
                Oproveedor.Estado = proveedor.Estado is null ? Oproveedor.Estado : proveedor.Estado;
                Oproveedor.DiaVisita = proveedor.DiaVisita is null ? Oproveedor.DiaVisita : proveedor.DiaVisita;
                Oproveedor.DiaEntrega = proveedor.DiaEntrega is null ? Oproveedor.DiaEntrega : proveedor.DiaEntrega;
                Oproveedor.Descripcion = proveedor.Descripcion is null ? Oproveedor.Descripcion : proveedor.Descripcion;

                _DBLaSurtidora.Proveedores.Update(Oproveedor);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Proveedor Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Proveedore proveedor)
        {
            Proveedore Oproveedor = _DBLaSurtidora.Proveedores.Find(proveedor.IdProveedor);
            if (Oproveedor == null)
            {
                return BadRequest("Proveedor no encontrado");
            }
            try
            {
                Oproveedor.Estado = proveedor.Estado;
                _DBLaSurtidora.Proveedores.Update(Oproveedor);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Proveedor cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }




        //[HttpDelete]
        //[Route("Eliminar/{IdProveedor:int}")]

        //public IActionResult Eliminar(int IdProveedor)
        //{
        //    Proveedore Oproveedor = _DBLaSurtidora.Proveedores.Find(IdProveedor);

        //    if (Oproveedor == null)
        //    {
        //        return BadRequest("Proveedor no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.Proveedores.Remove(Oproveedor);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Proveedor eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //    }

        //}
    }
}
