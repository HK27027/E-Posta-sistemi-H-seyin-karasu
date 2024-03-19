namespace EMailSending.InAndOutModels
{
    public class MAccount
    {
        public class Form
        {

            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? Password { get; set; }
            public string? Email { get; set; }
            public string? AccountId { get; set;}
        }
        public class Response
        {

            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
        }
        public class Info
        {

            public int? UserCount { get; set; }=0;
            public int? EmailCount { get; set; } = 0;
            public int? EmailSentCount { get; set; } = 0;
        }
    }
}
