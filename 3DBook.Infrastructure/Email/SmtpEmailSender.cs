using System.Net.Mail;
using _3DBook.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace _3DBook.Infrastructure.Email;

public class SmtpEmailSender(ILogger<SmtpEmailSender> logger,IOptions<MailServerConfiguration> mailServerOptions)  : IEmailSender
{
    private ILogger<SmtpEmailSender> _logger = logger;
    private MailServerConfiguration _mailServerConfiguration = mailServerOptions.Value;

    /// <inheritdoc />
    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        var emailClient = new SmtpClient(_mailServerConfiguration.Hostname, _mailServerConfiguration.Port);
        var message = new MailMessage
        {
            From = new MailAddress("from"),
            Subject = subject,
            Body = body
        };
        message.To.Add(new MailAddress(to));
        await emailClient.SendMailAsync(message);
        _logger.LogWarning("Sending email to {to} from {from} with subject {subject}.",to,from,subject);
    }
}