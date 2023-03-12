using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class PartProjectConnection
    {
        public int ProjectId { get; set; }
        public string PartName { get; set; }
        public int NumberReserved { get; set; }
        public int PartStateId { get; set; }

        #region DictionaryConverter
        public PartProjectConnection(Dictionary<string, string> values)
        {
            ProjectId = Convert.ToInt32(values["ProjectId"]);
            PartName = values["PartName"];
            NumberReserved = Convert.ToInt32(values["NumberReserved"]);
            PartStateId = Convert.ToInt32(values["PartStateId"]);
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "ProjectId", ProjectId.ToString() },
                { "PartName", PartName },
                { "NumberReserved", NumberReserved.ToString() },
                { "PartStateId", PartStateId.ToString() }
            };

            return values;
        }
        #endregion
    }
}
