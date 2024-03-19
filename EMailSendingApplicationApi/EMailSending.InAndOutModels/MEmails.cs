namespace EMailSending.InAndOutModels
{
    public class MEmails
    {
        public class Form
        {
            public string? Id { get; set; }
            public string? ServerName { get; set; }
            public string? Password { get; set; }
            public string? Username { get; set; }
            public string? PortNumber { get; set; }
            public string? AccountId { get; set; }
            public string? Search { get; set; }
        }
        public class Response
        {

            public string? Id { get; set; }
            public string? ServerName { get; set; }
            public string? Username { get; set; }
            public string? PortNumber { get; set; }
            public string? AccountId { get; set; }
            public string? Password { get; set; }
        }
    }
}
