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
    /// Interaction logic for RaktarvezetoBasePage.xaml
    /// </summary>
    public partial class RV_BasePage : Page
    {
        private MainWindow mainWindow;

        private Image IMG_alert;

        public RV_BasePage()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            _ = CheckForUnaccocatedParts();
        }

        private async Task CheckForUnaccocatedParts()
        {
            MenuBar.IsEnabled = false;
            bool foundUnallocatedParts = await mainWindow.CheckForUnaccocatedParts();
            MenuBar.IsEnabled = true;

            if (foundUnallocatedParts && SP_StoreNewParts.Children.Contains(IMG_alert) == false)
            {
                IMG_alert = new Image();
                IMG_alert.Source = new BitmapImage(new Uri("../img/alert.png", UriKind.Relative));
                IMG_alert.Height = 30;
                SP_StoreNewParts.Children.Add(IMG_alert);
            }
        }

        public void SetAlertAfterOrder()
        {
            if (SP_StoreNewParts.Children.Contains(IMG_alert) == false)
            {
                IMG_alert = new Image();
                IMG_alert.Source = new BitmapImage(new Uri("../img/alert.png", UriKind.Relative));
                IMG_alert.Height = 30;
                SP_StoreNewParts.Children.Add(IMG_alert);
            }
        }

        private async void Menu_ManageParts_Click(object sender, RoutedEventArgs e)
        {
            await CheckForUnaccocatedParts();
            FR_RaktarosMainFrame.Source = new Uri("./PartManagementPage.xaml", UriKind.Relative);
        }

        private void Menu_StoreNewParts_Click(object sender, RoutedEventArgs e)
        {
            FR_RaktarosMainFrame.Source = new Uri("./StoreNewPartsPage.xaml", UriKind.Relative);
            SP_StoreNewParts.Children.Remove(IMG_alert);
        }

        private async void Menu_OrderParts_Click(object sender, RoutedEventArgs e)
        {
            await CheckForUnaccocatedParts();
            FR_RaktarosMainFrame.Navigate(new OrderMissingPartsPage(this));
        }
    }
}
