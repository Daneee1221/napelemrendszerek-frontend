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
    /// Interaction logic for Raktarvezeto_PartManagementPage.xaml
    /// </summary>
    public partial class Raktarvezeto_PartManagementPage : Page
    {
        public Raktarvezeto_PartManagementPage()
        {
            InitializeComponent();
            List<Part> parts = ((MainWindow)Application.Current.MainWindow).StartGetPartsProcess();
            LB_Parts.DataContext = parts;
        }
        private void LB_Parts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SP_ModifyPartData.DataContext = LB_Parts.SelectedItem;
        }
    }
}
