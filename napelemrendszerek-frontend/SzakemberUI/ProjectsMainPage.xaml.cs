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
    /// Interaction logic for SzakemberProjektek.xaml
    /// </summary>
    public partial class ProjectsMainPage : Page
    {
        private List<Project> projects;
        private MainWindow mainWindow;

        public ProjectsMainPage()
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);

            loadProjectsList();
        }

        public async void loadProjectsList()
        {
            projects = await mainWindow.GetProjects();
            LB_projektekLista.DataContext = projects;
        }

        private void LB_projektekLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Project selectedProject = (Project)LB_projektekLista.SelectedItem;

            int status = selectedProject.ProjectStateId;

            switch(status)
            {
                case 1:
                    FR_ProjektekFrame.Navigate(new NewDraftPage(selectedProject.ProjectId));
                    break;
                case 2:
                    FR_ProjektekFrame.Navigate(new NewDraftPage(selectedProject.ProjectId));
                    break;
                case 3:
                    FR_ProjektekFrame.Navigate(new WaitScheduledPage(selectedProject.ProjectId));
                    break;
                case 4:
                    FR_ProjektekFrame.Navigate(new WaitScheduledPage(selectedProject.ProjectId));
                    break;
                case 5:
                    FR_ProjektekFrame.Navigate(new InProgressPage(selectedProject.ProjectId));
                    break;
                default:
                    break;
            }
        }
    }
}
