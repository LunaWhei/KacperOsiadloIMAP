using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MailKit.Net.Smtp;


namespace KacperOsiadloIMAP
{
     
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user = new User();
        bool Is_logged_in = false;

        public MainWindow()
        {
            InitializeComponent();
            if (!Is_logged_in)
            {
                Login_page.IsSelected = true;
            }

        }
        


        private void Save_login_data_Click(object sender, RoutedEventArgs e)
        {
            
            user.Login = Login.Text.ToString();
            user.Password = Password.Text.ToString();
            user.ImapPort = Convert.ToInt32(Imap_port.Text);
            user.Imap = ImapServer.Text.ToString();
            user.Smtp = Smtp.Text.ToString();
            user.EmailAdress = Email.Text.ToString();
            user.SmtpPort = Convert.ToInt32(Smtp_port.Text);
            MailboxPage mailboxpage = new MailboxPage();
            mailboxpage.UserData = user;
            Is_logged_in = true;
            MailboxFrame.Navigate(mailboxpage, user);



        }

        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            
            MailboxPage mailboxpage = new MailboxPage();
            mailboxpage.UserData = user;
            MailboxFrame.Navigate(mailboxpage, user);
        }

        private void New_Mail_Click(object sender, RoutedEventArgs e)
        {
            SendEmailPage p1 = new SendEmailPage();
            p1.UserData = user;
            MailboxFrame.Navigate(p1, user);
            
        }

        private void Settings_Selected(object sender, RoutedEventArgs e)
        {
            if (!Is_logged_in)
            {
                Login_page.IsSelected = true;
            }
        }

        private void Login_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
