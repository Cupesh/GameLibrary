using System.Data;
using GameLibrary.Server.Data.Contexts;
using GameLibrary.Server.Models;

namespace GameLibrary.Server.Data.Repositories
{
    public class DataGameLibrary : IDataGameLibrary
    {
        private readonly IRepository<GameLibraryDataContext> _repository;

        public DataGameLibrary(GameLibraryDataContext context) => _repository = new Repository<GameLibraryDataContext>(context);

        public async Task<List<User>> Test()
        {
            var sql = "SELECT UserName FROM tblUsers";
            var data = await _repository.GetDapperListAsync<User>(sql, null, CommandType.Text);

            return data;
        }
    }
}
