namespace ISOtrainSCIMIntegration.ApiWeb.Model
{
    public class ISOtrainEmployeeRequest
    {
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

    }
}
