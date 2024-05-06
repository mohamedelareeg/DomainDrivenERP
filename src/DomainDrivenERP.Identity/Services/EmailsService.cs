using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Identity.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace DomainDrivenERP.Identity.Services;

public class EmailsService : IEmailsService
{
    #region Fields
    private readonly EmailSettings _emailSettings;
    #endregion

    #region Constructors
    public EmailsService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }
    #endregion

    #region Handle Functions

    public async Task<Result<bool>> SendEmail(string email, string message, string? reason, string companyName)
    {
        try
        {
            // Sending a professional and customized email
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);

                // Create a more customized email body
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<p>Dear User,</p>" +
                               $"<p>{message}</p>" +
                               $"<p>Best Regards,<br/>{companyName}</p>",
                    TextBody = $"Dear User,{Environment.NewLine}{message}{Environment.NewLine}Best Regards,{Environment.NewLine}{companyName}"
                };

                var mimeMessage = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };

                mimeMessage.From.Add(new MailboxAddress(companyName, _emailSettings.FromEmail));
                mimeMessage.To.Add(new MailboxAddress("Recipient", email));
                mimeMessage.Subject = reason ?? "No submitted";



                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }

            // End of sending email
            return true;
        }
        catch (Exception)
        {
            return Result.Failure<bool>(new Error("Failed", "Failed to send email."));
        }
    }

    #endregion
}
