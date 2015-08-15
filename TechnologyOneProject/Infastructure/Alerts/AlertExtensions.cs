using System.Collections.Generic;
using System.Web.Mvc;

namespace TechnologyOneProject.Infastructure.Alerts
{
    public static class AlertExtensions
    {
        private const string Alerts = "_Alerts";

        public static List<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<Alert>();
            }

            return (List<Alert>) tempData[Alerts];
        }

        public static ActionResult WithSuccess(this ActionResult result, string message,string input)
        {
            return new AlertDecoratorResult(result, "alert-success", message,input);
        }

        public static ActionResult WithInfo(this ActionResult result, string message,string input)
        {
            return new AlertDecoratorResult(result, "alert-info", message,input);
        }

        public static ActionResult WithWarning(this ActionResult result, string message,string input)
        {
            return new AlertDecoratorResult(result, "alert-warning", message,input);
        }

        public static ActionResult WithError(this ActionResult result, string message,string input)
        {
            return new AlertDecoratorResult(result, "alert-danger", message,input);
        }
    }
}