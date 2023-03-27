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
        private SolidColorBrush errorInputBackground;

        public Raktarvezeto_PartManagementPage()
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));

            loadPartsList();
        }

        private async void loadPartsList()
        {

            parts = await mainWindow.GetParts();
            StackPanel parent = LB_Parts.Parent as StackPanel;
            parent.Children.Remove(TB_Loading);
            LB_Parts.DataContext = parts;
        }

        private void NumberOnlyInput(object sender, TextCompositionEventArgs e)
        {
            Regex numOnly = new Regex("[^0-9.-]+");
            e.Handled = numOnly.IsMatch(e.Text);
        }

        private void LB_Parts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_Parts.SelectedIndex == -1)
            {
                TB_MaxNumberInBox.Text = "";
                TB_SellPrice.Text = "";
            }
            else
            {
                TB_MaxNumberInBox.Text = (LB_Parts.SelectedItem as Part).MaxNumberInBox.ToString();
                TB_SellPrice.Text = (LB_Parts.SelectedItem as Part).SellPrice.ToString();
                TB_MaxNumberInBox.Background = null;
                TB_SellPrice.Background = null;
            }
        }

        private void BTN_ClearModifyForm_Click(object sender, RoutedEventArgs e)
        {
            LB_Parts.SelectedIndex = -1;
            TB_MaxNumberInBox.Clear();
            TB_SellPrice.Clear();
            TB_MaxNumberInBox.Background = null;
            TB_SellPrice.Background = null;
        }

        private async void BTN_SaveNewPart_Click(object sender, RoutedEventArgs e)
        {
            bool foundEmptyInput = false;
            if (TB_NewMaxNumberInBox.Text == "")
            {
                TB_NewMaxNumberInBox.Text = "Kötelező kitölteni!";
                TB_NewMaxNumberInBox.Background = errorInputBackground;
                foundEmptyInput = true;
            }
            if (TB_NewPartName.Text == "")
            {
                TB_NewPartName.Text = "Kötelező kitölteni!";
                TB_NewPartName.Background = errorInputBackground;
                foundEmptyInput = true;
            }
            if (TB_NewSellPrice.Text == "")
            {
                TB_NewSellPrice.Text = "Kötelező kitölteni!";
                TB_NewSellPrice.Background = errorInputBackground;
                foundEmptyInput = true;
            }
            if (foundEmptyInput)
            {
                return;
            }

            BTN_SaveNewPart.IsEnabled = false;
            BTN_ClearNewPartForm.IsEnabled = false;
            BTN_SaveNewPart.Content = "Kis türelmet";

            Part newPart = new Part(TB_NewPartName.Text, Convert.ToInt32(TB_NewMaxNumberInBox.Text), Convert.ToInt32(TB_NewSellPrice.Text), 0);
            string response = await mainWindow.AddPart(newPart); //switch-case -> display response to user     

            parts = await mainWindow.GetParts(); //use local list?
            LB_Parts.DataContext = parts;

            BTN_SaveNewPart.IsEnabled = true;
            BTN_ClearNewPartForm.IsEnabled = true;
            BTN_SaveNewPart.Content = "Mentés";

            TB_NewPartName.Clear();
            TB_NewMaxNumberInBox.Clear();
            TB_NewSellPrice.Clear();
        }

        private void BTN_ClearNewPartForm_Click(object sender, RoutedEventArgs e)
        {
            TB_NewPartName.Clear();
            TB_NewMaxNumberInBox.Clear();
            TB_NewSellPrice.Clear();
            TB_NewPartName.Background = null;
            TB_NewMaxNumberInBox.Background = null;
            TB_NewSellPrice.Background = null;
        }

        private void TB_GotFocus_Reset(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox))
            {
                return;
            }
            TextBox TB = (TextBox)sender;
            if (TB.Background != errorInputBackground)
            {
                return;
            }
            TB.Text = "";
            TB.Background = null;
        }

        private async void BTN_ModifyPart_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Parts.SelectedIndex == -1)
            {
                TB_MaxNumberInBox.Background = errorInputBackground;
                TB_SellPrice.Background = errorInputBackground;
                TB_MaxNumberInBox.Text = "<- Válassz a listából!";
                TB_SellPrice.Text = "<- Válassz a listából!";
                return;
            }
            bool foundEmptyInput = false;
            if (TB_MaxNumberInBox.Text == "")
            {
                TB_MaxNumberInBox.Background = errorInputBackground;
                TB_MaxNumberInBox.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (TB_SellPrice.Text == "")
            {
                TB_SellPrice.Background = errorInputBackground;
                TB_SellPrice.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (foundEmptyInput)
            {
                return;
            }

            Part selectedPart = LB_Parts.SelectedItem as Part;
            bool vaulesChanged = false;
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            newValues.Add("partName", selectedPart.PartName);
            if (selectedPart.SellPrice != Convert.ToInt32(TB_SellPrice.Text))
            {
                vaulesChanged = true;
                newValues.Add("sellPrice", TB_SellPrice.Text);
            }
            if (selectedPart.MaxNumberInBox != Convert.ToInt32(TB_MaxNumberInBox.Text))
            {
                vaulesChanged = true;
                newValues.Add("maxNumber", TB_MaxNumberInBox.Text);
            }
            if (vaulesChanged == false)
            {
                return;
            }

            BTN_ModifyPart.IsEnabled = false;
            BTN_ClearModifyForm.IsEnabled = false;
            BTN_ModifyPart.Content = "Kis türelmet";

            string response = await mainWindow.ModifyPart(newValues);

            parts = await mainWindow.GetParts(); //use local list?
            LB_Parts.DataContext = parts;

            BTN_ModifyPart.IsEnabled = true;
            BTN_ClearModifyForm.IsEnabled = true;
            BTN_ModifyPart.Content = "Módosítás";
        }
    }
}
