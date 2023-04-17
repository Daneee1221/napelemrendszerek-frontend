using napelemrendszerek_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private List<Part> parts;
        private MainWindow mainWindow;

        public SzakemberProjektUj()
        {
            InitializeComponent();

            mainWindow = ((MainWindow)Application.Current.MainWindow);

            loadPartList();
        }

        private async void loadPartList()
        {
            parts = await mainWindow.GetParts();
            LB_partsList.DataContext = parts;
        }

        private void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_ClearModifyForm_Click(object sender, RoutedEventArgs e)
        {
            TB_estimatedTimeInDays.Text = "";
            TB_workfee.Text = "";
        }
    }
}