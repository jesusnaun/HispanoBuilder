using System.ComponentModel.DataAnnotations;

namespace WorkForce.API.Models.State
{
    public class StateRequest
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool Active { get; set; } = true;
    }
}
