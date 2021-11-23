using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace KacperOsiadloIMAP.Models
{

    //TODO Move this to services
    class SmtpService
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        private User user { get; set; }

        public SmtpService (User user)
        {

            this.user = user;


        }

        //Sending messages fine
        public async Task SendEmail()
        {
            using (var client = new SmtpClient())
            {

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(user.Identity, user.EmailAdress));
                    message.To.Add(new MailboxAddress("carl", To));
                    message.Subject = Subject;

                    message.Body = new TextPart("plain")
                    {
                        Text = Body
                    };
                    client.Connect(user.Smtp, user.SmtpPort, SecureSocketOptions.Auto);
                   await client.AuthenticateAsync(user.Login, user.Password);
                if (client.IsAuthenticated)
                {
                    client.Send(message);
                    client.Disconnect(true);
                }



            }
        } 
    }
}
