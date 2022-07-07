using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Client.Models;
using GameLibrary.Shared.Models;

namespace GameLibrary.Client.Services
{
    internal class DataService : IDataService
    {
        private readonly ServerAPIClient _client;

        public DataService(ServerAPIClient serverClient)
        {
            _client = serverClient;
        }

        public async Task<ApiResponse<List<User>>> Test()
        {
            string url = $"Test/Test";
            return await _client.GetDataAsync<List<User>>(url);
        }

        #region User Management
        
        public async Task<ApiResponse<bool>> CheckUserNameUniqueness(string userName)
        {
            string url = $"UserManagement/CheckUserNameUniqueness?userName={userName}";
            var result = await _client.GetDataAsync<bool>(url);
            return result;
        }

        public async Task<ApiResponse<User>> CreateUser(User newUser)
        {
            string url = $"UserManagement/CreateUser";
            return await _client.PostDataAsync<User>(url, newUser);
        }

        public async Task<ApiResponse<User>> Login(User user)
        {
            string url = $"UserManagement/Login";
            return await _client.PostDataAsync<User>(url, user);
        }

        #endregion
    }
}
