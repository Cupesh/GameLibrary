using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Shared.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
