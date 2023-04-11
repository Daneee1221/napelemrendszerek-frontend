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

namespace napelemrendszerek_frontend.RaktarvezetoUI
{
    /// <summary>
    /// Interaction logic for Raktarvezeto__StoreNewPartsPage.xaml
    /// </summary>
    public partial class Raktarvezeto__StoreNewPartsPage : Page
    {
        public Raktarvezeto__StoreNewPartsPage()
        {
            InitializeComponent();
        }

        private void RowSelectorBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        //Tesztkód!
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;
            panel.Background = new SolidColorBrush(Colors.DarkTurquoise);
        }
    }
}
