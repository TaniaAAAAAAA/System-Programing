using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Kit = MailKit.Net.Smtp;

namespace DZ_MAIL____
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLogin.Focus();
          
        }

        private  void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Smtp();


        }

       
        private void Smtp()
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            if (txtLogin.Text != "" && txtPassword.Password != "")
            {
                try
                {
                    client.Credentials = new NetworkCredential(txtLogin.Text, txtPassword.Password);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(txtLogin.Text);

                    mail.To.Add(new MailAddress(txtLogin.Text));

                    mail.Subject = "Connected";
                    mail.Body = "Connected";
                    mail.IsBodyHtml = true;
                    client.SendMailAsync(mail);

                    Had had = new Had();
                    had.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    txtLogin.Text = "";
                    txtPassword.Password = "";
                }
            }
        }
    }
}
