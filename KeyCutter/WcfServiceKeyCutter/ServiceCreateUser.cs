using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceKeyCutter.Models;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WcfServiceKeyCutter
{
    public class ServiceCreateUser : IServiceCreateUser
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public User GetNewUser()
        {

            // create listner

            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 9000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                //while (true)
                //{
                //    Console.Write("Waiting for a connection... ");

                //    // Perform a blocking call to accept requests.
                //    // You could also use server.AcceptSocket() here.
                //  //  TcpClient client = server.AcceptTcpClient();
                //    Console.WriteLine("Connected!");

                //    data = null;

                
                //}
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();


            //TcpListener listener = null;
            //try
            //{

            //    listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            //    listener.Start();
            //    Console.WriteLine("Starting server...");

            //    while (true)
            //    {
            //        Console.WriteLine("Waiting for incoming client connections...");
            //        TcpClient client = listener.AcceptTcpClient();
            //        Console.WriteLine("Accepted connection from client...");

            //        StreamReader reader = new StreamReader(client.GetStream());
            //        StreamWriter writer = new StreamWriter(client.GetStream());
            //        string s = string.Empty;

            //        while (!(s = reader.ReadLine()).Equals("Exit") || (s == null))
            //        {
            //            Console.WriteLine("From client ->" + s);
            //            writer.WriteLine("From server" + s);
            //            writer.Flush();
            //        }

            //        reader.Close();
            //        writer.Close();
            //        client.Close();


            //    }
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e);
            //}
            //finally
            //{
            //    if (listener != null)
            //    {
            //        listener.Stop();
            //    }
            //}


            var user = new User() {Username="ady", Password= "asbkjsdna38723" };

            
            return user;
        }




    }
}
