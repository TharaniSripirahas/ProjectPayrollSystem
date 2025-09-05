using Payroll.Common.NonEntities;

namespace AuthService.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResult<UserLoginResponse>> LoginAsync(LoginRequest request);
        Task<ApiResult<object>> RegisterAsync(RegisterRequest request);
    }
}
