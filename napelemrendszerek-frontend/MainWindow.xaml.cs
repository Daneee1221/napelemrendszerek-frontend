﻿using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

using Comm;
using napelemrendszerek_backend.Models;
using napelemrendszerek_frontend.RaktarvezetoUI;

namespace napelemrendszerek_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int roleID = 0;

        private Process process;

        public MainWindow()
        {
            InitializeComponent();
            
            FR_mainFrame.Source = new Uri("./loginPage.xaml", UriKind.Relative);

            SocketClient.StartClient();
            process = new Process();
        }

        public async Task<string> Login(string username, string password)
        {
            Communication responseObject = await process.Login(username, password);

            if (responseObject.Message == "successful")
            {
                roleID = (int)responseObject.RoleId;
                //load next page
                switch (roleID)
                {
                    case 1:
                        FR_mainFrame.Source = new Uri("./RaktarvezetoUI/RV_BasePage.xaml", UriKind.Relative);
                        break;
                    case 2:
                        FR_mainFrame.Source = new Uri("./RaktarosUI/RaktarosUI.xaml", UriKind.Relative);
                        break;
                    case 3:
                        FR_mainFrame.Source = new Uri("./SzakemberUI/SZ_BasePage.xaml", UriKind.Relative);
                        break;
                    default:
                        break;
                }
                return "";
            }
            else
            {
                return responseObject.Message;
            }
        }

        public async Task<List<Part>> GetParts()
        {
            List<Part> parts = new List<Part>();

            Communication responseObject = await process.GetParts(roleID);

            foreach (Dictionary<string, string> pair in responseObject.Content)
            {
                parts.Add(new Part(pair));
            }

            return parts;
        }

        public async Task<string> AddPart(Part newPart)
        {
            Communication responseObject = await process.AddPart(newPart, roleID);

            return responseObject.Message;
        }

        public async Task<string> ModifyPart(Dictionary<string, string> newValues)
        {
            Communication responseObject = await process.ModifyPart(newValues, roleID);

            return responseObject.Message;
        }

        public async Task<List<Dictionary<string, string>>> GetCompartments()
        {
            Communication responseObject = await process.GetCompartments(roleID);

            return responseObject.Content;
        }

        public async Task<List<Part>> GetUnallocatedParts()
        {
            List<Part> parts = new List<Part>();

            Communication responseObject = await process.GetUnallocatedParts(roleID);

            foreach (Dictionary<string, string> pair in responseObject.Content)
            {
                parts.Add(new Part(pair));
            }

            return parts;
        }

        public async Task<string> SendChangedCompartments(List<CompartmentWithPart> changedCompartments)
        {
            Communication responseObject = await process.SendChangedCompartments(changedCompartments, roleID);

            return responseObject.Message;
        }

        public async Task<bool> CheckForUnaccocatedParts()
        {
            bool foundUnallocatedParts = false;
            Communication responseObject = await process.CheckForUnaccocatedParts(roleID);

            if (responseObject.Message == "successful")
            {
                foundUnallocatedParts = Convert.ToBoolean(responseObject.Content[0]["isThereUnallocated"]);
            }

            return foundUnallocatedParts;
        }
        
        public async Task<string> AddNewProject(Project newProject)
        {
            Communication responseObject = await process.AddNewProject(newProject, 3);

            return responseObject.Message;
        }

        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = new List<Project>();

            Communication responseObject = await process.GetProjects(roleID);

            foreach (Dictionary<string, string> pair in responseObject.Content)
            {
                projects.Add(new Project(pair));
            }

            return projects;
        }
    }
}
