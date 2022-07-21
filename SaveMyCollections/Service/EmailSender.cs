using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;

namespace SaveMyCollections.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {          
            await Execute(subject, message, toEmail);
        }

        public async Task Execute( string subject, string message, string toEmail)
        {
            string to = toEmail;
            string from = "noreply@savemycollections.com";
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Host = "mail.savemycollections.com";
            client.Port = 8889;
            //client.EnableSsl = true;
            client.Credentials = new NetworkCredential("noreply@savemycollections.com", "#r$9ib745U6KEpV");

            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string msg = "Send Outllok Mail Token";
            try
            {
                client.SendAsync(mailMessage, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                var message = string.Format("[{0}] Send canceled.", token);
                _logger.LogInformation(message);
            }
            if (e.Error != null)
            {
                var message = string.Format("[{0}] {1}", token, e.Error.ToString());
                _logger.LogInformation(message);
            }
            else
            {
                _logger.LogInformation("Message sent.");
            }
            
        }
    }
}
