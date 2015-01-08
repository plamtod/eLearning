/// <summary>
/// Returns a content negotiated result based on the Accept header.
/// Minimal implementation that works with JSON and XML content,
/// can also optionally return a view with HTML.    
/// </summary>
/// <example>
/// // model data only
/// public ActionResult GetCustomers()
/// {
///      return new CustomActionResult(repo.Customers.OrderBy( c=> c.Company) )
/// }
/// // optional view for HTML
/// public ActionResult GetCustomers()
/// {
///      return new CustomActionResult("List", repo.Customers.OrderBy( c=> c.Company) )
/// }
/// </example>
/// http://weblog.west-wind.com/posts/2014/May/20/Creating-ASPNET-MVC-Negotiated-Content-Results
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Elearning.MVC.CustomActionResult
{
    public class CustomActionResult: ActionResult
    {
        public object Data { get; set; }
        public string ViewName { get; set; }

        public CustomActionResult(string viewName, object data)
        {
            Data = data;
            ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (request.AcceptTypes.Contains("text/html")) 
            {

                var viewResult = new ViewResult{
                    ViewName = ViewName,
                    TempData = context.Controller.TempData,
                    ViewData = context.Controller.ViewData
                };
                viewResult.ViewData.Model = Data;
                viewResult.ExecuteResult(context.Controller.ControllerContext);
            }
            else if (request.AcceptTypes.Contains("text/plain"))
            {
                response.ContentType = "text/plain";
                response.Write(Data);

            }
            else if (request.AcceptTypes.Contains("application/json")) {

                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(response.Output))
                {
                    //var settings = new JsonSerializerSettings();
                    //settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jsonTextWriter, Data);
                    jsonTextWriter.Flush();
                }
            }
            else if (request.AcceptTypes.Contains("text/xml"))
            {
                using (XmlTextWriter writer = new XmlTextWriter(response.Output))
                {
                    writer.Formatting = System.Xml.Formatting.Indented;
                    XmlSerializer serializer = new XmlSerializer(Data.GetType());

                    serializer.Serialize(writer, Data);
                    writer.Flush();
                }
             }
        }
    }
}