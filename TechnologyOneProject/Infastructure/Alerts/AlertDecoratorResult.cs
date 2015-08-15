using System.Web.Mvc;

namespace TechnologyOneProject.Infastructure.Alerts
{
    public class AlertDecoratorResult : ActionResult
    {
        public AlertDecoratorResult(ActionResult innerResult,
            string alertClass,
            string message,
            string input)
        {
            InnerResult = innerResult;
            AlertClass = alertClass;
            Message = message;
            Input = input;
        }

        public ActionResult InnerResult { get; set; }
        public string AlertClass { get; set; }
        public string Message { get; set; }

        public string Input { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetAlerts();
            alerts.Add(new Alert(AlertClass, Message,Input));
            InnerResult.ExecuteResult(context);
        }
    }
}