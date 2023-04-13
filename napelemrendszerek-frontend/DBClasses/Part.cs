﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Part
    {
        public string PartName { get; set; }
        public int MaxNumberInBox { get; set; }
        public int SellPrice { get; set; }
        public int Unallocated { get; set; }

        public Part(string partName, int maxNumberInBox, int sellPrice, int unallocated)
        {
            PartName = partName;
            MaxNumberInBox = maxNumberInBox;
            SellPrice = sellPrice;
            Unallocated = unallocated;
        }

        #region DictionaryConverter
        public Part(Dictionary<string, string> values)
        {
            PartName = values["PartName"];
            MaxNumberInBox = Convert.ToInt32(values["MaxNumberInBox"]);
            SellPrice = Convert.ToInt32(values["SellPrice"]);
            Unallocated = Convert.ToInt32(values["Unallocated"]);
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "PartName", PartName },
                { "MaxNumberInBox", MaxNumberInBox.ToString() },
                { "SellPrice", SellPrice.ToString() },
                { "Unallocated", Unallocated.ToString() },
            };

            return values;
        }
        #endregion
    }
}
