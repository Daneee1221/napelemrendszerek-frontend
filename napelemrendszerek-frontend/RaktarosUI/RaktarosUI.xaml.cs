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
        private List<PartProjectConnection> projectParts;

        public RaktarosUI()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            projectParts = new List<PartProjectConnection>();

            _ = LoadProjects();
        }

        private async Task LoadProjects()
        {
            projects = await mainWindow.GetProjects(true);
            LB_projektLista.DataContext = projects;
        }

        private void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LB_projektLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_projektLista.SelectedIndex == -1)
            {
                return;
            }

            LB_projektLista.IsEnabled = false;
            BTN_kesz.IsEnabled = false;
            BTN_Kilepes.IsEnabled = false;
            
            int projectID = (LB_projektLista.SelectedItem as Project).ProjectId;

            List<Dictionary<string, string>> responseList = await mainWindow.GetProjectParts(projectID);

            foreach (Dictionary<string, string> item in responseList)
            {
                if (projectParts.Any(x => x.PartName == item["PartName"]))
                {
                    projectParts.Single(x => x.PartName == item["PartName"]).NumberReserved += Convert.ToInt32(item["NumberReserved"]);
                }
                else
                {
                    projectParts.Add(new PartProjectConnection(item));
                    projectParts.Last().NumberReserved = Convert.ToInt32(item["NumberReserved"]);
                }
            }
            LB_alkatreszekLista.DataContext = projectParts;

            LB_projektLista.IsEnabled = true;
            BTN_kesz.IsEnabled = true;
            BTN_Kilepes.IsEnabled = true;
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