using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comm
{
    class Process
    {
        public Process() { }
        public void ReadAndWrite()
        {
            //string data = Console.ReadLine();
            string data = "hardcodedData";
            Communication commObject = new Communication();

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = tsResponse.Result;
            Console.WriteLine("Received response: " + dResponse);
        }

        public Communication Login(string username, string password)
        {
            Communication commObject = new Communication();
            commObject.Message= "login";
            Dictionary<string, string> loginData = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };
            commObject.Content = loginData;

            Task<Communication> tsResponse = SocketClient.SendRequest(commObject);
            Console.WriteLine("Sent request, waiting for response");
            Communication dResponse = tsResponse.Result;

            return dResponse;
        }
    }
}
