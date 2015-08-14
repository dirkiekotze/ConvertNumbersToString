using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyOneProject.Models
{
    public class InputNumber
    {
        [Required]
        public double Number { get; set; }
    }
}