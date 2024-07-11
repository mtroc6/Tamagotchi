namespace WebPWrecover.Services;

public class AuthMessageSenderOptions
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public bool EnableSsl { get; set; }
}
