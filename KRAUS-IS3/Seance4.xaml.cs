using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
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
    public partial class Seance4 : Page, INotifyPropertyChanged
    {
        private int secondes = 0; // Nombre de secondes écoulées
        private bool enCours = false; //chrono en cours ou pas
        private readonly DispatcherTimer timer = new DispatcherTimer(); // Timer pour mettre à jour le chrono toutes les secondes

        public string Temps => $"{secondes / 60:00}:{secondes % 60:00}";//format en minutes et sec
        public double Angle => secondes % 60 * 6;//ange, 6 degrés par seconde

        public ICommand Demarrer { get; }
        public ICommand Arreter { get; }
        public ICommand Reinitialiser { get; }

        public Seance4()
        {
            InitializeComponent();

            Demarrer = new RelayCommand( 
                () =>
                {
                    enCours = true;
                    timer.Start();
                    Actualiser();
                },
                () => !enCours);// sert a savoir si le bouton doit etre actif ou pas

            Arreter = new RelayCommand(
                () =>
                {
                    timer.Stop();
                    enCours = false;
                    Actualiser();
                },
                () => enCours);// sert a savoir si le bouton doit etre actif ou pas

            Reinitialiser = new RelayCommand(
                () =>
                {
                    secondes = 0;
                    Actualiser();
                },
                () => !enCours && secondes > 0);// sert a savoir si le bouton doit etre actif ou pas

            timer.Interval = TimeSpan.FromSeconds(1); // Intervalle d'une seconde
            timer.Tick += (s, e) =>
            {
                secondes++;
                Actualiser();
            };// Met à jour le chrono toutes les secondes

            DataContext = this;
        }

        private void Actualiser()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temps)));// Met à jour l'affichage du temps
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Angle)));// Met à jour l'affichage de l'angle
            CommandManager.InvalidateRequerySuggested();// Met à jour l'état des boutons
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RelayCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> peutExecuter;// Fonction qui détermine si la commande peut être exécutée

        public RelayCommand(Action action, Func<bool> peutExecuter)// Constructeur de la commande
        {
            this.action = action;
            this.peutExecuter = peutExecuter;
        }

        public bool CanExecute(object parameter) => peutExecuter();

        public void Execute(object parameter) => action();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }// Permet de mettre à jour l'état des boutons
            remove { CommandManager.RequerySuggested -= value; }// Permet de mettre à jour l'état des boutons
        }
    }
}