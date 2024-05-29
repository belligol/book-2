using BookStore_Models.Models.Responses;

namespace BookStore_BL.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string userName, string password, string email);
        Task<AuthenticationResult> LoginAsync(string userName, string password);
        Task LogOff();
    }
}
