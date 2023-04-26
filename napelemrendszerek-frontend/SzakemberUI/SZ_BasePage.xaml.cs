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
    /// Interaction logic for Szakember.xaml
    /// </summary>
    public partial class SZ_BasePage : Page
    {
        private MainWindow mainWindow;

        public SZ_BasePage()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void Menu_NewCustomer(object sender, RoutedEventArgs e)
        {
            FR_SzakemberMainFrame.Source = new Uri("./AddNewProjectPage.xaml", UriKind.Relative);
        }

        private void Menu_Projects(object sender, RoutedEventArgs e)
        {
            FR_SzakemberMainFrame.Source = new Uri("./ProjectsMainPage.xaml", UriKind.Relative);
        }

        private void Menu_Logout_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Logout();
        }
    }
}
