using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibrary.Server.Data.Contexts
{
    public class GameLibraryDataContext : DbContext
    {
        public GameLibraryDataContext(DbContextOptions<GameLibraryDataContext> options) : base(options) => Database.SetCommandTimeout(300);
    }
}
