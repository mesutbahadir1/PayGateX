using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayGateX.Dtos.Account;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var appUser = new AppUser
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName!,
                Email = registerDto.Email,
                Role = registerDto.Role!
            };
            var createUser = await _userManager.CreateAsync(appUser,registerDto.Password);

            if (createUser.Succeeded)
            {
                IdentityResult roleResult;
                if (appUser.Role=="Admin") {
                    roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                }else if (appUser.Role=="CustomerSupport"){
                    roleResult = await _userManager.AddToRoleAsync(appUser, "CustomerSupport");
                }else if (appUser.Role=="Manager"){
                    roleResult = await _userManager.AddToRoleAsync(appUser, "Manager");
                }
                else
                {
                    roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                }
               
                if (roleResult.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        UserName = appUser.UserName,
                        FullName = appUser.FullName,
                        Role = appUser.Role,
                        Email = appUser.Email,
                        Token =  _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createUser.Errors);
            }
          
        }
        catch (Exception e)
        {
             return StatusCode(500, e);
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
        if (user == null)
            return Unauthorized("User not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

        if (!result.Succeeded)
            return Unauthorized("User name not found or password incorrect");

        return Ok(new NewUserDto
        {
            UserName = user.UserName,
            FullName = user.FullName,
            Role = user.Role,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    } 
}