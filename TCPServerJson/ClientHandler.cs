using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TCPServerJson
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient client)
        {
            Console.WriteLine(client.Client.RemoteEndPoint + " connected");
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            bool IsRunning = true;

            while (IsRunning)
            {
                string? message = reader.ReadLine();
                if (message == null)
                {
                    Console.WriteLine(client.Client.RemoteEndPoint + " disconnected");
                    break;
                }

                Request? answerObj = JsonConvert.DeserializeObject<Request>(message);
                Console.WriteLine("Client json sent: " + message);

                switch (answerObj?.Method)
                {
                    case "Random":
                        Random rand = new Random();
                        answerObj.result = rand.Next(answerObj.num1, answerObj.num2);
                        break;

                    case "Add":
                        answerObj.result = answerObj.num1 + answerObj.num2;
                        break;

                    case "Subtract":
                        answerObj.result = answerObj.num1 - answerObj.num2;
                        break;

                    default:
                        var errorResponse = new { error = "Invalid method" };
                        writer.WriteLine(JsonConvert.SerializeObject(errorResponse));
                        return;
                }

                Request answer = new Request
                {
                    Method = answerObj.Method,
                    num1 = answerObj.num1,
                    num2 = answerObj.num2,
                    result = answerObj.result
                };
                
                string jsonResponse = JsonConvert.SerializeObject(answer);
                writer.WriteLine(jsonResponse);
            }
            client.Close();
        }
    }
}

