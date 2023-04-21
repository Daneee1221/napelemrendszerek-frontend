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
        private ProjectsMainPage parent;
        private int projectID;

        public WaitScheduledPage(int projectID, ProjectsMainPage Parent)
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            this.parent = Parent;
            this.projectID = projectID;
            parts = new List<ReservedPart>();
            loadPartList();
        }

        private async void loadPartList()
        {
            List<Dictionary<string,string>> responseList = await mainWindow.GetProjectParts(projectID);

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

        private void BTN_kesz_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
