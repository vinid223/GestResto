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

        // Définition des variables nécessaire pour l'écran
        private bool Erreur;
        private IList<int> lstTable = new List<int>();
        private Brush brushesBase;

        public AjoutCommandeView()
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();
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
                // Si nous avons pas de table dans la bd on affiche un message et on ne fait pas de boucle
                if (ViewModel.Tables.Count > 0)
                {
                    // On se crée une table temporaire pour effectuer la suppression sans problème
                    List<Table> tableTemp = ViewModel.Tables.ToList();


                    // On boucle dans toutes les tables
                    foreach (var item in tableTemp)
                    {
                        // Si la table est assigné à une commande on ne l'affiche pas
                        if (item.EstAssigne == true)
                        {
                            // On supprime de la liste la table qui est déjà en cour d'utilisation
                            ViewModel.Tables.Remove(item);
                        }
                    }

                    // On resauvegarde la liste des tables
                    tableTemp = ViewModel.Tables.ToList();

                    // On boucle pour chacune des table pour vérifier si elle est active
                    foreach (var item in tableTemp)
                    {
                        // Si la table n'est pas active on ne l'affiche pas
                        if (item.EstActif == false)
                        {
                            // On supprime de la liste la table qui n'est pas active
                            ViewModel.Tables.Remove(item);
                        }
                    }

                    // S'il n'y a pas de table de disponible après l'analyse des tables on affiche un message
                    if (ViewModel.Tables.Count == 0)
                    {
                        MessageBox.Show("Toutes les tables sont présentement prises. Veuillez terminer une commande en cours pour libérer une ou plusieurs tables.", "Aucune table disponible.", MessageBoxButton.OK, MessageBoxImage.Information);    
                    }

                    // On donne la source des tables à notre liste de table
                    listeTableDisponible.ItemsSource = ViewModel.Tables;
                }
                else
                {
                    // On affiche un message à l'écran de l'usager et on fait une entrée dans le fichier de log
                    Constante.LogErreur("Aucune table dans la base de données. Le serveur ne peut pas créer de commande");
                    MessageBox.Show("Aucune table dans la base de données. Veuillez contacter un administrateur.", "Aucune table disponible.", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // On test si l'utilisateur à cliqué sur au moins une table
            if (lstTable.Count > 0)
            {
                // On crée la commande dans la bd
                Constante.commande = new Commande("Active", DateTime.Now);
                Constante.commande.IdEmploye = Constante.employe.IdEmploye ?? default(int);

                // On se crée une liste temporaire pour contenir toutes les tables sélectionné
                // On a pas la choix ici d'utuliser le namespace au complet pour accéder à l'objet de table puisque
                // table entre en confusion avec une classe table du système
                IList<GestResto.Logic.Model.Entities.Table> listTableTemp = new List<GestResto.Logic.Model.Entities.Table>();

                // On boucle dans chacune des tables de notre ViewModel
                foreach (var item in ViewModel.Tables)
                {
                    // Si le numéro de la table en cour correspond à une des tables sélectionné on la sauvegarde
                    if (lstTable.Contains(item.NoTable))
                    {
                        item.EstAssigne = true;
                        listTableTemp.Add(item);
                    }
                }

                // On définie la liste de table à notre objet de commande
                Constante.commande.ListeTables = listTableTemp;

                // On enregistre la nouvelle commande et on reçoit le résultat de l'insertion
                Constante.commande = ViewModel.CreerCommande(Constante.commande);

                // On test si la commande à bien reçu son id de création
                if (Constante.commande.IdCommande != null)
                {
                    // Si elle n'est pas null on redirige à la page de la commande avec la commande en cour
                    IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                    mainVM.ChangeView<CommandeView>(new CommandeView());
                }
                else
                {
                    // On affiche un message à l'utilisateur
                    MessageBox.Show("Un problème est survenu lors de la création de la commande", "Erreur de création de commande", MessageBoxButton.OK, MessageBoxImage.Error);
                    Constante.LogErreur("Un problème est survenu lors de la création d'une commande");

                    // On redirige l'utilisateur à la liste des commandes puisqu'il y a eu un problème
                    IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                    mainVM.ChangeView<CommandesView>(new CommandesView());
                }   
            }
            else
            {
                // On affiche un message si l'utilisateur à cliqué sur le bouton créer sans avoir sélectionné une ou plusieurs tables
                Constante.LogNavigation("L'utilisateur tente de créer une commande sans avoir sélectionné une table");
                MessageBox.Show("Veuillez choisir une table avant de créer une commande. S'il n'y a pas de table de disponible, veuillez fermer une commande en cours pour libérer une table.", "Aucune table sélectionnée",MessageBoxButton.OK,MessageBoxImage.Information);
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
