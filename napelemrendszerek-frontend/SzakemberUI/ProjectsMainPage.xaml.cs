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
        private SZ_BasePage parentPage;

        public ProjectsMainPage(SZ_BasePage parentPage)
        {
            InitializeComponent();
            mainWindow = ((MainWindow)Application.Current.MainWindow);
            this.parentPage = parentPage;

            _ = loadProjectsList();
        }

        public async Task loadProjectsList()
        {
            projects = await mainWindow.GetProjects();
            SP_projectList.Children.Remove(TB_Loading);
            LB_projektekLista.DataContext = null;
            LB_projektekLista.DataContext = projects;

            parentPage.ReEnableMenuBar();
        }

        public async Task refreshProjectsList()
        {
            parentPage.DisableMenuBar();

            int pID = ((Project)LB_projektekLista.SelectedItem).ProjectId;
            await loadProjectsList();
            Project p = projects.Single(x => x.ProjectId == pID);
            LB_projektekLista.SelectedItem = p;

            parentPage.ReEnableMenuBar();
        }

        public void ReEnableList()
        {
            LB_projektekLista.IsEnabled = true;
            parentPage.ReEnableMenuBar();
        }

        private void LB_projektekLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LB_projektekLista.SelectedIndex == -1)
            {
                return;
            }

            parentPage.DisableMenuBar();

            Project selectedProject = (Project)LB_projektekLista.SelectedItem;

            LB_projektekLista.IsEnabled = false;

            int status = selectedProject.ProjectStateId;

            switch(status)
            {
                case 1:
                    FR_ProjektekFrame.Navigate(new NewDraftPage(selectedProject.ProjectId, selectedProject.ProjectStateId, this));
                    break;
                case 2:
                    FR_ProjektekFrame.Navigate(new NewDraftPage(selectedProject.ProjectId, selectedProject.ProjectStateId, this));
                    break;
                case 3:
                    FR_ProjektekFrame.Navigate(new WaitScheduledPage(selectedProject.ProjectId, selectedProject.ProjectStateId, this));
                    break;
                case 4:
                    FR_ProjektekFrame.Navigate(new WaitScheduledPage(selectedProject.ProjectId, selectedProject.ProjectStateId, this));
                    break;
                case 5:
                    FR_ProjektekFrame.Navigate(new InProgressPage(selectedProject.ProjectId, this));
                    break;
                default:
                    ReEnableList();
                    break;
            }
        }
    }
}
