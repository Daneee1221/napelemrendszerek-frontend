using napelemrendszerek_backend.Models;
using napelemrendszerek_frontend.RaktarvezetoUI;
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
        private List<CompartmentWithPart> compartments;
        private List<CompartmentWithPart> modifiedCompartments;

        public RaktarosUI()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            projectParts = new List<PartProjectConnection>();
            modifiedCompartments = new List<CompartmentWithPart>();

            _ = LoadProjectsAndCompartmants();
        }

        private async Task LoadProjectsAndCompartmants()
        {
            projects = await mainWindow.GetProjects(true);
            if (projects == null)
            {
                projects = new List<Project>();
            }

            LB_projektLista.DataContext = projects;

            List<Dictionary<string, string>> responseList = await mainWindow.GetCompartments();

            compartments = new List<CompartmentWithPart>();
            foreach (Dictionary<string, string> compartmentDict in responseList)
            {
                compartments.Add(new CompartmentWithPart(compartmentDict));
            }
        }

        private async void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {
            if (LB_projektLista.SelectedIndex == -1)
            {
                return;
            }

            LB_projektLista.IsEnabled = false;
            BTN_kesz.IsEnabled = false;

            int projectID = (LB_projektLista.SelectedItem as Project).ProjectId;
            string res = await mainWindow.StartProject(modifiedCompartments, projectID);

            if (res == "successful")
            {
                BTN_kesz.Foreground = new SolidColorBrush(Colors.Green);
                BTN_kesz.Content = "Sikeres!";
            }
            await Task.Delay(1500);
            BTN_kesz.IsEnabled = true;
            BTN_kesz.Foreground = new SolidColorBrush(Colors.Black);
            BTN_kesz.Content = "Kész";

            LB_projektLista.SelectedIndex = -1;

            LB_projektLista.IsEnabled = true;
            BTN_Kilepes.IsEnabled = true;
            BTN_utvonal.IsEnabled = true;
        }

        private async void LB_projektLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            projectParts.Clear();
            modifiedCompartments.Clear();
            LB_alkatreszekLista.DataContext = null;
            LB_boxLista.DataContext = null;
            BTN_kesz.IsEnabled = false;

            if (LB_projektLista.SelectedIndex == -1)
            {
                return;
            }

            LB_projektLista.IsEnabled = false;
            BTN_utvonal.IsEnabled = false;
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
            BTN_utvonal.IsEnabled = true;
            BTN_Kilepes.IsEnabled = true;
        }

        private void BTN_Kilepes_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Logout();
        }

        private void BTN_utvonal_Click(object sender, RoutedEventArgs e)
        {
            LB_projektLista.IsEnabled = false;
            BTN_utvonal.IsEnabled = false;
            BTN_Kilepes.IsEnabled = false;

            modifiedCompartments.Clear();
            foreach (PartProjectConnection part in projectParts)
            {
                int numNeeded = part.NumberReserved;
                do
                {
                    CompartmentWithPart comp = compartments.FirstOrDefault(x => x.PartName == part.PartName && x.StoredAmount > 0);
                    if (comp.StoredAmount >= numNeeded)
                    {
                        comp.StoredAmount -= numNeeded;
                        comp.NumTaken += numNeeded;
                        numNeeded = 0;
                    }
                    else
                    {
                        comp.NumTaken = comp.StoredAmount;
                        numNeeded -= comp.StoredAmount;
                        comp.StoredAmount = 0;
                    }
                    modifiedCompartments.Add(comp);

                } while (numNeeded > 0);

            }

            modifiedCompartments.Sort((x, y) => x.Id.CompareTo(y.Id));

            LB_boxLista.DataContext = null;
            LB_boxLista.DataContext = modifiedCompartments;

            LB_projektLista.IsEnabled = true;
            BTN_Kilepes.IsEnabled = true;
            BTN_kesz.IsEnabled = true;
        }
    }
}