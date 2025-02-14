﻿using Microsoft.AspNetCore.Mvc;
using AlbertCalculator.Dtos;
using AlbertCalculator.Service;

namespace AlbertCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignupUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthUserDto result = await _userService.SignupUserAsync(userDto);

            SetTokenCookies(result.RefreshToken);

            return Ok(new { Message = "User created", Data = result });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SinginUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthUserDto result = await _userService.SigninUserAsync(userDto);

            SetTokenCookies(result.RefreshToken);

            return Ok(new { Message = "User signed in", Data = result });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
             Response.Cookies.Delete("RefreshToken");

             return Ok(new { Message = "User logged out successfully" });
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<AuthUserDto>> RefreshUser()
        {
            string? refreshToken = Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token not found. Please login again.");
            }

            AuthUserDto result = await _userService.RefreshUserAsync(refreshToken);

            SetTokenCookies(result.RefreshToken);

            return Ok(result);
        }

        private void SetTokenCookies(string refreshToken)
        {
            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            });
        }
    }
}
