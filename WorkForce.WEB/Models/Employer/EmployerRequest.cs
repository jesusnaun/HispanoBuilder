namespace WorkForce.WEB.Models.Employer
{
    public class EmployerRequest
    {
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int StateId { get; set; }
        public string County { get; set; }
        public string Message { get; set; }
    }
}
