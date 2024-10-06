using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using zaloclone_test.Configurations;

namespace zaloclone_test.Utilities
{
    public static class EmailHandler
    {
        private static readonly string EmailDisplayName = ConfigManager.gI().EmailDisplayName;
        private static readonly string EmailHost = ConfigManager.gI().EmailHost;
        private static readonly string EmailUsername = ConfigManager.gI().EmailUsername;
        private static readonly string EmailPassword = ConfigManager.gI().EmailPassword;

        public static string SendEmail(string To, string Subject, string Body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(EmailDisplayName, EmailUsername));
            email.To.Add(MailboxAddress.Parse(To));
            email.Subject = Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = Body };

            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(EmailUsername, EmailPassword);
                smtp.Send(email);
            }
            catch (Exception e)
            {
                return $"{e.Message}: inner: {e.InnerException}";
            }
            finally { smtp.Disconnect(true); }

            return "";
        }

    }
}
