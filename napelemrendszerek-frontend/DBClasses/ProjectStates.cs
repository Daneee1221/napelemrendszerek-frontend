using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class ProjectStates
    {
        public int ProjectStateId { get; set; }
        public string ProjectStateName { get; set; }

        #region DictionaryConverter
        public ProjectStates(Dictionary<string, string> values)
        {
            ProjectStateId = Convert.ToInt32(values["ProjectStateId"]);
            ProjectStateName = values["ProjectStateName"];
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "ProjectStateId", ProjectStateId.ToString() },
                { "ProjectStateName", ProjectStateName }
            };

            return values;
        }
        #endregion
    }
}
