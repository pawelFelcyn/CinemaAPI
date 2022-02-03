using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody]RegisterDto dto)
    {
        _service.Register(dto);

        return Ok();
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody]LoginDto dto)
    {
        var token = _service.GetJwtToken(dto);

        return Ok(token);
    }
}
