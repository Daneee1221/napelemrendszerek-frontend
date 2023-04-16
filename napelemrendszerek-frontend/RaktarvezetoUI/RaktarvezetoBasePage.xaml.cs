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
    public partial class RaktarvezetoBasePage : Page
    {
        private MainWindow mainWindow;

        private Image IMG_alert;

        public RaktarvezetoBasePage()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            CheckForUnaccocatedParts();
        }

        private async void CheckForUnaccocatedParts()
        {
            bool foundUnallocatedParts = await mainWindow.CheckForUnaccocatedParts();

            if (foundUnallocatedParts)
            {
                IMG_alert = new Image();
                IMG_alert.Source = new BitmapImage(new Uri("../img/alert.png", UriKind.Relative));
                IMG_alert.Height = 30;
                SP_StoreNewParts.Children.Add(IMG_alert);
            }
        }

        private void Menu_ManageParts_Click(object sender, RoutedEventArgs e)
        {
            //CheckForUnaccocatedParts(); //Meg kéne várni az eredményt, de voidra nincs await
            FR_RaktarosMainFrame.Source = new Uri("./Raktarvezeto_PartManagementPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Menu_StoreNewParts_Click(object sender, RoutedEventArgs e)
        {
            FR_RaktarosMainFrame.Source = new Uri("./Raktarvezeto_StoreNewPartsPage.xaml", UriKind.RelativeOrAbsolute);
            SP_StoreNewParts.Children.Remove(IMG_alert);
        }
    }
}
