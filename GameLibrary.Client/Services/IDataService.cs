using GameLibrary.Client.Models;
using GameLibrary.Shared.Models;

namespace GameLibrary.Client.Services
{
    public interface IDataService
    {
        Task<ApiResponse<List<User>>> Test();

        #region User Management

        Task<ApiResponse<bool>> CheckUserNameUniqueness(string userName);
        Task<ApiResponse<User>> CreateUser(User newUser);

        #endregion
    }
}
