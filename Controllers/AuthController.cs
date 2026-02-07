using FaceLockAuth.API.Data;
using FaceLockAuth.API.DTOs;
using FaceLockAuth.API.Models;
using FaceLockAuth.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Register([FromForm] RegisterFaceRequest request)
        {
            var imagePath = await _faceStorageService.SaveFaceImageAsync(request.FaceImage);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                FaceImagePath = imagePath,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> FaceLogin([FromForm] FaceLoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("User not found");

            var isFaceValid = await _faceAuthService.VerifyFaceAsync(
                user.FaceImagePath,
                request.FaceImage);

            if (!isFaceValid)
                return Unauthorized("Face verification failed");

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                message = "Face login successful",
                token
            });
            //return Ok(new { message = "Face login successful" });
        }
    }

}
