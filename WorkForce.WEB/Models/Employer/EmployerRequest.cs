using System.ComponentModel.DataAnnotations;

namespace WorkForce.WEB.Models.Employer
{
    public class EmployerRequest
    {
        [Required]
        public string ContactName { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9\-]+$",
        ErrorMessage = "Phone must contain only numbers")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a state")]
        public int StateId { get; set; }

        public string County { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

    }
}



