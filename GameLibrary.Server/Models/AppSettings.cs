﻿namespace GameLibrary.Server.Models
{
    public class AppSettings
    {
        public GeneralSettings General { get; set; }
    }

    public class GeneralSettings
    {
        public string UserName { get; set; }
    }
}
