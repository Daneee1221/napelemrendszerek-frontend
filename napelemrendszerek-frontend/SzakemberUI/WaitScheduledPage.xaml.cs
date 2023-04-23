using napelemrendszerek_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public class ReservedPart
    {
        public string PartName { get; set; }
        public int Available { get; set; }
        public int Missing { get; set; }

        public ReservedPart() { }
        
    }


    /// <summary>
    /// Interaction logic for SzakemberProjektekWaitScheduled.xaml
    /// </summary>
    /// 
    public partial class WaitScheduledPage : Page
    {
        private MainWindow mainWindow;
        private List<ReservedPart> parts;
        private ProjectsMainPage parentPage;
        private SolidColorBrush errorInputBackground;
        private int projectID;
        private int projectStateID;


        public WaitScheduledPage(int projectID, int ProjectStateId, ProjectsMainPage Parent)
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));
            this.parentPage = Parent;
            this.projectID = projectID;
            this.projectStateID = ProjectStateId;
            parts = new List<ReservedPart>();
            if(ProjectStateId == 3)
            {
                TB_workFee.IsEnabled = true;
                L_status.Content = "Waiting";
            }
            else
            {
                L_status.Content = "Scheduled";
            }
            loadPartList();
        }

        private async void loadPartList()
        {

            Project response = await mainWindow.GetSingleProject(projectID);
            L_estimatedTimeInDays.Content = response.EstimatedTimeInDays;
            if(projectStateID == 4)
            {
                TB_workFee.Text = response.WorkFee.ToString();
            }

            List<Dictionary<string,string>> responseList = await mainWindow.GetProjectParts(projectID);

            SP_partsList.Children.Remove(TB_Loading);

            foreach (Dictionary<string,string> item in responseList)
            {
                if(parts.Any(x => x.PartName == item["PartName"]))
                {
                    if (item["PartStateId"] == "1")
                    {
                        parts.Single(x => x.PartName == item["PartName"]).Available = Convert.ToInt32(item["NumberReserved"]);
                    }
                    else
                    {
                        parts.Single(x => x.PartName == item["PartName"]).Missing = Convert.ToInt32(item["NumberReserved"]);
                    }
                }
                else
                {
                    if (item["PartStateId"] == "1")
                    {
                        parts.Add(new ReservedPart()
                        {
                            PartName = item["PartName"],
                            Available = Convert.ToInt32(item["NumberReserved"])
                        });
                    }
                    else
                    {
                        parts.Add(new ReservedPart()
                        {
                            PartName = item["PartName"],
                            Missing = Convert.ToInt32(item["NumberReserved"])
                        });
                    }
                }
            }

            LB_alkatreszekLista.DataContext = parts;

          
        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {

            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);

        }

        private async void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {
            bool foundError = false;
            if(TB_workFee.Text == "")
            {
                TB_workFee.Background = errorInputBackground;
                TB_workFee.Text = "Kötelező!";
                foundError= true;
            }

            if(foundError)
            {
                return;
            }

            string response = await mainWindow.setWorkfeeAndEstimatedTime(projectID, null, TB_workFee.Text);

            _ = await mainWindow.ChangeProjectState(projectID, 3, 4);

            parentPage.refreshProjectsList();
        }

        private void TB_workFee_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_workFee.Background == errorInputBackground)
            {
                TB_workFee.Background = null;
                TB_workFee.Text = "";
            }
        }
    }

}
