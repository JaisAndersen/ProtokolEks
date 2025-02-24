using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProtokolEks
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient client)
        {
            Console.WriteLine(client.Client.RemoteEndPoint);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            bool isRunning = true;

            while (isRunning)
            {
                string? message = reader.ReadLine();
                Console.WriteLine(message);
                if (message == "time")
                {
                    writer.WriteLine(DateTime.Now.ToString("dd/MM/yy - HH:mm"));
                    writer.Flush();
                }
                else if (message == "wee")
                {
                    writer.WriteLine(message.ToUpper());
                    writer.Flush();
                }
                else if (message == "ToUpper")
                {
                    writer.WriteLine("write to convert to uppercase");
                    writer.Flush();
                    string? toUpperMessage = reader.ReadLine();
                    writer.WriteLine(toUpperMessage.ToUpper());
                    writer.Flush();
                }
                else if (message == "ToLower")
                {
                    writer.WriteLine("write to convert to lowercase");
                    writer.Flush();
                    string? toLowerMessage = reader.ReadLine();
                    writer.WriteLine(toLowerMessage.ToLower());
                    writer.Flush();
                }
                else if (message == "RollDice")
                {
                    writer.WriteLine("which die 4/6/8/10/12/20");
                    writer.Flush();
                    string? dicePick = reader.ReadLine();
                    if (dicePick == "4" || dicePick == "6" || dicePick == "8" || dicePick == "10" || dicePick == "12" || dicePick == "20")
                    {
                        int dicePickParsed = Int32.Parse(dicePick);
                        Random random = new Random();
                        writer.WriteLine(random.Next(1, dicePickParsed + 1));
                        writer.Flush();
                    }                    
                    else
                    {
                        writer.WriteLine("you have to pick dice number 4/6/8/10/12/20");
                        writer.Flush();
                    }
                }
                else if (message == "close")
                {
                    writer.WriteLine("Connection closed");
                    writer.Flush();
                    isRunning = false;
                }                
            }
            client.Close();
        }
    }
}
