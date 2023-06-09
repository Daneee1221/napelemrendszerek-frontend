﻿using napelemrendszerek_frontend.RaktarvezetoUI;
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
    /// Interaction logic for RaktarvezetoBasePage.xaml
    /// </summary>
    public partial class RV_BasePage : Page
    {
        private MainWindow mainWindow;

        private Image IMG_alert;

        public RV_BasePage()
        {
            InitializeComponent();

            mainWindow = (MainWindow)Application.Current.MainWindow;

            _ = CheckForUnaccocatedParts();
        }

        public void ReEnableMenuBar()
        {
            MenuBar.IsEnabled = true;
        }

        private async Task CheckForUnaccocatedParts()
        {
            bool foundUnallocatedParts = await mainWindow.CheckForUnaccocatedParts();

            if (foundUnallocatedParts && SP_StoreNewParts.Children.Contains(IMG_alert) == false)
            {
                IMG_alert = new Image();
                IMG_alert.Source = new BitmapImage(new Uri("../img/alert.png", UriKind.Relative));
                IMG_alert.Height = 30;
                SP_StoreNewParts.Children.Add(IMG_alert);
            }
        }

        public void SetAlertAfterOrder()
        {
            if (SP_StoreNewParts.Children.Contains(IMG_alert) == false)
            {
                IMG_alert = new Image();
                IMG_alert.Source = new BitmapImage(new Uri("../img/alert.png", UriKind.Relative));
                IMG_alert.Height = 30;
                SP_StoreNewParts.Children.Add(IMG_alert);
            }
        }

        private async void Menu_ManageParts_Click(object sender, RoutedEventArgs e)
        {
            MenuBar.IsEnabled = false;
            await CheckForUnaccocatedParts();
            FR_RaktarosMainFrame.Navigate(new PartManagementPage(this));
        }

        private void Menu_StoreNewParts_Click(object sender, RoutedEventArgs e)
        {
            MenuBar.IsEnabled = false;
            FR_RaktarosMainFrame.Navigate(new StoreNewPartsPage(this));
            SP_StoreNewParts.Children.Remove(IMG_alert);
        }

        private async void Menu_OrderParts_Click(object sender, RoutedEventArgs e)
        {
            MenuBar.IsEnabled = false;
            await CheckForUnaccocatedParts();
            FR_RaktarosMainFrame.Navigate(new OrderMissingPartsPage(this));
        }

        private void Menu_Logout_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Logout();
        }
    }
}
