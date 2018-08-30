using System;
using System.Collections.Generic;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;

/*
 * In order to send email via gmail, you will need to do one of two things.
 * 1. Enable less secure apps (https://www.google.com/settings/security/lesssecureapps)
 * 2. Obtain OAuth 2.0 credentials for your app (https://developers.google.com/accounts/docs/OAuth2)
 * This example assumes you have enabled less secure apps.
 */

namespace MailkitExample
{
    public class Program
    {
        private const bool SendEmail = false;

        private const string Subject = "Email Subject";

        private static Dictionary<string, string> Recipients = new Dictionary<string, string>
        {
            {"Random Person", "random.person@internet.com"},
        };

        public static void Main(string[] args)
        {
            var message = new MimeMessage
            {
                Subject = Subject,
                Body = GenerateMessageBody()
            };

            message.From.Add(new MailboxAddress(Constants.GmailDisplayName, Constants.GmailUsername));

            foreach (var pair in Recipients)
            {
                message.To.Add(new MailboxAddress(pair.Key, pair.Value));
            }

            // Write out the Outlook file
            using (var output = new StreamWriter("output.eml"))
            {
                message.WriteTo(output.BaseStream);
            }

            // Write out the HTML file
            using (var output = new StreamWriter("output.html"))
            {
                output.Write(message.HtmlBody);
            }

            // Send the email via gmail
            if (SendEmail)
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(Constants.GmailUsername, Constants.GmailPassword);

                    //Remove any OAuth functionality as we won't be using it. 
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static MimeEntity GenerateMessageBody()
        {
            var builder = new BodyBuilder();

            // If you want a text part, add it here as well.

            // Add embedded images to the email here.
            var image = builder.LinkedResources.Add("image.png");
            image.ContentId = MimeUtils.GenerateMessageId();

            // Read in the template and do substitutions
            using (var input = new StreamReader("template.html"))
            {
                var template = input.ReadToEnd();
                builder.HtmlBody = template
                    .Replace("${Logo}", image.ContentId)
                    .Replace("${FirstName}", "Rando")
                    .Replace("${LinkUrl}", "http://www.github.com")
                    .Replace("${Subject}", "Why I Don't Like You");
            }

            return builder.ToMessageBody();
        }
    }
}
