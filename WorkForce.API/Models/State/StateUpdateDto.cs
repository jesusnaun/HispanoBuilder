using System.ComponentModel.DataAnnotations;

namespace WorkForce.API.Models.State
{
    public class StateUpdateDto
    {
        [StringLength(10)]
        public string? Code { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        public bool? Active { get; set; }
    }
}
