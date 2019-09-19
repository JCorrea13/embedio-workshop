using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace EmbedioWorkshop
{
        public class Program
        {
            private static void Main(string[] args)
            {
                var server = new WebServer("http://localhost:1234");

                server.RunAsync();

                Console.WriteLine("Hello World!");
                Console.ReadLine();
            }
        }
}
