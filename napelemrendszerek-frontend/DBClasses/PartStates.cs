using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class PartStates
    {
        public int PartStateId { get; set; }
        public string PartStateName { get; set; }

        #region DictionaryConverter
        public PartStates(Dictionary<string, string> values)
        {
            PartStateId = Convert.ToInt32(values["PartStateId"]);
            PartStateName = values["PartStateName"];
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "PartStateId", PartStateId.ToString() },
                { "PartStateName", PartStateName }
            };

            return values;
        }
        #endregion
    }
}
