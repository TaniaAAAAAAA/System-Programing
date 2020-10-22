using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace DZ_MAIL____
{
    /// <summary>
    /// Interaction logic for NewLetter.xaml
    /// </summary>
    public partial class NewLetter : Window
    {
        public NewLetter()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Smtp();
        }

        MailMessage mail = new MailMessage();
        private void Smtp()
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            if (txtTo.Text != "" && txtPole.Text != "")
            {
                try
                {
                    client.Credentials = new NetworkCredential("projectsprog1@gmail.com", "qqwwee11!!");

                     
                    mail.From = new MailAddress("projectsprog1@gmail.com");

                    mail.To.Add(new MailAddress(txtTo.Text));

                    mail.Subject = txtTitle.Text;
                    mail.Body = txtPole.Text;
                    mail.IsBodyHtml = true;
                    client.SendMailAsync(mail);

                    MessageBox.Show("Letter sended!");
                    txtPole.Text = "";
                    txtTitle.Text = "";
                   // this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    
                   
                }
            }
        }

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private void Button_Click(object sender, RoutedEventArgs e) //Attachments
        {
            if (openFileDialog1.ShowDialog() == false)
                return;

            string filename = openFileDialog1.FileName;

          
            mail.Attachments.Add(new Attachment(filename));
        }




    }
}
