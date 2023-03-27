using napelemrendszerek_backend.Models;
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
    /// Interaction logic for SzakemberProjektUj.xaml
    /// </summary>
    public partial class SzakemberProjektUj : Page
    {
        public SzakemberProjektUj()
        {
            InitializeComponent();

            //List<Part> listPart = new List<Part>();
            //listPart.Add(new Part("Csavar", 10, 100, 5));
            //listPart.Add(new Part("Kábel", 20, 200, 15));

            //LB_alkatreszekLista.DataContext = listPart;
            //LB_projektAnyagokLista.DataContext = listPart;
        }

        private void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_megse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
