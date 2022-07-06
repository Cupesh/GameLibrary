using GameLibrary.Server.Models;
using GameLibrary.Shared.Models;

namespace GameLibrary.Server.Data.Repositories
{
    public interface IDataGameLibrary
    {
        #region User Management

        Task<User> CreateUser(User newUser);
        Task<bool> CheckUserNameUniqueness(string userName);
        Task<User> Login(User user);

        #endregion

        Task<List<Game>> Populate(Game game);
    }
}
