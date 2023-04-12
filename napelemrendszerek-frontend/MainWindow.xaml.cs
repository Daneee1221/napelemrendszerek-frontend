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
        private int roleID = 0;

        private Process process;

        public MainWindow()
        {
            InitializeComponent();
            
            FR_mainFrame.Source = new Uri("./loginPage.xaml", UriKind.RelativeOrAbsolute);

            SocketClient.StartClient();
            process = new Process();
        }

        public async Task<string> Login(string username, string password)
        {
            Communication responseObject = await process.Login(username, password);

            if (responseObject.Message == "successful")
            {
                roleID = (int)responseObject.RoleId;
                //load next page
                switch (roleID)
                {
                    case 1:
                        FR_mainFrame.Source = new Uri("./RaktarvezetoUI/RaktarvezetoBasePage.xaml", UriKind.RelativeOrAbsolute);
                        break;
                    case 2:
                        FR_mainFrame.Source = new Uri("./RaktarosUI/RaktarosUI.xaml", UriKind.RelativeOrAbsolute);
                        break;
                    case 3:
                        FR_mainFrame.Source = new Uri("./SzakemberUI/Szakember.xaml", UriKind.RelativeOrAbsolute);
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

        public async Task<List<Part>> GetParts()
        {
            List<Part> parts = new List<Part>();

            Communication responseObject = await process.GetParts(roleID);

            foreach (Dictionary<string, string> pair in responseObject.Content)
            {
                parts.Add(new Part(pair));
            }

            return parts;
        }

        public async Task<string> AddPart(Part newPart)
        {
            Communication responseObject = await process.AddPart(newPart, roleID);

            return responseObject.Message;
        }

        public async Task<string> ModifyPart(Dictionary<string, string> newValues)
        {
            Communication responseObject = await process.ModifyPart(newValues, roleID);

            return responseObject.Message;
        }

        public async Task<string> AddNewProject(Project newProject)
        {
            Communication responseObject = await process.AddNewProject(newProject, 3);

            return responseObject.Message;
        }
    }
}
