using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Compartment
    {
        public int CompartmentId { get; set; }
        public int CompartmentRow { get; set; }
        public int CompartmentCulomn { get; set; }
        public int CompartmentCell { get; set; }
        public int StoredAmount { get; set; }
        public string StoredPartName { get; set; }

        #region DictionaryConverter
        public Compartment(Dictionary<string, string> values)
        {
            CompartmentId = Convert.ToInt32(values["CompartmentId"]);
            CompartmentRow = Convert.ToInt32(values["CompartmentRow"]);
            CompartmentCulomn = Convert.ToInt32(values["CompartmentCulomn"]);
            CompartmentCell = Convert.ToInt32(values["CompartmentCell"]);
            StoredAmount = Convert.ToInt32(values["StoredAmount"]);
            StoredPartName = values["StoredPartName"];
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "CompartmentId", CompartmentId.ToString() },
                { "CompartmentRow", CompartmentRow.ToString() },
                { "CompartmentCulomn", CompartmentCulomn.ToString() },
                { "CompartmentCell", CompartmentCell.ToString() },
                { "StoredAmount", StoredAmount.ToString() },
                { "StoredPartName", StoredPartName }
            };

            return values;
        }
        #endregion
    }
}
