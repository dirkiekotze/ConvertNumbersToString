namespace TechnologyOneProject.Infastructure.Alerts
{
    public class Alert
    {
        public Alert(string alertClass, string message, string input)
        {
            AlertClass = alertClass;
            Message = message;
            Input = input;
        }

        public string AlertClass { get; set; }
        public string Message { get; set; }
        public string Input { get; set; }
    }
}