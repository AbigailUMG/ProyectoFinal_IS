using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using BackendApi.Models;


namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaController : ControllerBase
    {
        private readonly LaSurtidoraBuenPrecioIsContext _DBLaSurtidora;

        public MedidaController(LaSurtidoraBuenPrecioIsContext _LaSurtidora)
        {
            _DBLaSurtidora = _LaSurtidora;

        }
    }
}
