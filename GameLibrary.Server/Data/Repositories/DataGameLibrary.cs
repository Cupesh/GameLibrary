﻿using System.Data;
using GameLibrary.Server.Data.Contexts;
using GameLibrary.Server.Models;
using GameLibrary.Shared.Models;

namespace GameLibrary.Server.Data.Repositories
{
    public class DataGameLibrary : IDataGameLibrary
    {
        private readonly IRepository<GameLibraryDataContext> _repository;

        public DataGameLibrary(GameLibraryDataContext context) => _repository = new Repository<GameLibraryDataContext>(context);

        #region User Management

        public async Task<bool> CheckUserNameUniqueness(string userName)
        {
            var sql = $"SELECT * FROM tblUsers WHERE UserName = @userName";
            var par = new { userName };
            var result = await _repository.GetDapperListAsync<User>(sql, par, CommandType.Text);

            if (result.Count > 0) { return false; }
            else { return true; }
        }

        public async Task<User> CreateUser(User newUser)
        {
            var sql = $"INSERT INTO tblUsers (UserName, Password, Region, EmailAddress) VALUES (@userName, @password, @region, @emailAddress) " +
                      $"SELECT * FROM tblUsers WHERE UserName = @userName";
            var pars = new { userName = newUser.UserName, password = newUser.Password, region = newUser.Region, emailAddress = newUser.EmailAddress };
            var result = await _repository.GetDapperListAsync<User>(sql, pars, CommandType.Text);

            return result.First();
        }

        public async Task<User> Login(User user)
        {
            var sql = $"SELECT * FROM tblUsers WHERE UserName = @userName";
            var par = new { userName = user.UserName };
            var result = await _repository.GetDapperListAsync<User>(sql, par, CommandType.Text);

            return result.First();
        }

        public async Task<bool> CheckEmailForPwdRecovery(string email)
        {
            var sql = $"SELECT EmailAddress FROM tblUsers WHERE EmailAddress = @email";
            var par = new { email = email };
            var result = await _repository.GetDapperListAsync<string>(sql, par, CommandType.Text);

            string? emailAddress = result.FirstOrDefault();
            if(String.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        #endregion

        #region Game queries

        public async Task<List<PSXGame>> FindGamesBySearchText(string searchText)
        {
            var sql = $"SELECT * FROM tblPSXGamesPAL WHERE Language = 'English' AND (OfficialTitle LIKE '%' + @searchText + '%' OR CommonTitle LIKE '%' + @searchText +'%')";
            var par = new { searchText = searchText };
            var result = await _repository.GetDapperListAsync<PSXGame>(sql, par, CommandType.Text);

            return result;
        }

        #endregion

        public async Task<List<PSXGame>> Populate(PSXGame game)
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

            var data = await _repository.GetDapperListAsync<PSXGame>(sql, pars, CommandType.Text);

            return data;
        }
    }
}
