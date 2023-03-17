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
    /// <summary>
    /// Interaction logic for Raktarvezeto_PartManagementPage.xaml
    /// </summary>
    public partial class Raktarvezeto_PartManagementPage : Page
    {
        private MainWindow mainWindow;
        private List<Part> parts;

        public Raktarvezeto_PartManagementPage()
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);

            parts = mainWindow.StartGetPartsProcess();
            LB_Parts.DataContext = parts;
        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {
            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);
        }

        private void LB_Parts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SP_ModifyPartData.DataContext = LB_Parts.SelectedItem;
        }

        private void BTN_ClearModifyForm_Click(object sender, RoutedEventArgs e)
        {
            LB_Parts.SelectedIndex = -1;
            TB_MaxNumberInBox.Clear();
            TB_SellPrice.Clear();
        }

        private void BTN_SaveNewPart_Click(object sender, RoutedEventArgs e)
        {
            if (TB_NewMaxNumberInBox.Text == "")
            {
                //TODO: Hiba jelzés
                return;
            }
            if (TB_NewPartName.Text == "")
            {
                //TODO: Hiba jelzés
                return;
            }
            if (TB_NewSellPrice.Text == "")
            {
                //TODO: Hiba jelzés
                return;
            }
            Part newPart = new Part(TB_NewPartName.Text, Convert.ToInt32(TB_NewMaxNumberInBox.Text), Convert.ToInt32(TB_NewSellPrice.Text), 0);
            string response = mainWindow.StartAddPartProcess(newPart); //switch-case -> display response to user

            parts = mainWindow.StartGetPartsProcess(); //use local list?
            LB_Parts.DataContext = parts;

            //clear form
        }

        private void BTN_ClearNewPartForm_Click(object sender, RoutedEventArgs e)
        {
            TB_NewPartName.Clear();
            TB_NewMaxNumberInBox.Clear();
            TB_NewSellPrice.Clear();
        }

        private void BTN_ModifyPart_Click(object sender, RoutedEventArgs e)
        {
            if (TB_MaxNumberInBox.Text == "")
            {
                //TODO: Hiba jelzés
                return;
            }
            if (TB_SellPrice.Text == "")
            {
                //TODO: Hiba jelzés
                return;
            }
            if (LB_Parts.SelectedIndex == -1)
            {
                //TODO: Hiba jelzés
                return;
            }

            Part selectedPart = LB_Parts.SelectedItem as Part;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            if (selectedPart.SellPrice != Convert.ToInt32(TB_SellPrice.Text))
            {
                newValues.Add("SellPrice", TB_SellPrice.Text);
            }
            if (selectedPart.MaxNumberInBox != Convert.ToInt32(TB_MaxNumberInBox.Text))
            {
                newValues.Add("MaxNumberInBox", TB_MaxNumberInBox.Text);
            }
            if (newValues.Count == 0)
            {
                //TODO: Hiba jelzés
                return;
            }

            //TODO: küldés backendre
        }
    }
}
