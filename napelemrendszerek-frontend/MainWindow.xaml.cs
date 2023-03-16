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

        public MainWindow()
        {
            InitializeComponent();
            
            FR_mainFrame.Source = new Uri("loginPage.xaml", UriKind.RelativeOrAbsolute);

            //Thread connThread = new Thread(StartClient);
            //connThread.Start();
        }

        public string StartLoginProcess(string username, string password)
        {
            Thread connThread = new Thread(() => Login(username, password));
            connThread.Start();
            connThread.Join();

            if(responseObject.Message == "successful")
            {
                roleID = (int)responseObject.RoleId;
                //load next page
                switch (roleID)
                {
                    case 1:
                        FR_mainFrame.Source = new Uri("RaktarvezetoBasePage.xaml", UriKind.RelativeOrAbsolute);
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

        public void Login(string username, string password)
        {
            SocketClient.StartClient();
            Process process = new Process();
            responseObject = process.Login(username, password);
            SocketClient.Close();
        }
    }
}
