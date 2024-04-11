using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;


namespace BackendApi.Controllers
{
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor al guardar credenciales" });
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







    }

}
