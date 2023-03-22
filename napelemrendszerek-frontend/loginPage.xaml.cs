using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        private MainWindow mainWindow;
        public loginPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private async void BTN_logInButton_Click(object sender, RoutedEventArgs e)
        {
            TB_Response.Text = string.Empty;
            string username = TB_username.Text;
            string password = PB_password.Password;

            string response = await mainWindow.StartLoginProcess(username, password);

            switch (response)
            {
                case "nodata":
                    TB_Response.Text = "Nincs ilyen felhasználó!";
                    break;
                case "wrongpassword":
                    TB_Response.Text = "Hibás jelszó!";
                    break;
                default:
                    break;
            }
        }
    }
}
