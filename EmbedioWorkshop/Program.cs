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
            /*server.OnGet( async (ctx) => {
                await ctx.SendStringAsync($"Hola Mundo {ctx.RequestedPath}", "text/plain",  System.Text.Encoding.UTF8);
            });*/

            server.WithLocalSessionManager();
            
            server.WithWebApi("/api", (m) => {
                m.RegisterController<SampleController>();

                server.OnHttpException += (ctx, httpException) =>
                {
                    return ctx.SendDataAsync(httpException);
                };
            });


            server.WithStaticFolder("/", "C://Users//jose.correa//embedio-workshop//staticFiles", false);
            

                server.RunAsync();

                Console.WriteLine("Hello World!");
                Console.ReadLine();
            }
        }

    public class SampleController : WebApiController
    {
        [Route(HttpVerbs.Get, "/counter")]
        public int Count()
        {
            var isValue = HttpContext.Session.TryGetValue("counter", out var value);
            if (isValue)
            {
                HttpContext.Session["counter"] = (int)value + 1;
            }
            else
            {
                HttpContext.Session["counter"] = 1;
            }

            return (int)HttpContext.Session["counter"];
        }


        [Route(HttpVerbs.Get, "/version")]
        public object GetVersion() => new { Version = "1.0.0" };

        [Route(HttpVerbs.Get, "/version/{client}")]
        public object GetClient(string client, [QueryField] string project) => new { Version = "1.0.0", Client = client, Project = project };

        [Route(HttpVerbs.Post, "/version")]
        public Task<object> Echo() => HttpContext.GetRequestDataAsync<object>();

        /*[Route(HttpVerbs.Post, "/echo2")]
        public Task<object> EchoError() {
            if()

            return HttpContext.GetRequestDataAsync<object>();
        }*/


    }
}
