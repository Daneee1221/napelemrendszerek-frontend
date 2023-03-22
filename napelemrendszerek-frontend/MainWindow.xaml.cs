using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Comm;
using napelemrendszerek_backend.Models;

namespace napelemrendszerek_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Communication responseObject = new Communication();
        private int roleID = 0;

        private Process process;

        public MainWindow()
        {
            InitializeComponent();
            
            FR_mainFrame.Source = new Uri("./loginPage.xaml", UriKind.RelativeOrAbsolute);

            SocketClient.StartClient();
            process = new Process();
        }

        public async Task<string> StartLoginProcess(string username, string password)
        {
            responseObject = await process.Login(username, password);

            if (responseObject.Message == "successful")
            {
                roleID = (int)responseObject.RoleId;
                //load next page
                switch (roleID)
                {
                    case 1:
                        FR_mainFrame.Source = new Uri("./RaktarvezetoUI/RaktarvezetoBasePage.xaml", UriKind.RelativeOrAbsolute);
                        break;
                    default:
                        break;
                }
                return "";
            }
            else
            {
                return responseObject.Message;
            }
        }

        public async Task<List<Part>> StartGetPartsProcess()
        {
            List<Part> parts = new List<Part>();

            responseObject = await process.GetParts(roleID);

            foreach (Dictionary<string, string> pair in responseObject.Content)
            {
                parts.Add(new Part(pair));
            }

            return parts;
        }

        public async Task<string> StartAddPartProcess(Part newPart)
        {
            responseObject = await process.AddPart(newPart, roleID);

            return responseObject.Message;
        }

        public async Task<string> StartModifyPartProcess(Dictionary<string, string> newValues)
        {
            responseObject = await process.ModifyPart(newValues, roleID);

            return responseObject.Message;
        }
    }
}
