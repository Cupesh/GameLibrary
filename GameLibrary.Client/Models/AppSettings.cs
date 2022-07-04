using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Client.Models
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
