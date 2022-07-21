using GameLibrary.Shared.Models;

namespace GameLibrary.Server.Data.Repositories
{
    public interface IDataGameLibrary
    {
        #region User Management

        Task<User> CreateUser(User newUser);
        Task<bool> CheckUserNameUniqueness(string userName);
        Task<User> Login(User user);
        Task<bool> CheckEmailForPwdRecovery(string email);

        #endregion

        #region Game queries

        Task<List<PSXGame>> FindGamesBySearchText(string searchText);

        #endregion

        Task<List<PSXGame>> Populate(PSXGame game);
    }
}
