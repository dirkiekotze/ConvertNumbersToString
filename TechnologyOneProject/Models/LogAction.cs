using System;
using System.ComponentModel.DataAnnotations;

namespace TechnologyOneProject.Models
{
    public class LogAction
    {
        public LogAction(string action, string controller, string inputValue, string outputValue)
        {
            Action = action;
            Controller = controller;
            PerformedAt = DateTime.Now;
        }

        [DataType(DataType.Date)]
        public DateTime PerformedAt { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }
        public float InputValue { get; set; }
        public string OutputValue { get; set; }
    }
}