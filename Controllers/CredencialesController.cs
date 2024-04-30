using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using BackendApi.Services;
using Microsoft.AspNetCore.Cors;

// lOS LIBRERIAS PARA EL TOKEN
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BackendApi.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;
        
         // Obtenemos nuestro secret key
         private readonly string secretKey;

        public CredencialesController(LaSurtidoraBuenPrecioIsContext _laSurtidora, IConfiguration config)
        {
            //Obtenemos nuestra llave secreta que esta en appsettings.json
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _DBLaSurtidora = _laSurtidora;
        }

        [HttpGet]
        [Route("Lista-Activados")]


        public IActionResult ListaCredenciales()
        {
            List<Credenciale> credenciales = new List<Credenciale>();

            try
            {
                
                credenciales = _DBLaSurtidora.Credenciales.Where(m => m.Estado == true).ToList();

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = credenciales });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = credenciales });
            }

        }

        [HttpGet]
        [Route("Lista-desactivados")]
        public IActionResult ListaMarcaNulos()
        {
            List<Credenciale> Ocredencial = new List<Credenciale>();

            try
            {
                Ocredencial = _DBLaSurtidora.Credenciales.Where(m => m.Estado == false).ToList();
                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Dato correcto", response = Ocredencial });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Ocredencial });
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

                return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Datos enviados correctamente", response = Ocredencial });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message, response = Ocredencial });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Credenciale credenciales)
        {


            try
            {
                credenciales.Estado = true;
                string ContraEncrip = Encriptacion.EncripContra(credenciales.Password);
                credenciales.Password = ContraEncrip;


                _DBLaSurtidora.Credenciales.Add(credenciales);
                _DBLaSurtidora.SaveChanges();

                Console.WriteLine(credenciales);

                return StatusCode(StatusCodes.Status200OK, new { ok= true, mensaje = "Credenciales Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                // Agregar registros de depuración para imprimir la excepción interna
                Console.WriteLine("Error al guardar credenciales: " + ex.ToString());

                // Devolver un código de estado 500 para indicar un error interno del servidor
                //return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor al guardar credenciales" });
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = "Error al guardar los datos", detalle = ex.InnerException.Message });
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


                return StatusCode(StatusCodes.Status200OK, new { ok= true, mensaje = "Credenciales Editado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Estado")]
        public IActionResult Desactivar(Credenciale credencial)
        {
            Credenciale Ocredencial = _DBLaSurtidora.Credenciales.Find(credencial.IdCredenciales);
            if (Ocredencial == null)
            {
                return BadRequest("Credencial no encontrado");
            }
            try
            {
                Ocredencial.Estado = credencial.Estado;
                _DBLaSurtidora.Credenciales.Update(Ocredencial);
                _DBLaSurtidora.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credenciales cambio de estado Exitosamente" });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { ex.Message });
            }
        }




        //[HttpDelete]
        //[Route("Eliminar/{IdCredenciales:int}")]

        //public IActionResult Eliminar(int Idcredenciales)
        //{
        //    Credenciale Ocredencial = _DBLaSurtidora.Credenciales.Find(Idcredenciales);

        //    if (Ocredencial == null)
        //    {
        //        return BadRequest("Credencial no encontrado");
        //    }

        //    try
        //    {
        //        _DBLaSurtidora.Credenciales.Remove(Ocredencial);
        //        _DBLaSurtidora.SaveChanges();


        //        return StatusCode(StatusCodes.Status200OK, new { ok = true, mensaje = "Credencial eliminado exitosamente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { ok = false, mensaje = ex.Message });
        //    }

        //}

        [HttpPost]
 [Route("Validar")]
 public IActionResult Validar([FromBody] Credenciale validacion )
 {
    
     Console.WriteLine(validacion.Username);

    //  if (validacion.Username == null && validacion.Password == null)
    //  {
    //      return BadRequest("datos no encontrados");
    //  }

        if (validacion.Username == null && validacion.Password == null)
            {
                return BadRequest("datos no encontrados");
            }
     try
     {
         var credenciales = this._DBLaSurtidora.Credenciales.FirstOrDefault(c => c.Username == validacion.Username);
         var PassEntrada = Encriptacion.EncripContra(validacion.Password);



        //  if (credenciales!= null && credenciales.Password == PassEntrada)
        //  {
        //      return StatusCode(StatusCodes.Status200OK, new { mensaje = "Inicio de Sesion Correcto" });
        //  }
        //  else
        //  {
        //      return StatusCode(StatusCodes.Status200OK, new { mensaje = "Credenciales no existentes " });
        //  }

        
        
        
         if (credenciales != null && credenciales.Password == PassEntrada)
        {

            // if (credenciales.FkRol == null || (credenciales.FkRol != "administrador" && credenciales.FkRol != "vendedor"))
            // {
            //     return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "Acceso denegado: rol no autorizado" });
            // }




            // var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            // var claims = new ClaimsIdentity();

            // claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, validacion.Username));
            // claims.AddClaim(new Claim(ClaimTypes.Role, credenciales.rol));

        //     if (credenciales.FkRol == null || credenciales.FkRol != 5 && credenciales.FkRol != 2)
        //     {
        //         return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "Acceso denegado: rol no autorizado" });
        //     }

        //     // string rol;
        //     // if (credenciales.FkRol == 5)
        //     // {
        //     //     rol = "administrador";
        //     // }
        //     // else if (credenciales.FkRol == 2)
        //     // {
        //     //     rol = "Vendedor";
        //     // }
           

        //     var keyBytes = Encoding.ASCII.GetBytes(secretKey);
        //     var claims = new ClaimsIdentity();

        //     claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, validacion.Username));
        //     claims.AddClaim(new Claim(ClaimTypes.Role, credenciales.FkRol?.ToString()));  
              


        //     var TokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = claims,
        //         Expires = DateTime.UtcNow.AddMinutes(5),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        //     };

        //      //Hhacemos lectura del token
        //      var tokenHandler = new JwtSecurityTokenHandler();
        //      var tokenconfig = tokenHandler.CreateToken(TokenDescriptor);

        //     // Nuestro token creado

        //     string tokencreado = tokenHandler.WriteToken(tokenconfig);

        //     return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

        // Verificación de rol basada en números
           if (credenciales.FkRol == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "Acceso denegado: rol no autorizado" });
            }

            int rolAsignado;
            switch (credenciales.FkRol)
            {
                case 5:
                    rolAsignado = 5; // Administrador
                    break;
                case 2:
                    rolAsignado = 2; // Vendedor
                    break;
                default:
                    return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "Acceso denegado: rol no autorizado" });
            }

            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, validacion.Username));
            claims.AddClaim(new Claim(ClaimTypes.Role, rolAsignado.ToString()));

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            // Hacemos lectura del token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenconfig = tokenHandler.CreateToken(TokenDescriptor);

            // Nuestro token creado
            string tokencreado = tokenHandler.WriteToken(tokenconfig);

            return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

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
