using CSharpFunctionalExtensions;
using HospitalManagementSystem.Application.Models;
using HospitalManagementSystem.Application.Models.Account;
using HospitalManagementSystem.Application.Models.User;


namespace HospitalManagementSystem.Application.Services.IServices
{
    public interface IAccountService
    {
        Task<Result<LoginResponseModel, DomainError>> LoginAsync(LoginModel model);
        Task<Result<RegisterResponseModel, DomainError>> RegisterAsync(RegisterModel model);
        Task<Result<string>> ForgetPasswordAsync(ForgotPasswordModel model);
        Task<Result<string, DomainError>> ResetPasswordAsync(ResetPasswordModel model);
        Task<Result<string, DomainError>> ConfirmEmailAysnc(ConfirmEmailModel model);
        Task<Result<TwoFactorAuthenticationResponseModel, DomainError>> EnableTwoFactorAuthenticationAsync(TwoFactorAuthenticationModel model);
        Task<Result<GetAuthenticatorKeyResponseModel, DomainError>> GetAuthenticatorTokenAysnc(GetAuthenticatorTokenModel model);
        Task SignOutAsync();
        Task<Result> GetTwoFactorAuthenticationUserAsync(GetTwoFactorAuthenticationUserModel model);
        Task<Result<VerifyAuthenticatorResponseModel>> TwoFactorAuthenticatorSignInAsync(VerifyAuthenticatorModel model);
        Task<Result<ProfileModel>> GetProfileDataAsync(string? UserName);
     }
}
