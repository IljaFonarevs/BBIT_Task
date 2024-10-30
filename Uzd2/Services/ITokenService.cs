using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Uzd2.Datatypes;
using Uzd2.DTOs;


public interface ITokenSerivce
{
    Task<string> GenerateToken(ApplicationUser user);
    Task<string> Login(LoginDTO model);
    Task<string> Register(RegisterDTO model);

}

public class TokenService : ITokenSerivce
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),      
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("ApartmentNumber", user.apartNumber.ToString())
        };
        var roles = await _userManager.GetRolesAsync(user);
        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<String> Login([FromBody] LoginDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach (var claim in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, claim));
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return await GenerateToken(user);
        }
        return null;
    }
    
    public async Task<String> Register([FromBody] RegisterDTO model)
    {



        var user = new ApplicationUser { UserName = model.Username, Email = model.Email, apartNumber = model.ApartNumber };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (model.Username.Contains("admin")) await _userManager.AddToRoleAsync(user, "Admin");
        else await _userManager.AddToRoleAsync(user, "houseOwner");
        if (result.Succeeded)
        {
            return "User created successfully";
        }
        
        return result.ToString();
    }
}
