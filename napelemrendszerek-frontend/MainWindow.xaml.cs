using AsynchronousClient;
using ClientProcess;
using Communication;

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

namespace napelemrendszerek_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            FR_mainFrame.Source = new Uri("loginPage.xaml", UriKind.RelativeOrAbsolute);

            //Thread connThread = new Thread(StartClient);
            //connThread.Start();
        }

        private void StartClient()
        {
            SocketClient.StartClient();
            Process process = new Process();
            process.ReadAndWrite();
            SocketClient.Close();
        }
    }
}
