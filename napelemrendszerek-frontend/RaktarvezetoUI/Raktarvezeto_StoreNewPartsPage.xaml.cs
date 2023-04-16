using napelemrendszerek_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace napelemrendszerek_frontend.RaktarvezetoUI
{
    public class CompartmentWithPart
    {
        public string Id { get; set; }
        public string PartName { get; set; }
        public int StoredAmount { get; set; }
        public int MaxAmount { get; set; }

        public CompartmentWithPart(Dictionary<string, string> values)
        {
            Id = values["CompartmentId"];
            StoredAmount = Convert.ToInt32(values["StoredAmount"]);
            if (values.ContainsKey("PartName"))
            {
                PartName = values["PartName"];
                MaxAmount = Convert.ToInt32(values["MaxNumberInBox"]);
            }
            else
            {
                PartName = "Üres Rekesz!";
                MaxAmount = 0;
            }
        }
        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "compartmentID", Id },
                { "partName", PartName },
                { "db", StoredAmount.ToString() }
            };
            return values;
        }
    }

    /// <summary>
    /// Interaction logic for Raktarvezeto__StoreNewPartsPage.xaml
    /// </summary>
    public partial class Raktarvezeto__StoreNewPartsPage : Page
    {
        private readonly char[] shelfRowLetters = { 'A', 'B', 'C', 'D', 'E', 'F' };

        private SolidColorBrush unselectableBrush;
        private SolidColorBrush selectableBrush;
        private SolidColorBrush lastBrush;
        private SolidColorBrush fullBrush;
        private int selectedShelfIndex;

        private List<CompartmentWithPart> compartments;
        private List<Part> parts;
        private List<CompartmentWithPart> changedCompartments;
        private MainWindow mainWindow;

        public Raktarvezeto__StoreNewPartsPage()
        {
            InitializeComponent();
            unselectableBrush = new SolidColorBrush(Color.FromRgb(180, 180, 180));
            lastBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            selectableBrush = new SolidColorBrush(Color.FromRgb(144, 212, 162));
            fullBrush = new SolidColorBrush(Color.FromRgb(214, 173, 90));

            mainWindow = (MainWindow)Application.Current.MainWindow;

            changedCompartments = new List<CompartmentWithPart>();

            loadCompartmentsAndParts();
            //TODO
            //Alert on load (to the menu bar)
            //
            //alert to save before quit???
        }

        private async void loadCompartmentsAndParts()
        {
            parts = await mainWindow.GetUnallocatedParts();
            LB_Parts.DataContext = parts;

            List<Dictionary<string, string>> responseList = await mainWindow.GetCompartments();

            compartments = new List<CompartmentWithPart>();
            foreach (Dictionary<string, string> compartmentDict in responseList)
            {
                compartments.Add(new CompartmentWithPart(compartmentDict));
            }

            //1. Sor betöltése
            RowSelectorBTN_Click(BTN_SelectRow1, new RoutedEventArgs());
        }

        private void RowSelectorBTN_Click(object sender, RoutedEventArgs e)
        {
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
            selectedShelfIndex = parent.Children.IndexOf(btn) - 1;

            GenerateUIBoxes();

            SetCompartmentColors();
        }

        private void GenerateUIBoxes()
        {
            WP_Compartments.Children.Clear();

            foreach (CompartmentWithPart comp in compartments.Where(x => x.Id.First() == shelfRowLetters[selectedShelfIndex]))//Kezdőkarakter alapján csak az adott raktársoron megy végig
            {
                TextBlock TBname = new TextBlock
                {
                    Text = comp.PartName,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                TextBlock TBamount = new TextBlock
                {
                    Text = $"{comp.StoredAmount} / {comp.MaxAmount}",
                    FontSize = 25,
                    FontWeight = FontWeights.Light,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                TextBlock TBboxId = new TextBlock
                {
                    Text = $"BoxID: {comp.Id}",
                    FontSize = 12,
                    FontWeight = FontWeights.Medium,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 10, 5, 0)
                };

                StackPanel stackPanel = new StackPanel
                {
                    Background = unselectableBrush,
                    Orientation = Orientation.Vertical,
                    Width = 178,
                    Height = 98
                };
                stackPanel.MouseLeftButtonDown += StackPanel_MouseLeftButtonDown;

                Border border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    BorderThickness = new Thickness(1)
                };
                border.MouseEnter += StackPanel_MouseEnter;
                border.MouseLeave += StackPanel_MouseLeave;

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
            ((sender as Border).Child as StackPanel).Background = lastBrush;
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel SP = (sender as Border).Child as StackPanel;
            lastBrush = SP.Background as SolidColorBrush;
            SolidColorBrush hoverBrush = new SolidColorBrush(Color.FromRgb((byte)(lastBrush.Color.R - 50), (byte)(lastBrush.Color.G - 50), (byte)(lastBrush.Color.B - 50)));
            int childIndex = WP_Compartments.Children.IndexOf(sender as Border);
            int rowIndex = childIndex % 5;
            int columnIndex = childIndex / 5;
            (SP_ColumnNumbers.Children[columnIndex] as TextBlock).Background = hoverBrush;
            (SP_RowNumbers.Children[rowIndex] as TextBlock).Background = hoverBrush;
            SP.Background = hoverBrush;
        }

        private void SetCompartmentColors()
        {
            if (LB_Parts.SelectedIndex == -1)
            {
                return;
            }

            List<CompartmentWithPart> filteredCompartments = compartments.Where(x => x.Id.First() == shelfRowLetters[selectedShelfIndex]).ToList();
            for (int i = 0; i < WP_Compartments.Children.Count; i++)
            {
                FrameworkElement element = WP_Compartments.Children[i] as FrameworkElement;

                StackPanel stackPanel = (element as Border).Child as StackPanel;

                if ((LB_Parts.SelectedItem as Part).PartName == filteredCompartments[i].PartName) // alkatrész stimmel
                {
                    if (filteredCompartments[i].StoredAmount == filteredCompartments[i].MaxAmount)
                    {
                        //fullon van
                        stackPanel.Background = fullBrush;
                    }
                    else
                    {
                        stackPanel.Background = selectableBrush;
                    }
                }
                else if (filteredCompartments[i].PartName == "Üres Rekesz!")
                {
                    stackPanel.Background = selectableBrush;
                }
                else
                {
                    stackPanel.Background = unselectableBrush;
                }

            }
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LB_Parts.SelectedIndex == -1)
            {
                LB_Parts.Background = new SolidColorBrush(Color.FromRgb(227, 113, 104));
                TB_PartListTitle.Foreground = new SolidColorBrush(Colors.Red);
                TB_PartListTitle.Text = "Válassz a listából!";
                TB_PartListTitle.FontStyle = FontStyles.Normal;
                TB_PartListTitle.FontWeight = FontWeights.Normal;
                return;
            }

            StackPanel stackPanel = sender as StackPanel;
            Border parent = stackPanel.Parent as Border;
            int stackPanelIndex = WP_Compartments.Children.IndexOf(parent);
            int compIndex = stackPanelIndex + selectedShelfIndex * 25;
            CompartmentWithPart selectedComp = compartments[compIndex];
            Part selectedPart = LB_Parts.SelectedItem as Part;

            if (selectedComp.PartName != selectedPart.PartName && selectedComp.PartName != "Üres Rekesz!")
            {
                return;
            }
            if (selectedComp.StoredAmount == selectedComp.MaxAmount && selectedComp.PartName != "Üres Rekesz!")
            {
                return;
            }

            int storableAmount = 0;
            if (selectedComp.MaxAmount == 0)//Üresbe pakol
            {
                storableAmount = selectedPart.MaxNumberInBox;
                if (storableAmount > selectedPart.Unallocated)
                {
                    storableAmount = selectedPart.Unallocated;
                }
                selectedComp.PartName = selectedPart.PartName;
                selectedComp.StoredAmount = storableAmount;
                selectedComp.MaxAmount = selectedPart.MaxNumberInBox;
            }
            else//Megkezdettbe pakol
            {
                storableAmount = selectedComp.MaxAmount - selectedComp.StoredAmount;
                if (storableAmount > selectedPart.Unallocated)
                {
                    storableAmount = selectedPart.Unallocated;
                }
                selectedComp.StoredAmount = storableAmount;
                selectedComp.MaxAmount = selectedPart.MaxNumberInBox;
            }

            if (selectedComp.StoredAmount == selectedComp.MaxAmount)//A kiválasztott stackpanel színezése
            {
                lastBrush = fullBrush;
            }

            bool partGotRemoved = false;
            selectedPart.Unallocated -= storableAmount;
            if (selectedPart.Unallocated == 0)
            {
                parts.Remove(selectedPart);
                partGotRemoved = true;
            }

            changedCompartments.Add(selectedComp);

            //Update UI
            (stackPanel.Children[0] as TextBlock).Text = selectedComp.PartName;
            (stackPanel.Children[1] as TextBlock).Text = $"{selectedComp.StoredAmount} / {selectedComp.MaxAmount}";
            SetCompartmentColors();

            LB_Parts.DataContext = null;
            LB_Parts.DataContext = parts;
            if (partGotRemoved == false)
            {
                LB_Parts.SelectedItem = selectedPart;
            }
        }

        private void LB_Parts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Parts.Background = new SolidColorBrush(Colors.White);
            TB_PartListTitle.Foreground = new SolidColorBrush(Colors.Black);
            TB_PartListTitle.Text = "Alkatrészek:";
            TB_PartListTitle.FontStyle = FontStyles.Italic;
            TB_PartListTitle.FontWeight = FontWeights.Light;
            SetCompartmentColors();
        }

        private async void BTN_save_Click(object sender, RoutedEventArgs e)
        {
            BTN_save.IsEnabled = false;
            BTN_save.Content = "Kis türelmet";
            string res = await mainWindow.SendChangedCompartments(changedCompartments);

            if (res == "successful")
            {
                BTN_save.Content = "Sikeres mentés!";
                BTN_save.Foreground = new SolidColorBrush(Colors.Green);
            }
            await Task.Delay(2500);
            BTN_save.IsEnabled = true;
            BTN_save.Foreground = new SolidColorBrush(Colors.Black);
            BTN_save.Content = "Mentés";
        }
    }
}
