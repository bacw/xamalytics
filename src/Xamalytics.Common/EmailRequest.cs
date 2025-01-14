namespace Xamalytics.Common
{
    public class EmailRequest
    {
        public EmailRequest(List<string> toMail, string fromMail, string fromDisplayName, string subject, string body)
        {
            ToMail = toMail;
            FromMail = fromMail;
            FromDisplayName = fromDisplayName;
            Subject = subject;
            Body = body;
        }

        public string FromMail { get; set; }
        public string FromDisplayName { get; set; }
        public List<string> ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
