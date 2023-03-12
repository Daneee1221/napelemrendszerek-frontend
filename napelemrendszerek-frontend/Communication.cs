using napelemrendszerek_backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Comm
{
    [Serializable]
    public class Communication
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public List<Dictionary<string, string>> Content { get; set; }
        public int? RoleId { get; set; }

        public Communication()
        {
            Message = String.Empty;
            Content = new List<Dictionary<string, string>>();
            Date = DateTime.Now;
        }
        public Communication(string message, List<Dictionary<string, string>> content, int roleId)
        {
            this.Message = message;
            this.Date = DateTime.Now;
            this.Content = content;
            this.RoleId = roleId;
        }

        public void addItemToContent(Dictionary<string, string> newElement)
        {
            Content.Add(newElement);
        }

        public override string ToString()
        {
            return Message + "[" + Date.ToString() + "]";
        }
    }
    
}
