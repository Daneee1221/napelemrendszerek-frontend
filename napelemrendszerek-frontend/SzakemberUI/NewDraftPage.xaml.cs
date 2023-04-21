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
        private Dictionary<string, string> selectedPartsDict;
        private int projectID;

        public NewDraftPage(int projectID)
        {
            InitializeComponent();

            this.projectID = projectID;
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));
            projectParts = new List<Part>();
            selectedPartsDict= new Dictionary<string, string>();
            loadPartList();
        }

        private async void loadPartList()
        {
            parts = await mainWindow.GetParts();
            LB_partsList.DataContext = parts;
        }

        private void checkForUnavailablePart()
        {
            allPartsAvailable = true;
            foreach (Part part in projectParts)
            {
                if(part.NumberAvailable < Convert.ToInt32(selectedPartsDict[part.PartName]))
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

        private async void BTN_kesz_Click(object sender, RoutedEventArgs e)
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

            string response = await mainWindow.addPartsToProject(projectID, selectedPartsDict);

            if (response == "wait")
            {
                L_status.Content = "Wait";
            }

            if (response == "scheduled")
            {
                L_status.Content = "Scheduled";
            }

            string estimatedTimeInDays = TB_estimatedTimeInDays.Text;
            if (TB_workfee.IsEnabled == false)
            {
                string workFee = TB_workfee.Text;
                _ = await mainWindow.setWorkfeeAndEstimatedTime(projectID, estimatedTimeInDays, workFee);
            }
            else
            {
                _ = await mainWindow.setWorkfeeAndEstimatedTime(projectID, estimatedTimeInDays);
            }


            //WaitScheduledPage secPage = new WaitScheduledPage(projectID);
            //NavigationService.Navigate(secPage);

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
            projectParts.Add((Part)btn.DataContext);
            selectedPartsDict[((Part)btn.DataContext).PartName] = "1";

            parts.Remove((Part)btn.DataContext);

            LB_partsList.DataContext = null;
            LB_projectPartsList.DataContext = null;
            LB_partsList.DataContext = parts;
            LB_projectPartsList.DataContext = projectParts;

            checkForUnavailablePart();

        }

        private void BTN_bin_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            parts.Add((Part)btn.DataContext);
            parts = parts.OrderBy(x => x.PartName).ToList();

            selectedPartsDict.Remove(((Part)btn.DataContext).PartName);
            projectParts.Remove((Part)btn.DataContext);

            if(projectParts.Count == 0)
            {
                L_status.Content = "Új";
            }

            LB_partsList.DataContext = null;
            LB_projectPartsList.DataContext = null;
            LB_partsList.DataContext = parts;
            LB_projectPartsList.DataContext = projectParts;

            checkForUnavailablePart();

        }

        private void TB_projectPartsNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            StackPanel parent = (sender as TextBox).Parent as StackPanel;
            selectedPartsDict[((Part)parent.DataContext).PartName] = (sender as TextBox).Text;

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
    }
}