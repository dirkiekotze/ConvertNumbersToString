using System.ComponentModel.DataAnnotations;

namespace TechnologyOneProject.Models
{
    public class InputNumber
    {
        [Required]
        public double Number { get; set; }
    }
}