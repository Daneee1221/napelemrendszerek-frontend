using napelemrendszerek_backend.Models;
using napelemrendszerek_frontend.RaktarvezetoUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comm
{
    class Process
    {
        public Process() { }

        public async Task<Communication> Login(string username, string password)
        {
            Communication commObject = new Communication();
            commObject.Message= "login";
            Dictionary<string, string> loginData = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };
            commObject.addItemToContent(loginData);

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> GetParts(int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getParts";
            commObject.RoleId = roleID;
            commObject.addItemToContent(new Dictionary<string, string>());

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> AddPart(Part newPart, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "addPart";
            commObject.addItemToContent(newPart.GetValues());
            commObject.RoleId = roleID;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> ModifyPart(Dictionary<string, string> newValues, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "modifyPart";
            commObject.addItemToContent(newValues);
            commObject.RoleId = roleID;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> GetCompartments(int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getAllCompartments";
            commObject.RoleId = roleID;
            commObject.addItemToContent(new Dictionary<string, string>());

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> GetUnallocatedParts(int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getUnallocatedParts";
            commObject.RoleId = roleID;
            commObject.addItemToContent(new Dictionary<string, string>());

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> SendChangedCompartments(List<CompartmentWithPart> changedCompartments, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "modifyCompartment";
            commObject.RoleId = roleID;
            foreach (CompartmentWithPart compartment in changedCompartments)
            {
                commObject.addItemToContent(compartment.GetValues());
            }

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> AddNewProject(Project newProject, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "addProject";
            commObject.addItemToContent(newProject.GetValues());
            commObject.RoleId = roleID;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> CheckForUnaccocatedParts(int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getUnallocatedStatus";
            commObject.RoleId = roleID;
            commObject.addItemToContent(new Dictionary<string, string>());

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> GetProjects(int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getAllProjects";
            commObject.RoleId = roleID;
            commObject.addItemToContent(new Dictionary<string, string>());

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> addPartsToProject(Dictionary<string, string> partsList, Dictionary<string, string> id, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "addPartsToProject";
            commObject.addItemToContent(id);
            commObject.addItemToContent(partsList);
            commObject.RoleId = roleID;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> setWorkfeeAndEstimatedTime(Dictionary<string, string> l, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "setWorkfeeAndEstimatedTime";
            commObject.addItemToContent(l);
            commObject.RoleId = roleID;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }

        public async Task<Communication> GetProjectParts(Dictionary<string,string> d, int roleID)
        {
            Communication commObject = new Communication();
            commObject.Message = "getAllPartsForProject";
            commObject.RoleId = roleID;
            commObject.addItemToContent(d);

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = await tsResponse;

            return dResponse;
        }
    }
}
