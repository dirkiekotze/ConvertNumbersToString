using System.Web.Mvc;
using TechnologyOneProject.Models;

namespace TechnologyOneProject.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public string InputString;
        public string OutputString;

        public LogAttribute(string inPutValue, string outPut)
        {
            InputString = inPutValue;
            OutputString = outPut;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var logInfo = new LogAction(filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, InputString, OutputString);

            //Write This to File
        }
    }
}