using FrescuraApi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FrescuraApi.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosCotroller:ControllerBase{  
        private readonly FreshContext _context;
        public UsuariosCotroller(FreshContext context) => _context = context;
    }
}