using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Users
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }

        #region DictionaryConverter
        public Users(Dictionary<string, string> values)
        {
            Username = values["Username"];
            UserPassword = values["UserPassword"];
            RoleId = Convert.ToInt32(values["RoleId"]);
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "Username", Username },
                { "UserPassword", UserPassword },
                { "RoleId", RoleId.ToString() }
            };

            return values;
        }
        #endregion
    }
}
