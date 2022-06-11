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

        public async Task<List<Game>> Populate(Game game)
        {
            var sql = $" BEGIN IF NOT EXISTS (SELECT * FROM tblPSXGamesPAL WHERE SerialNumber = @serialNumber AND Barcode = @barcode) BEGIN INSERT INTO tblPSXGamesPAL (OfficialTitle, CommonTitle, SerialNumber, Region, GenreStyle, Developer, Publisher, DateReleased, Barcode, Language, GameDescription, NumberOfPlayers, Vibration, MultiTapFunction, LinkCableFunction) VALUES (@officialTitle, @commonTitle, @serialNumber, @region, @genreStyle, @developer, @publisher, @dateReleased, @barcode, @language, @gameDescription, @numberOfPlayers, @vibration, @multiTapFunction, @linkCableFunction) END END";
            var pars = new {
                               officialTitle = game.OfficialTitle,
                               commonTitle = game.CommonTitle,
                               serialNumber = game.SerialNumber,
                               region = game.Region,
                               genreStyle = game.GenreStyle,
                               developer = game.Developer,
                               publisher = game.Publisher,
                               dateReleased = game.DateReleased,
                               barcode = game.Barcode,
                               language = game.Language,
                               gameDescription = game.GameDescription,
                               numberOfPlayers = game.NumberOfPlayers,
                               vibration = game.Vibration,
                               multiTapFunction = game.MultiTapFunction,
                               linkCableFunction = game.LinkCableFunction
                           };

            var data = await _repository.GetDapperListAsync<Game>(sql, pars, CommandType.Text);

            return data;
        }
    }
}
