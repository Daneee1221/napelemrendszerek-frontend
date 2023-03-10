using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class ProjectHistory
    {
        public int ProjectId { get; set; }
        public int OldProjectStateId { get; set; }
        public int NewProjectStateId { get; set; }
        public DateTime DateOfChange { get; set; }

        #region DictionaryConverter
        public ProjectHistory(Dictionary<string, string> values)
        {
            ProjectId = Convert.ToInt32(values["ProjectId"]);
            OldProjectStateId = Convert.ToInt32(values["OldProjectStateId"]);
            NewProjectStateId = Convert.ToInt32(values["NewProjectStateId"]);
            DateOfChange = Convert.ToDateTime(values["DateOfChange"]);
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "ProjectId", ProjectId.ToString() },
                { "OldProjectStateId", OldProjectStateId.ToString() },
                { "NewProjectStateId", NewProjectStateId.ToString() },
                { "DateOfChange", DateOfChange.ToString() }
            };

            return values;
        }
        #endregion
    }
}
