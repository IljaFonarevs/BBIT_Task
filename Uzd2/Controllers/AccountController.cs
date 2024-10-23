using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uzd2.Datatypes;
using Uzd2.DTOs;
using Uzd2.Services;

[ApiController]
[Route("api/Account")]
public class AccountController : ControllerBase
{
    
    private readonly ITokenSerivce _tokenService;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenSerivce tokenService)
    {
        
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var result = await _tokenService.Login(model);
        if (result == null) return BadRequest();
        else return Ok(result);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {



        var result = await _tokenService.Register(model);
        if (result != "User created successfully") return BadRequest(result);
        else return Ok(result);
    }
    


}