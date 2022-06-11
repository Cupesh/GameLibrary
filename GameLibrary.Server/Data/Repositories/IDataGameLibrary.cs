using GameLibrary.Server.Models;

namespace GameLibrary.Server.Data.Repositories
{
    public interface IDataGameLibrary
    {
        Task<List<User>> Test();
    }
}
