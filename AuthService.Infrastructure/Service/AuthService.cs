using AuthService.Core.Configurations;
using AuthService.Core.Interfaces;
using AuthService.Core.PasswordService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payroll.Common.DatabaseContext;
using Payroll.Common.DTOs;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly DbContextPayrollProject _context;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;
        private readonly IPasswordService _passwordService;

        public AuthService(
            DbContextPayrollProject context,
            IOptions<JwtSettings> jwtSettings,
            ILogger<AuthService> logger,
            IPasswordService passwordService)

            

        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _passwordService = passwordService;
        }

        public async Task<ApiResult<UserLoginResponse>> LoginAsync(LoginRequest request)
        {
            var response = new ApiResult<UserLoginResponse>();

            try
            {
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);

                if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
                {
                    response.ResponseCode = 2;
                    response.Message = "Invalid username or password";
                    return response;
                }

                user.LastLogin = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                var token = GenerateJwtToken(user);

                response.ResponseCode = 1;
                response.Message = "Login successful";
                response.ResponseData.Add(new UserLoginResponse
                {
                    UserID = user.UserId,
                    Username = user.Username,
                    RoleName = user.Role.RoleName,
                    Token = token
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                response.ResponseCode = 0;
                response.Message = "An error occurred during login";
                response.ErrorDesc = ex.ToString();
                return response;
            }
        }


        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ApiResult<object>> RegisterAsync(RegisterRequest request)
        {
            var response = new ApiResult<object>();

            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    response.ResponseCode = 2;
                    response.Message = "Username already exists";
                    return response;
                }

                var hashedPassword = _passwordService.HashPassword(request.Password);

                var newUser = new User
                {
                    Username = request.Username,
                    PasswordHash = hashedPassword,
                    RoleId = request.RoleId,
                    EmployeeId = request.EmployeeId,
                    CreatedBy = request.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true,
                    RecordStatus = true
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var registeredUserData = new
                {
                    newUser.UserId,
                    newUser.Username,
                    newUser.PasswordHash,
                    newUser.RoleId,
                    newUser.EmployeeId,
                    newUser.CreatedBy,
                    newUser.CreatedOn
                };

                response.ResponseCode = 1;
                response.Message = "User registered successfully";
                response.ResponseData.Add(registeredUserData);

                return response;
            }
            catch (DbUpdateException dbEx)
            {
                response.ResponseCode = 0;
                response.Message = "Database update failed";
                response.ErrorDesc = dbEx.InnerException?.Message ?? dbEx.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 0;
                response.Message = "An error occurred during registration";
                response.ErrorDesc = ex.ToString();
                return response;
            }
        }


    }
}
