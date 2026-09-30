using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class Seance3 : Page
    {
        private readonly ObservableCollection<Tache> taches = new ObservableCollection<Tache>(); // Liste observable pour la liaison avec l'interface utilisateur.

        private readonly string fichier = System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "taches.txt"); // Chemin du fichier de sauvegarde dans le dossier du projet

        public Seance3()
        {
            InitializeComponent();
            TachesListBox.ItemsSource = taches; //liaison entre liste obs et listbox
            ChargerTaches();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e) // Ajoute une nouvelle tâche à la liste
        {
            string texte = NouvelleTacheTextBox.Text.Trim(); //le text entré recup 
            if (texte == "")
                return;
            taches.Add(new Tache
            {
                Texte = texte,
                IsDone = false // set son status a pas fait direct
            });
            NouvelleTacheTextBox.Clear();
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)// le usprimer de la lsite
        {
            if (TachesListBox.SelectedItem is Tache tache)
                taches.Remove(tache);
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)//l'enregistrer dans la liste
        {
            try
            {
                string[] lignes = taches.Select(t =>
                    (t.IsDone ? "1" : "0") + "|" + t.Texte).ToArray();//si 1 fait ou pas fait et le texte de la tache avec separateur puis nom de la tache
                File.WriteAllLines(fichier, lignes);
                MessageBox.Show("Liste mise a jour.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ereur : " + ex.Message);
            }
        }

        private void ChargerTaches() //charge les taches du fichier dans la liste
        {
            if (!File.Exists(fichier))//si le fichier n'existe pas on quitte la fonction
                return;

            try
            {
                foreach (string ligne in File.ReadAllLines(fichier))//on lit chaque ligne du fichier
                {
                    int separateur = ligne.IndexOf('|');

                    if (separateur >= 0)
                    {
                        string etat = ligne.Substring(0, separateur);
                        string texte = ligne.Substring(separateur + 1);

                        taches.Add(new Tache
                        {
                            Texte = texte,
                            IsDone = etat == "1"
                        });
                    }
                    else
                    {
                        taches.Add(new Tache
                        {
                            Texte = ligne,
                            IsDone = false
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ereur: " + ex.Message);
            }
        }
    }

    public class Tache : INotifyPropertyChanged // sert a notifier changement d'état de la tache
    {
        private string _texte = "";
        private bool _isDone;

        public string Texte
        {
            get => _texte;//recup le texte de la tache
            set//set le texte de la tache
            {
                if (_texte == value)//si le texte est le meme que celui qu'on veut mettre on quitte la fonction
                    return;

                _texte = value;
                OnPropertyChanged();
            }
        }

        public bool IsDone//recup le status de la tache
        {
            get => _isDone;//recup le status de la tache
            set
            {
                if (_isDone == value)// si le status est le meme que celui qu'on veut mettre on quitte la fonction
                    return;

                _isDone = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;//event qui notifie les changements de propriété

        private void OnPropertyChanged(
            [CallerMemberName] string nom = null)//notifie les changements de propriété
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nom));//si l'event est null on ne fait rien sinon on notifie le changement de propriété
        }
    }

    public class IsDoneConverter : IValueConverter//convertit le status de la tache en couleur ou decoration
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool terminee = value is bool etat && etat;

            if ((parameter as string) == "Decoration")
            {
                return terminee ? TextDecorations.Strikethrough : null; //si la tache est faite on met une ligne sur le texte sinon on ne met rien
            }

            return terminee ? Brushes.Gray : Brushes.Black;//si la tache est faite on met le texte en gris sinon on le met en noir
        }

        public object ConvertBack(//on ne convertit pas en arrière
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();//on ne convertit pas en arrière
        }
    }
}