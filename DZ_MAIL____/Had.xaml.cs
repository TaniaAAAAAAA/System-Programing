using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DZ_MAIL____
{
    /// <summary>
    /// Interaction logic for Had.xaml
    /// </summary>
    public partial class Had : Window
    {
        private static ObservableCollection<string> letters = new ObservableCollection<string>();
        public Had()
        {
            InitializeComponent();
            lb_Letters.ItemsSource = letters;
            DowloadLetters();

        }



        private async static void DowloadLetters()
        {
            using (ImapClient imap = new ImapClient())
            {
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                imap.Authenticate("projectsprog1@gmail.com", "qqwwee11!!");
                //if (imap.IsAuthenticated)
                //{
                var inbox = imap.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                try
                {
                    for (int i = 0; i <= inbox.Count / 10; i++)
                    {
                        MimeMessage message = inbox.GetMessage(i);
                        letters.Add(i + " " + message.Subject + "-->" + message.From.Mailboxes.First().Name + " | " + message.Date);

                    }

                }
                catch { }
                //}
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            letters.Clear();
            DowloadLetters();
        }

        private void btnNewLetter_Click(object sender, RoutedEventArgs e)
        {
            NewLetter newLetter = new NewLetter();
            newLetter.Show();

        }

        private void lb_Letters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lb_Letters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DowloadONELetter();
        }

        private async void DowloadONELetter()
        {
            using (ImapClient imap = new ImapClient())
            {
                await imap.ConnectAsync("imap.gmail.com", 993, true);
                imap.Authenticate("projectsprog1@gmail.com", "qqwwee11!!");
                //if (imap.IsAuthenticated)
                //{
                var inbox = imap.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                try
                {
                    for (int i = 0; i <= inbox.Count / 10; i++)
                    {
                        if (lb_Letters.SelectedIndex == i)
                        {
                            MimeMessage message = inbox.GetMessage(i);
                            letters.Clear();
                            ShowLetter showLetter = new ShowLetter();
                            showLetter.txtFrom2.Text = message.From.Mailboxes.First().Name;
                            showLetter.txtTitle2.Text = message.Subject;
                            showLetter.txtDate2.Text = message.Date.ToString();
                            showLetter.txtPole2.Text = message.Body.ToString();
                            showLetter.Show();

                        }

                    }

                }
                catch { }
                //}
            }
        }
    }
}
