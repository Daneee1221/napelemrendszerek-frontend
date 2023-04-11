using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace napelemrendszerek_backend.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int ProjectStateId { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ProjectLocation { get; set; }
        public string ProjectDescription { get; set; }
        public int? WorkFee { get; set; }
        public int? EstimatedTimeInDays { get; set; }

        public Project(string customerName, string customerAddress, string customerPhone, string customerEmail, string projectLocation, string projectDescription, string createdBy)
        {
            ProjectStateId = 1;
            CreatedBy = createdBy;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerPhone = customerPhone;
            CustomerEmail = customerEmail;
            StartDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            ProjectLocation = projectLocation;
            ProjectDescription = projectDescription;
        }

        #region DictionaryConverter

        public Project(Dictionary<string, string> values)
        {
            ProjectId = Convert.ToInt32(values["ProjectId"]);
            ProjectStateId = Convert.ToInt32(values["ProjectStateId"]);
            CreatedBy = values["CreatedBy"];
            CustomerName = values["CustomerName"];
            CustomerAddress = values["CustomerAddress"];
            CustomerPhone = values["CustomerPhone"];
            CustomerEmail = values["CustomerEmail"];
            StartDate = Convert.ToDateTime(values["StartDate"]);
            LastModifiedDate = Convert.ToDateTime(values["LastModifiedDate"]);
            if (values["ClosedDate"] == "")
            {
                ClosedDate = null;
            }
            else
            {
                ClosedDate = Convert.ToDateTime(values["ClosedDate"]);
            }
            ProjectLocation = values["ProjectLocation"];
            ProjectDescription = values["ProjectDescription"];
            if (values["WorkFee"] == "")
            {
                WorkFee = null;
            }
            else
            {
                WorkFee = Convert.ToInt32(values["WorkFee"]);
            }

            if (values["EstimatedTimeInDays"] == "")
            {
                EstimatedTimeInDays = null;
            }
            else
            {
                EstimatedTimeInDays = Convert.ToInt32(values["EstimatedTimeInDays"]);
            }
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "ProjectId", ProjectId.ToString() },
                { "ProjectStateId", ProjectStateId.ToString() },
                { "CreatedBy", CreatedBy.ToString() },
                { "CustomerName", CustomerName.ToString() },
                { "CustomerAddress", CustomerAddress.ToString() },
                { "CustomerPhone", CustomerPhone.ToString() },
                { "CustomerEmail", CustomerEmail.ToString() },
                { "StartDate", StartDate.ToString() },
                { "LastModifiedDate", LastModifiedDate.ToString() },
                { "ClosedDate", ClosedDate.ToString() },
                { "ProjectLocation", ProjectLocation.ToString() },
                { "ProjectDescription", ProjectDescription.ToString() },
                { "WorkFee", WorkFee.ToString() },
                { "EstimatedTimeInDays", EstimatedTimeInDays.ToString() },
            };

            return values;
        }
        #endregion
    }
}
