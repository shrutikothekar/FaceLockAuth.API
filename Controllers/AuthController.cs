using FaceLockAuth.API.Data;
using FaceLockAuth.API.DTOs;
using FaceLockAuth.API.Models;
using FaceLockAuth.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FaceLockAuth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFaceStorageService _faceStorageService;
        private readonly IFaceAuthService _faceAuthService;
        private readonly IJwtTokenService _jwtTokenService;


        public AuthController(
            AppDbContext context,
            IFaceStorageService faceStorageService,
            IFaceAuthService faceAuthService,
            IJwtTokenService jwtTokenService)
        {
            _context = context;
            _faceStorageService = faceStorageService;
            _faceAuthService = faceAuthService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterFaceRequest request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
                return BadRequest("Email already registered");

            //var imagePath = await _faceStorageService
            //    .SaveBase64ImageAsync(request.FaceDescriptor);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                FaceDescriptor = request.FaceDescriptor, // ✅ directly save float[],
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }


        [HttpPost("login")]
        //public async Task<IActionResult> FaceLogin([FromForm] FaceLoginRequest request)
        public async Task<IActionResult> FaceLogin([FromBody] FaceLoginRequest request)
        {
            var user = await _context.Users
       .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("User not found");

            var isFaceValid = await _faceAuthService.VerifyFaceAsync(
                user.FaceDescriptor,
                request.FaceDescriptor);

            if (!isFaceValid)
                return Unauthorized("Face verification failed");

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                message = "Face login successful",
                token
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            var fullName = User.FindFirst("fullname")?.Value;

            return Ok(new
            {
                userId,
                email,
                fullName
            });
        }

    }

}
