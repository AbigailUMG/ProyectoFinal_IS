using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Controllers
{
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
        [Route("Lista")]


        public IActionResult ListaMetodoPago()
        {
            List<MetodosPago> metodopago = new List<MetodosPago>();

            try
            {
                metodopago = _DBLaSurtidora.MetodosPagos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Dato correcto", response = metodopago });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = metodopago });
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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = metodopago });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message, response = metodopago });
            }
        }




        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] MetodosPago metodopago)
        {

            try
            {
                _DBLaSurtidora.MetodosPagos.Add(metodopago);
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


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Metodo de Pago Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{IdMetodoPago:int}")]

        public IActionResult Eliminar(int IdMetodoPago)
        {
            MetodosPago Ometodopago = _DBLaSurtidora.MetodosPagos.Find(IdMetodoPago);

            if (Ometodopago == null)
            {
                return BadRequest("Metodo de pago no encontrado");
            }

            try
            {
                _DBLaSurtidora.MetodosPagos.Remove(Ometodopago);
                _DBLaSurtidora.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Metodo de pago eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });
            }

        }




    }
}
