using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
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

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour AjoutCommandeView.xaml
    /// </summary>
    public partial class AjoutCommandeView : UserControl
    {
        public CommandesViewModel ViewModel { get { return (CommandesViewModel)DataContext; } }
        private bool Erreur;
        private IList<int> lstTable = new List<int>();
        private Brush brushesBase;

        public AjoutCommandeView()
        {
            //<Button Click="btn_Click" Content="Table 3" MinWidth="75" Width="Auto" Margin="10" Padding="25"/>
            InitializeComponent();
            try
            {
                DataContext = new CommandesViewModel();
            }
            catch (Exception e)
            {
                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des tables :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des tables: " + exceptionMessage);
                Erreur = true;
            }

            // Si on a pas eu d'erreur lors du chargement
            if (!Erreur)
            {
                // On boucle pour chaque commandes et on crée un bouton
                foreach (var item in ViewModel.Tables)
                { 
                    // On se définie un nouveau bouton
                    Button bouton = new Button();
                    bouton.Click += btn_Click;
                    bouton.Width = 115;
                    bouton.Height = 85;
                    bouton.Content = item.IdTable;
                    bouton.Name = "Bouton" + Convert.ToString(item.IdTable);
                    bouton.Margin = new Thickness(5);

                    // On ajoute le bouton à notre wrappannel
                    listeTableDisponible.Children.Add(bouton);
                }
            }
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandesView>(new CommandesView());
        }

        private void btnCreer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On crée la commande dans la bd
            Commande commande = new Commande("Active", DateTime.Now);
            commande.IdEmploye = Constante.employe.IdEmploye ?? default(int);
            commande = ViewModel.CreerCommande(commande);

            // On test si la commande à bien reçu son id de création
            if (commande.IdCommande != null)
            {
                // Si elle n'est pas null on redirige à la page de la commande avec la commande en cour
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CommandeView>(new CommandeView(commande));
            }
        }

        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // On test si on a copié la couleur du bouton par défaut
            if (brushesBase == null)
            {
                // On sauvegarde la couleur par défaut du background de windows
                brushesBase = (sender as Button).Background;   
            }

            // On test si la liste contient la table cliqué
            if (lstTable.Contains(Convert.ToInt32((sender as Button).Content)))
            {
                // Si c'est le cas on peut remettre la couleur par défaut parce que le bouton à déjà été cliqué
                (sender as Button).Background = brushesBase;

                // On supprime de la liste le numéro de la table
                lstTable.Remove(Convert.ToInt32((sender as Button).Content));
            }
            else
            {
                // On change la couleur du bouton et on sauvegarde le numéro de la table
                (sender as Button).Background = new SolidColorBrush(Color.FromRgb(135,206,255));
                lstTable.Add(Convert.ToInt32((sender as Button).Content));
            }
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
    }
}
