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
        private SolidColorBrush errorInputBackground;

        public loginPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));
        }

        private async void BTN_logInButton_Click(object sender, RoutedEventArgs e)
        {
            TB_Response.Text = string.Empty;
            string username = TB_username.Text;
            string password = PB_password.Password;
      
            bool foundEmptyInput = false;
            if(username == "")
            {
                TB_username.Background = errorInputBackground;
                TB_Response.Text = "Mindkét mezőt kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if(password == "")
            {
                PB_password.Background = errorInputBackground;
                TB_Response.Text = "Mindkét mezőt kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if(foundEmptyInput)
            {
                return;
            }

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

        private void TB_GotFocus_Reset(object sender, RoutedEventArgs e)
        {
            if(!(sender is TextBox || sender is PasswordBox))
            {
                return;
            }

            TextBox TB;
            PasswordBox PB;

            if(sender is TextBox)
            {
                TB = sender as TextBox;
                if(TB.Background == errorInputBackground)
                {
                    TB.Text = "";
                    TB.Background = null;
                }

            }else
            {
                PB = sender as PasswordBox;
                if (PB.Background == errorInputBackground)
                {
                    PB.Password = "";
                    PB.Background = null;
                }
            }
        }
    }
}
