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
using System.Text.RegularExpressions;
using System.Windows.Markup.Localizer;

namespace napelemrendszerek_frontend
{
    /// <summary>
    /// Interaction logic for SzakemberProjektUj.xaml
    /// </summary>
    public partial class NewDraftPage : Page
    {
        private List<Part> parts;
        private List<Part> projectParts;
        private MainWindow mainWindow;
        private SolidColorBrush errorInputBackground;
        private bool allPartsAvailable = false;
        private int projectID;
        private int stateID;
        private ProjectsMainPage parentPage;

        public NewDraftPage(int projectID, int stateID, ProjectsMainPage parentpage )
        {
            InitializeComponent();

            this.projectID = projectID;
            this.stateID = stateID;
            parentPage = parentpage;
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));
            projectParts = new List<Part>();
            loadPartList();
            
        }

        private async void loadPartList()
        {
            if (stateID == 1)
            {
                SP_projectPartsList.Children.Remove(TB_Loading2);
            }

            parts = await mainWindow.GetParts();
            SP_partsList.Children.Remove(TB_Loading);
            LB_partsList.DataContext = parts;


            if (stateID == 2)
            {
                L_status.Content = "Draft";

                List<Dictionary<string, string>> responseList = await mainWindow.GetProjectParts(projectID);
              
                SP_projectPartsList.Children.Remove(TB_Loading2);

                foreach (Dictionary<string, string> item in responseList)
                {
                    if (projectParts.Any(x => x.PartName == item["PartName"]))
                    {
                        projectParts.Single(x => x.PartName == item["PartName"]).NumberReserved += Convert.ToInt32(item["NumberReserved"]);
                    }
                    else
                    {
                        projectParts.Add(parts.Single(x => x.PartName == item["PartName"]));
                        projectParts.Last().NumberReserved = Convert.ToInt32(item["NumberReserved"]);
                    }
                }
                LB_projectPartsList.DataContext = projectParts;

                checkForUnavailablePart();
            }

            BTN_lezaras.IsEnabled= true;
            BTN_mentes.IsEnabled= true;
            parentPage.ReEnableList();
        }

        private void checkForUnavailablePart()
        {
            allPartsAvailable = true;
            foreach (Part part in projectParts)
            {
                if(part.NumberAvailable < part.NumberReserved)
                {
                    allPartsAvailable = false;
                    break;
                }
            }

            if(allPartsAvailable)
            {
                TB_workfee.IsEnabled = true;
            }
            else
            {
                TB_workfee.IsEnabled = false;
            }
        }

        private async void BTN_lezaras_Click(object sender, RoutedEventArgs e)
        {

            bool foundError = false;
            if (projectParts.Count == 0)
            {
                LB_projectPartsList.Background = errorInputBackground;
                L_projectParts.Content = "A lista üres!";
                foundError = true;
            }

            if (string.IsNullOrEmpty(TB_workfee.Text) && TB_workfee.IsEnabled == true)
            {
                TB_workfee.Background = errorInputBackground;
                TB_workfee.Text = "Kötelező!";
                foundError = true;
            }

            if (string.IsNullOrEmpty(TB_estimatedTimeInDays.Text))
            {
                TB_estimatedTimeInDays.Background = errorInputBackground;
                TB_estimatedTimeInDays.Text = "Kötelező!";
                foundError = true;
            }

            if (foundError)
            {
                return;
            }

            BTN_lezaras.IsEnabled = false;
            BTN_mentes.IsEnabled = false;

            Dictionary<string, string> selectedPartsDict = new Dictionary<string, string>();
            foreach (Part item in projectParts)
            {
                selectedPartsDict.Add(item.PartName, item.NumberReserved.ToString());
            }
            string response = await mainWindow.addPartsToProject(projectID, "close", selectedPartsDict);

            if (response == "wait")
            {
                L_status.Content = "Wait";
            }

            if (response == "scheduled")
            {
                L_status.Content = "Scheduled";
            }

            string estimatedTimeInDays = TB_estimatedTimeInDays.Text;
            if (TB_workfee.IsEnabled == true)
            {
                string workFee = TB_workfee.Text;
                _ = await mainWindow.setWorkfeeAndEstimatedTime(projectID, estimatedTimeInDays, workFee);
            }
            else
            {
                _ = await mainWindow.setWorkfeeAndEstimatedTime(projectID, estimatedTimeInDays);
            }

            await parentPage.refreshProjectsList();
        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {

            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);

        }

        private void BTN_ClearModifyForm_Click(object sender, RoutedEventArgs e)
        {

            TB_estimatedTimeInDays.Text = "";
            TB_workfee.Text = "";

        }

        private void BTN_plusz_Click(object sender, RoutedEventArgs e)
        {
            L_status.Content = "Draft";
            Button btn = (Button)sender;
            ((Part)btn.DataContext).NumberReserved = 1;
            projectParts.Add((Part)btn.DataContext);

            parts.Remove((Part)btn.DataContext);

            LB_partsList.DataContext = null;
            LB_projectPartsList.DataContext = null;
            LB_partsList.DataContext = parts;
            LB_projectPartsList.DataContext = projectParts;

            LB_projectPartsList.Background= null;
            L_projectParts.Content = "Projekt anyagok";

            checkForUnavailablePart();

        }

        private void BTN_bin_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            parts.Add((Part)btn.DataContext);
            parts = parts.OrderBy(x => x.PartName).ToList();

            projectParts.Remove((Part)btn.DataContext);

            if(projectParts.Count == 0)
            {
                L_status.Content = "New";
            }

            LB_partsList.DataContext = null;
            LB_projectPartsList.DataContext = null;
            LB_partsList.DataContext = parts;
            LB_projectPartsList.DataContext = projectParts;

            checkForUnavailablePart();

        }

        private void TB_projectPartsNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkForUnavailablePart();
        }

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if(tb.Background == errorInputBackground)
            {
                tb.Background = null;
                tb.Text = "";
            }
        }

        private async void BTN_mentes_Click(object sender, RoutedEventArgs e)
        {
            if (projectParts.Count == 0)
            {
                LB_projectPartsList.Background = errorInputBackground;
                L_projectParts.Content = "A lista üres!";
                return;
            }
            BTN_mentes.IsEnabled = false;
            BTN_lezaras.IsEnabled = false;

            Dictionary<string, string> selectedPartsDict = new Dictionary<string, string>();
            foreach (Part item in projectParts)
            {
                selectedPartsDict.Add(item.PartName, item.NumberReserved.ToString());
            }
            _ = await mainWindow.addPartsToProject(projectID, "save", selectedPartsDict);

            await parentPage.refreshProjectsList();

        }
    }
}