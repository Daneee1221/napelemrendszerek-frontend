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
    public class OrderPart
    {
        public string PartName { get; set; }
        public int Amount { get; set; }

        private int numberOrdered;
        public int NumberOrdered
        {
            get { return numberOrdered; }
            set 
            {
                if (value < 1)
                {
                    return;
                }
                numberOrdered = value;
            }
        }


        public OrderPart(string partName, int amount)
        {
            PartName = partName;
            Amount = amount;
            NumberOrdered = 0;
        }
    }

    /// <summary>
    /// Interaction logic for OrderMissingPartsPage.xaml
    /// </summary>
    public partial class OrderMissingPartsPage : Page
    {
        private List<OrderPart> neededParts;
        private List<OrderPart> orderedParts;
        private Dictionary<string, int> arrivedParts;
        private MainWindow mainWindow;
        private RV_BasePage parentPage;

        public OrderMissingPartsPage(RV_BasePage parent)
        {
            InitializeComponent();

            this.parentPage = parent;
            mainWindow = (MainWindow)Application.Current.MainWindow;
            neededParts = new List<OrderPart>();
            orderedParts = new List<OrderPart>();
            arrivedParts = new Dictionary<string, int>();

            _ = LoadNeededParts();
        }


        private async Task LoadNeededParts()
        {
            List<Dictionary<string, string>> responseList = await mainWindow.GetAllMissingParts();

            foreach (Dictionary<string, string> dict in responseList)
            {
                if (neededParts.Any(x => x.PartName == dict["PartName"]))
                {
                    neededParts.Single(x => x.PartName == dict["PartName"]).Amount += Convert.ToInt32(dict["NumberReserved"]);
                }
                else
                {
                    neededParts.Add(new OrderPart(dict["PartName"], Convert.ToInt32(dict["NumberReserved"])));
                }
            }

            LB_NeededParts.DataContext = neededParts;

            parentPage.ReEnableMenuBar();
        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {
            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);
        }

        private void BTN_plusz_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OrderPart selectedPart = (OrderPart)btn.DataContext;
            selectedPart.NumberOrdered = 1;
            orderedParts.Add(selectedPart);
            neededParts.Remove(selectedPart);

            LB_NeededParts.DataContext = null;
            LB_OrderedParts.DataContext = null;
            LB_NeededParts.DataContext = neededParts;
            LB_OrderedParts.DataContext = orderedParts;
        }

        private void BTN_bin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OrderPart selectedPart = (OrderPart)btn.DataContext;
            selectedPart.NumberOrdered = 0;
            neededParts.Add(selectedPart);
            orderedParts.Remove(selectedPart);

            neededParts.Sort((a, b) => a.PartName.CompareTo(b.PartName));

            LB_NeededParts.DataContext = null;
            LB_OrderedParts.DataContext = null;
            LB_NeededParts.DataContext = neededParts;
            LB_OrderedParts.DataContext = orderedParts;
        }

        private async void BTN_Order_Click(object sender, RoutedEventArgs e)
        {
            if (orderedParts.Count == 0)
            {
                return;
            }
            Dictionary<string, int> currentOrder = new Dictionary<string, int>();
            foreach (OrderPart part in orderedParts)
            {
                currentOrder[part.PartName] = part.NumberOrdered;
                if (arrivedParts.ContainsKey(part.PartName))
                {
                    arrivedParts[part.PartName] += part.NumberOrdered;
                }
                else
                {
                    arrivedParts.Add(part.PartName, part.NumberOrdered);
                }
                if (part.NumberOrdered < part.Amount)
                {
                    part.Amount -= part.NumberOrdered;
                    neededParts.Add(part);
                }
            }
            LB_ArrivedParts.DataContext = null;
            LB_ArrivedParts.DataContext = arrivedParts;

            orderedParts.Clear();
            LB_OrderedParts.DataContext = null;

            LB_NeededParts.DataContext = null;
            LB_NeededParts.DataContext = neededParts;

            string response  = await mainWindow.SendUnallocatedParts(currentOrder);
            if (response == "successful")
            {
                parentPage.SetAlertAfterOrder();
                currentOrder.Clear();
            }
        }
    }
}
