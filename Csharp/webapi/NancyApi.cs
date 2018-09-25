

using Nancy;

using Nancy.Hosting.Self;

using System;
using System.Threading.Tasks;

namespace NancyFxDemo

{

    public class Pro

    {

       public static void run()

        {

            using (var host = new NancyHost(

                new Uri("http://localhost:9521")))

            {

                host.Start();

                Console.WriteLine("Press any key to stop...");

                Console.Read();

                host.Stop();

            }

        }

    }

 

    public class GuidGeneratorModule : NancyModule

    {

        public GuidGeneratorModule()

        {

            Get["/"] = (p, x) =>
            {
                return GetString();
            };

        }
        public async Task<dynamic> GetString()
        {
            return await Task.FromResult("Hello");
        }

    }
            class ConcatParams

        {

            public string A { get; set; }

            public string B { get; set; }

        }

 

}

