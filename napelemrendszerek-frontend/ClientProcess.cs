using System;
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
    }
}
