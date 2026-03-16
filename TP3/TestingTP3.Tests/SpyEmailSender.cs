using TestingTP3;

public class SpyEmailSender : IEmailSender
{
    public List<(string To, string Subject, string Body)> SentEmails
    {
        get;
    } = new();
    public void Send(string to, string subject, string body)
    {
        SentEmails.Add((to, subject, body));
    }
}