using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Shared.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
    }
}
