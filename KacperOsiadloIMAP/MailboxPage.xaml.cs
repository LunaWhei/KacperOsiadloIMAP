using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KacperOsiadloIMAP.Models;
using MailKit.Net.Imap;
using KacperOsiadloIMAP.Services;

namespace KacperOsiadloIMAP
{
    /// <summary>
    /// Interaction logic for MailboxPage.xaml
    /// </summary>
    public partial class MailboxPage : Page
    {
        public MailboxPage()
        {
            InitializeComponent();
        }
        User user;

        public User UserData
        {
            get { return user; }
            set { user = value; }
        }
        List<MailMessage> MailMessageList = new List<MailMessage>();

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            ImapService imapService = new ImapService(user);
            MailMessage mailMessages = new MailMessage();
            IList<MailKit.UniqueId> uids = imapService.GetAllUids();
            foreach (var uid in uids)
            {
                await Task.Run(() => {
                    mailMessages = imapService.GetMessageByUid(uid);
                    MailMessage mail = imapService.GetMessageByUid(uid);
                    this.Dispatcher.Invoke(() =>
                    {

                        Mailbox_list.Items.Add(new MailMessage() { Subject = mailMessages.Subject, Date = mailMessages.Date, From = mailMessages.From });

                    });
                });
                 
            }
            
            





        }
    }
}
