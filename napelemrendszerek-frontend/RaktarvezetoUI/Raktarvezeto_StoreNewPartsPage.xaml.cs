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
using static System.Net.Mime.MediaTypeNames;

namespace napelemrendszerek_frontend.RaktarvezetoUI
{
    class TestClass
    {
        public string PartName { get; set; }
        public int Amount { get; set; }

        public TestClass(string partName, int amount)
        {
            this.PartName = partName;
            this.Amount = amount;
        }
    }

    /// <summary>
    /// Interaction logic for Raktarvezeto__StoreNewPartsPage.xaml
    /// </summary>
    public partial class Raktarvezeto__StoreNewPartsPage : Page
    {
        public Raktarvezeto__StoreNewPartsPage()
        {
            InitializeComponent();

            //1. Sor betöltése
            RowSelectorBTN_Click(BTN_SelectRow1, new RoutedEventArgs());

            List<TestClass> parts = new List<TestClass>
            {
                new TestClass("Teszt Alkatrész 1", 12),
                new TestClass("Teszt Alkatrész 2", 24),
                new TestClass("Teszt Alkatrész 3", 8)
            };
            LB_Parts.DataContext = parts;
        }

        private void RowSelectorBTN_Click(object sender, RoutedEventArgs e)
        {
            WP_Compartments.Children.Clear();

            Button btn = (Button)sender;
            btn.IsEnabled = false;
            StackPanel parent = btn.Parent as StackPanel;
            foreach (FrameworkElement element in parent.Children)
            {
                if (element is Button && element.Name != btn.Name)
                {
                    element.IsEnabled = true;
                }
            }
            int selectedShelf = parent.Children.IndexOf(btn);
            //adott sort lekérni a backendtől -> outer join, hogy minden compartment benne legyen ami a sorba tartozik

            for (int i = 0; i < 25; i++) // majd foreach lesz egyszer
            {
                //TODO -> HardCoded cuccokat kibombázni
                Border border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    BorderThickness = new Thickness(1)
                };
                border.MouseEnter += StackPanel_MouseEnter;
                border.MouseLeave += StackPanel_MouseLeave;

                StackPanel stackPanel = new StackPanel
                {
                    Background = new SolidColorBrush(Colors.White),
                    Orientation = Orientation.Vertical,
                    Width = 178,
                    Height = 98
                };
                stackPanel.MouseLeftButtonDown += StackPanel_MouseLeftButtonDown;
                
                TextBlock TBname = new TextBlock
                {
                    Text = "Napelem panel (500W)",
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                TextBlock TBamount = new TextBlock
                {
                    Text = "2 / " + selectedShelf,
                    FontSize = 25,
                    FontWeight = FontWeights.Light,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                TextBlock TBboxId = new TextBlock
                {
                    Text = "BoxID: A1-1",
                    FontSize = 12,
                    FontWeight = FontWeights.Medium,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 10, 5, 0)
                };
                stackPanel.Children.Add(TBname);
                stackPanel.Children.Add(TBamount);
                stackPanel.Children.Add(TBboxId);
                border.Child = stackPanel;
                WP_Compartments.Children.Add(border);
            }
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            int childIndex = WP_Compartments.Children.IndexOf(sender as Border);
            int rowIndex = childIndex % 5;
            int columnIndex = childIndex / 5;
            (SP_ColumnNumbers.Children[columnIndex] as TextBlock).Background = null;
            (SP_RowNumbers.Children[rowIndex] as TextBlock).Background = null;
            ((sender as Border).Child as StackPanel).Background = null;
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            int childIndex = WP_Compartments.Children.IndexOf(sender as Border);
            int rowIndex = childIndex % 5;
            int columnIndex = childIndex / 5;
            (SP_ColumnNumbers.Children[columnIndex] as TextBlock).Background = new SolidColorBrush(Color.FromRgb(170, 210, 230));
            (SP_RowNumbers.Children[rowIndex] as TextBlock).Background = new SolidColorBrush(Color.FromRgb(170, 210, 230));
            ((sender as Border).Child as StackPanel).Background = new SolidColorBrush(Color.FromRgb(170, 210, 230));
        }

        //Tesztkód!
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;
            panel.Background = new SolidColorBrush(Colors.DarkTurquoise);
        }
    }
}
