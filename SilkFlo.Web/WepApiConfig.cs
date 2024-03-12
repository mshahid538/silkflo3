//using Microsoft.Extensions.PlatformAbstractions;
//using Swashbuckle.Application;
//using System.Web.Http;

//namespace SilkFlo.Web
//{
//    public static class WebApiConfig
//    {
//        public static void Register(HttpConfiguration config)
//        {
//            // Web API routes
//            config.EnableSwagger(c =>
//            {
//                c.SingleApiVersion("v1", "My API");
                
//                // Add XML comments for documentation (optional)
//                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
//                //var xmlCommentsFile = Path.Combine(basePath, "MyProject.xml");
//                //c.IncludeXmlComments(xmlCommentsFile);
//            })
//                .EnableSwaggerUi();

//            //config.EnableSwaggerUi(c =>
//            //{
//            //    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "My API V1");
//            //});
//        }
//    }
//}
