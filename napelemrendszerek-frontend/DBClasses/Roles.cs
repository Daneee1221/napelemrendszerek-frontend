using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        #region DictionaryConverter
        public Roles(Dictionary<string, string> values)
        {
            RoleId = Convert.ToInt32(values["Username"]);
            RoleName = values["UserPassword"];
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "RoleId", RoleId.ToString() },
                { "RoleName", RoleName }
            };

            return values;
        }
        #endregion
    }
}
