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
                MailMessage mail = new MailMessage(); // Création d'un nouvel objet MailMessage
                mail.From = new MailAddress(txtEmailExpediteur.Text); // Définition de l'adresse e-mail de l'expéditeur
                mail.To.Add(txtEmailDestinataire.Text); // Ajout de l'adresse e-mail du destinataire
                mail.Subject = txtObjet.Text; // Définition de l'objet du mail
                mail.Body = txtMessage.Text;// Définition du corps du mail

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //pour google
                smtp.Credentials = new NetworkCredential(txtEmailExpediteur.Text, txtPassword.Password); // Définition de mdp etc
                smtp.EnableSsl = true; //ssl pour securité

                smtp.Send(mail);

                txtStatus.Foreground = Brushes.Green; // Changement de la couleur du texte pour indiquer le succès
                txtStatus.Text = "Mail envoyé !";
            }
            catch (Exception ex)// sinon erreur
            {
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = "Erreur : " + ex.Message;
            }
        }
    }
}
