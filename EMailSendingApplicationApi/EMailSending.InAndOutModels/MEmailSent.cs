namespace EMailSending.InAndOutModels
{
    public class MEmailSent
    {
        public class Form
        {
            public string? Id { get; set; }
            public string? UserId { get; set; }
            public string? Title { get; set; }
            public string? Content { get; set; }
            public string? EMailId { get; set; }
            public string? Search { get; set; }
            public string? AccountId { get; set; }
            public List<MUsers.Response>? UserIds { get; set; }
        }
        public class Response
        {

            public string? Id { get; set; }
            public string? UserId { get; set; }
            public string? UserName { get; set; }
            public string? Title { get; set; }
            public string? Content { get; set; }
            public string? EMailId { get; set; }
            public string? EmailName { get; set; }
            public string? AccountId { get; set; }
        }
    }
}
