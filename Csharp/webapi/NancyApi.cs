

using Nancy;

using Nancy.Hosting.Self;
using Nancy.Owin;//这个不需要
using System;
using System.Threading.Tasks;

namespace NancyFxDemo

{

    public class Pro

    {

       public static void run()

        {//在vs nuget安装引用 没有问题 ，vscode这里报错

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
            // Get["/ok"] = (p) =>
            // {
            //     return "ok";
            // };
            // Get["/json"] = p =>
            // {
            //     return Response.AsJson(new { result = true, message = "科比" });
               
            // };
            // //http://localhost:9521/xml?id=1
            // Get["/xml"] = p =>
            // {
            //     Person1 model = new Person1() { Name = "科比" };
            //     model.ID = Request.Query["id"];
            //     return Response.AsXml(model);
            // };
            // //http://localhost:9521/xml/1
            // Get["/xml/{id}"] = p =>
            // {
            //     Person1 model = new Person1() { Name = "科比1" };
            //     model.ID = p.id;
            //     return Response.AsXml(model);
            // };
            // Post["/photos/add"] = parameters =>
            // {
            //     // Add the photo, then redirect
            //     string slug = "newPhoto";
            //     return Response.AsRedirect("/admin/photos/edit/"
            //     + slug);
            // };

            // Get["/photos/edit/{slug}"] = parameters =>
            // {
            //     return String.Format(@"Display the form to edit a 

            //     photo called '{0}'.",
            //     parameters.slug);
            // };
            // Delete["/"] = x => {
            //     return "Default delete root";
            // };

        }
        public async Task<dynamic> GetString()
        {
            return await Task.FromResult("Hello");
        }

    }
    public class Person1
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class ConcatParams

    {

        public string A { get; set; }

        public string B { get; set; }

    }

 

}

