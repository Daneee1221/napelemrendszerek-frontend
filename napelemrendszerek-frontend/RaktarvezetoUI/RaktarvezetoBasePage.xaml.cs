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
        public RaktarvezetoBasePage()
        {
            InitializeComponent();
        }

        private void Menu_ManageParts_Click(object sender, RoutedEventArgs e)
        {
            FR_RaktarosMainFrame.Source = new Uri("./Raktarvezeto_PartManagementPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Menu_StoreNewParts_Click(object sender, RoutedEventArgs e)
        {
            FR_RaktarosMainFrame.Source = new Uri("./Raktarvezeto_StoreNewPartsPage.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
