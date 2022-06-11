using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Client.Models;

namespace GameLibrary.Client.Services
{
    public interface IDataService
    {
        Task<ApiResponse<List<User>>> Test();
    }
}
