using FetchDailyReport.Model;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport.Utility
{
    class MailUtility
    {
        public static void SendEmailReport(string subject, string emailMessage, string attachedfilePath = null)
        {
            string emailConfigJson = File.ReadAllText(URLs.EmailConfigFilePath);
            EmailModel mail = JsonConvert.DeserializeObject<EmailModel>(emailConfigJson);

            var message = new MimeMessage();

            mail.EmailSubject = subject;

            message.From.Add(new MailboxAddress(mail.FromName, mail.FromEmail));
            message.To.Add(new MailboxAddress(mail.ToName, mail.ToEmail));

            // Add CCs
            List<string> bccNames = new List<string>(mail.MailCcs.Keys);
            foreach (var name in bccNames)
            {
                message.Cc.Add(new MailboxAddress(name, mail.MailCcs[name]));
            }

            message.Subject = mail.EmailSubject;
            message.Body = new TextPart("plain")
            {
                Text = emailMessage
            };

            var body = new TextPart("plain")
            {
                Text = emailMessage
            };
            var multipart = new Multipart("mixed");
            multipart.Add(body);

            if (attachedfilePath != null)
            {
                var attachment = new MimePart("text", "csv")
                {
                    ContentObject = new ContentObject(File.OpenRead(attachedfilePath), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(attachedfilePath)
                };
                multipart.Add(attachment);
            }
            message.Body = multipart;

            // Send the mail
            using (var client = new SmtpClient())
            {
                client.Connect(mail.SmtpServer, mail.SmtpPort, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(mail.FromLoginId, mail.FromLoginPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
