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

        public OrderPart(string partName, int amount)
        {
            PartName = partName;
            Amount = amount;
        }
    }

    /// <summary>
    /// Interaction logic for OrderMissingPartsPage.xaml
    /// </summary>
    public partial class OrderMissingPartsPage : Page
    {
        private List<OrderPart> neededParts;
        private List<OrderPart> orderedParts;
        private List<OrderPart> arrivedParts;
        private MainWindow mainWindow;

        public OrderMissingPartsPage()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;
            neededParts = new List<OrderPart>();
            orderedParts = new List<OrderPart>();
            arrivedParts = new List<OrderPart>();

            _ = LoadNeededParts();
        }


        private async Task LoadNeededParts()
        {
            List<Dictionary<string, string>> responseList = await mainWindow.GetAllMissingParts();

            foreach (Dictionary<string, string> dict in responseList)
            {
                neededParts.Add(new OrderPart(dict["PartName"], Convert.ToInt32(dict["NumberReserved"])));
            }

            LB_NeededParts.DataContext = neededParts;
        }

        private void TB_OrderAmount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {
            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);
        }

        private void BTN_plusz_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            orderedParts.Add((OrderPart)btn.DataContext);
            neededParts.Remove((OrderPart)btn.DataContext);

            LB_NeededParts.DataContext = null;
            LB_OrderedParts.DataContext = null;
            LB_NeededParts.DataContext = neededParts;
            LB_OrderedParts.DataContext = orderedParts;
        }

        private void BTN_bin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            neededParts.Add((OrderPart)btn.DataContext);
            orderedParts.Remove((OrderPart)btn.DataContext);

            neededParts.Sort((a, b) => a.PartName.CompareTo(b.PartName));

            LB_NeededParts.DataContext = null;
            LB_OrderedParts.DataContext = null;
            LB_NeededParts.DataContext = neededParts;
            LB_OrderedParts.DataContext = orderedParts;
        }
    }
}
