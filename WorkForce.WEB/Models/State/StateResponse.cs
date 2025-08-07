namespace WorkForce.WEB.Models.State
{
    public class StateResponse
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
