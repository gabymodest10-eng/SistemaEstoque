using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
     


namespace SistemaEstoque.Controllers;

[ApiController]
[Route("[Controller]")]
public class LoginController : ControllerBase
    {
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
        {
        _configuration = configuration;
        }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest login)
        {
        if (login.Usuario != "GABRIELA" || login.Senha != "GABRIELA")
            {
            return Unauthorized();
            }
        var chave = _configuration["Token:Key"];
        var chaveSegura = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(chave!));

        var credenciais = new SigningCredentials(
           chaveSegura,
           SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[]
        {
                new Claim(ClaimTypes.Name, "GABRIELA")
                },
               expires: DateTime.UtcNow.AddHours(1),
               signingCredentials: credenciais);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenString);
        }
    }


