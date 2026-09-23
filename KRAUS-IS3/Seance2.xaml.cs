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

namespace KRAUS_IS3
{
    /// <summary>
    /// Logique d'interaction pour Seance2.xaml
    /// </summary>
    public partial class Seance2 : Page
    {
        public Seance2()
        {
            InitializeComponent();
        }
        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(txtEmailExpediteur.Text);
                mail.To.Add(txtEmailDestinataire.Text);
                mail.Subject = txtObjet.Text;
                mail.Body = txtMessage.Text;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(txtEmailExpediteur.Text, txtPassword.Password);
                smtp.EnableSsl = true;

                smtp.Send(mail);

                txtStatus.Foreground = Brushes.Green;
                txtStatus.Text = "Mail envoyé !";
            }
            catch (Exception ex)
            {
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = "Erreur lors de l'envoi : " + ex.Message;
            }
        }
    }
}
