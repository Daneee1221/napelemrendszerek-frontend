using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Comm
{
    public class SocketClient
    {
        private const string host = "127.0.0.1";
        private const int port = 50000;

        private static StreamWriter writer;
        private static StreamReader reader;
        private static TcpClient client;

        public static void StartClient()
        {
            try
            {
                //Server IP address
                IPAddress ipAddress = IPAddress.Loopback;

                if (ipAddress == null)
                    throw new Exception("No IPv4 address for server");
                client = new TcpClient();
                client.Connect(ipAddress, port); // Connect
                Console.WriteLine("Connect to server " + ipAddress + " on port " + port);
                NetworkStream networkStream = client.GetStream();
                writer = new StreamWriter(networkStream);
                reader = new StreamReader(networkStream);
                writer.AutoFlush = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Close()
        {
            if (client.Connected)
            {
                client.Close();
            }
        }

        public static async Task<Communication> SendRequest(Communication data)
        {
            try
            {
                string requestData = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
                await writer.WriteLineAsync(requestData);
                string responseStr = await reader.ReadLineAsync();
                Communication response = JsonConvert.DeserializeObject<Communication>(responseStr, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
                return response;
            }
            catch (Exception ex)
            {
                return new Communication() { Message = ex.Message };
            }
        }
    }
}
