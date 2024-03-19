

namespace EMailSending.InAndOutModels
{
    public class MUsers
    {
        public class Form
        {

            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Title { get; set; }
            public string? AccountId { get; set; }
            public string? CompanyName { get; set; }
            public string? Search { get; set; }
        }
        public class Response
        {

            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Title { get; set; }
            public string? CompanyName { get; set; }
        }
    }
}
