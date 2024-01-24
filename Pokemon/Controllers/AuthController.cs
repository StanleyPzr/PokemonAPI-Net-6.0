using Microsoft.AspNetCore.Mvc;
using Pokemon.DTO;
using Pokemon.Helpers;
using Pokemon.Interfaces;
using Pokemon.Models.Auth;

namespace Pokemon.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : Controller

    {
        private readonly IUserRepository _repository;
        private readonly JwtServices _jwtService;

        public AuthController(IUserRepository repository, JwtServices jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar(RegistroDto registro)
        {
            var usuario = new Usuario
            {
                Nombre = registro.Nombre,
                Email = registro.Email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(registro.Contraseña)
            };

            _repository.CreateUsuario(usuario);

            return Ok("Success");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto login)
        {
            var usuario = _repository.GetByEmail(login.Email);
            if (usuario == null)
            {
                return BadRequest("Credenciales(user) incorrectas");
            }
            if (!BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña))
            {
                return BadRequest("Credenciales(pass) incorrectas");
            }

            var jwt = _jwtService.Generar(usuario.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new{
                message = "Inicio de sesión exitoso"
            });

        }

        [HttpGet("User")]
        public IActionResult Usuario()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int IdUsuario = int.Parse(token.Issuer);

                var usuario = _repository.GetById(IdUsuario);

                return Ok(usuario);


            }
            catch (Exception ex)
            {               
                Console.WriteLine($"Error en Usuario: {ex.Message}");                
                return Unauthorized(new { error = "Error al procesar la solicitud", details = ex.Message });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions { 
                Path = "/",
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
