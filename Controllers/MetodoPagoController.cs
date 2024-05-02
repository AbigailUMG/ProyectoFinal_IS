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
    public class MetodoPagoController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public MetodoPagoController(LaSurtidoraBuenPrecioIsContext _laSurtidora)
        {
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]
        public IActionResult ListaMetodoPago()
        {
            List<MetodosPago> metodopago = new List<MetodosPago>();

            try
            {
                metodopago = _DBLaSurtidora.MetodosPagos.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = metodopago });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = metodopago });
            }

        }
        
        [HttpGet]
        [Route("Lista-desactivados")]
        public IActionResult ListaMetodoPagoNulos()
        {
            List<MetodosPago> metodopago = new List<MetodosPago>();

            try
            {
                metodopago = _DBLaSurtidora.MetodosPagos.Where(m => m.Estado == false).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = metodopago });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = metodopago });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdMetodoPago:int}")]
        public IActionResult Obtener(int IdMetodoPago)
        {
            MetodosPago metodopago = _DBLaSurtidora.MetodosPagos.Find(IdMetodoPago);

            if (metodopago == null)
            {
                return BadRequest("Metodo de pago no encontrado");
            }

            try
            {
                metodopago = _DBLaSurtidora.MetodosPagos.Where(p => p.IdMetodoPago == IdMetodoPago).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "ok", response = metodopago });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = metodopago });
            }
        }




        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] MetodosPago metodopago)
        {

            try
            {
                 metodopago.Estado = true;
                _DBLaSurtidora.MetodosPagos.Add(metodopago);
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
        public IActionResult Editar([FromBody] MetodosPago metodopago)
        {

            MetodosPago Ometodopago = _DBLaSurtidora.MetodosPagos.Find(metodopago.IdMetodoPago);

            if (Ometodopago == null)
            {
                return BadRequest("Categoria no encontrado");
            }
            try
            {

                Ometodopago.Nombre = Ometodopago.Nombre is null ? Ometodopago.Nombre : metodopago.Nombre;
                Ometodopago.Descripcion = Ometodopago.Descripcion is null ? Ometodopago.Descripcion : metodopago.Descripcion;

                _DBLaSurtidora.MetodosPagos.Update(Ometodopago);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Metodo de Pago Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(MetodosPago metodopago)
        {
            MetodosPago Ometodopago = _DBLaSurtidora.MetodosPagos.Find(metodopago.IdMetodoPago);
            if (Ometodopago == null)
            {
                return BadRequest("Metodo de pago no encontrado");
            }
            try
            {
                Ometodopago.Estado = metodopago.Estado;
                _DBLaSurtidora.MetodosPagos.Update(Ometodopago);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Metodo de pago cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }

        // [HttpDelete]
        // [Route("Eliminar/{IdMetodoPago:int}")]

        // public IActionResult Eliminar(int IdMetodoPago)
        // {
        //     MetodosPago Ometodopago = _DBLaSurtidora.MetodosPagos.Find(IdMetodoPago);

        //     if (Ometodopago == null)
        //     {
        //         return BadRequest("Metodo de pago no encontrado");
        //     }

        //     try
        //     {
        //         _DBLaSurtidora.MetodosPagos.Remove(Ometodopago);
        //         _DBLaSurtidora.SaveChanges();


        //         return StatusCode(StatusCodes.Status200OK, new { ok= true, mensaje = "Metodo de pago eliminado exitosamente" });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //     }

        // }




    }
}
