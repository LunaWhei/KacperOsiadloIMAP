using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using KacperOsiadloIMAP.Models;
using MailKit.Security;
using MailKit;
using MailKit.Search;

namespace KacperOsiadloIMAP.Services
{
    public class ImapService
    {
        public ImapService(User user)
        {
            this.user = user;
        }

        User user;

        public IList<UniqueId> GetAllUids()
        {
            using (var client = new ImapClient())
            {
                client.Connect(user.Imap, user.ImapPort, SecureSocketOptions.Auto);
                client.Authenticate(user.Login, user.Password);

                client.Inbox.Open(FolderAccess.ReadOnly);
                var uids = client.Inbox.Search(SearchQuery.All);
                client.Disconnect(true);

                return uids;


            }
        }
        public MailMessage GetMessageByUid(UniqueId uid)
        {
            var MessageList = new MailMessage();
            using (var client = new ImapClient())
            {
                client.Connect(user.Imap, user.ImapPort, SecureSocketOptions.Auto);
                client.Authenticate(user.Login, user.Password);

                client.Inbox.Open(FolderAccess.ReadOnly);
                var message = client.Inbox.GetMessage(uid);
                MessageList = new MailMessage { From = message.From, Subject = message.Subject, Date = message.Date.ToString() };

                client.Disconnect(true);
                 return MessageList;

            }
        }

    }
}

