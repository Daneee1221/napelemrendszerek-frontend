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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace napelemrendszerek_frontend
{
    /// <summary>
    /// Interaction logic for SzakemberProjektInProgress.xaml
    /// </summary>
    public partial class SzakemberProjektInProgress : Page
    {
        private MainWindow mainWindow;

        public SzakemberProjektInProgress()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;

        }

        private void BTN_done_Click(object sender, RoutedEventArgs e)
        {
            BTN_unsuccessful.IsEnabled = false;
            BTN_done.IsEnabled = false;

            L_status.Content = "Sikeres projekt";

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("../img/verified.gif", UriKind.RelativeOrAbsolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(animatedGif, image);

        }

        private void BTN_unsuccessful_Click(object sender, RoutedEventArgs e)
        {
            BTN_unsuccessful.IsEnabled = false;
            BTN_done.IsEnabled = false;

            L_status.Content = "Sikertelen projekt";

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("../img/invalid.gif", UriKind.RelativeOrAbsolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(animatedGif, image);

        }

    }
}
