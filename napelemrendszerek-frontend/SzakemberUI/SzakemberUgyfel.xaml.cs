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
    /// <summary>
    /// Interaction logic for SzakemberUgyfel.xaml
    /// </summary>
    public partial class SzakemberUgyfel : Page
    {
        private SolidColorBrush errorInputBackground;
        private MainWindow mainWindow;

        public SzakemberUgyfel()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            errorInputBackground = new SolidColorBrush(Color.FromScRgb(0.69f, 1f, 0.05f, 0.05f));

            TB_name.Focus();
        }

        private async void BTN_ujUgyfelHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            TB_ugyfelResponse.Text = string.Empty;
            string teljesNev = TB_name.Text;
            string cim = TB_cim.Text;
            string telefonszam = TB_telefonszam.Text;
            string email = TB_email.Text;
            string helyszin = TB_helyszin.Text;
            string leiras = TB_leiras.Text;

            bool foundEmptyInput = false;
            if (teljesNev == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (cim == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (telefonszam == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (email == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (helyszin == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (leiras == "")
            {
                TB_name.Background = errorInputBackground;
                TB_ugyfelResponse.Text = "Kötelező kitölteni!";
                foundEmptyInput = true;
            }
            if (foundEmptyInput)
            {
                return;
            }

            BTN_ujUgyfelHozzaadas.IsEnabled = false;
            BTN_ujUgyfelHozzaadas.Content = "Kis türelmet!";

            Project newProject = new Project(teljesNev, cim, telefonszam, email, helyszin, leiras, "Teszt Szakember");

            string response = await mainWindow.AddNewProject(newProject);

            BTN_ujUgyfelHozzaadas.Content = "Hozzáad";
            BTN_ujUgyfelHozzaadas.IsEnabled = true;

            switch (response)
            {
                case "successful":
                    TB_ugyfelResponse.Text = "Sikeres rögzítés";
                    break;
                case "already exists":
                    TB_ugyfelResponse.Text = "Az ügyfél már szerepel a rendszerben!";
                    break;
            }
        }


        private void BTN_ClearForm_Click(object sender, RoutedEventArgs e)
        {
            TB_name.Text = "";
            TB_cim.Text = "";
            TB_telefonszam.Text = "";
            TB_email.Text = "";
            TB_helyszin.Text = "";
            TB_leiras.Text = "";
        }
    }
}
