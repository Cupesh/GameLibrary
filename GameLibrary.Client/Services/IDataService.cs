using GameLibrary.Client.Models;
using GameLibrary.Shared.Models;

namespace GameLibrary.Client.Services
{
    public interface IDataService
    {
        #region User Management

        Task<ApiResponse<bool>> CheckUserNameUniqueness(string userName);
        Task<ApiResponse<User>> CreateUser(User newUser);
        Task<ApiResponse<User>> Login(User user);
        Task<ApiResponse<bool>> SendPwdRecoveryEmail(string email);

        #endregion
    }
}
