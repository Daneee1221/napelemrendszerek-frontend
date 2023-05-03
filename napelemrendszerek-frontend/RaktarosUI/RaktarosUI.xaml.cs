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
    /// Interaction logic for RaktarosUI.xaml
    /// </summary>
    public partial class RaktarosUI : Page
    {
        private MainWindow mainWindow;
        private List<Project> projects;

        public RaktarosUI()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            _ = LoadProjects();
        }

        private async Task LoadProjects()
        {
            projects = await mainWindow.GetProjects();
            LB_projektLista.DataContext = projects;
        }

        private void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LB_projektLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_projektLista.IsEnabled = false;
            BTN_kesz.IsEnabled = false;
            //Kilepes false
        }

        private void BTN_Kilepes_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Logout();
        }

        private void BTN_utvonal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}