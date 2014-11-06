using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour CommandesView.xaml
    /// </summary>
    public partial class CommandesView : UserControl
    {
        public CommandesViewModel ViewModel { get { return (CommandesViewModel)DataContext; } }
        private bool Erreur;

        public CommandesView()
        {
            InitializeComponent();

            try
            {
                DataContext = new CommandesViewModel();
            }
            catch (Exception e)
            {

                StringBuilder messageErreur = new StringBuilder();
                string exceptionMessage = e.InnerException.Message;

                messageErreur.Append("Une erreur s'est produite, il est impossible d'afficher la liste des commandes :\n");
                messageErreur.Append(exceptionMessage);

                MessageBox.Show(messageErreur.ToString(), "Une erreur s'est produite", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Constante.LogErreur("Impossible d'afficher la liste des commandes: " + exceptionMessage);
                Erreur = true;
            }

            if (!Erreur)
            {
                // On boucle pour chaque commandes et on crée un bouton
                foreach (var item in ViewModel.Commandes)
                { 
                    // On se définie un nouveau bouton
                    Button bouton = new Button();
                    bouton.Click += btnDetail_Click;
                    bouton.Width = 115;
                    bouton.Height = 85;

                    // On boucle dans toutes les tables de la commande pour afficher le bon nom
                    StringBuilder chaine = new StringBuilder();
                    int i = 0;

                    // Boucle parcourant la liste de table
                    foreach (var table in item.ListeTables)
                    {
                        // Si ce n'est pas le premier élément on ajoute une virgule
                        if (i > 0)
                        {
		                    chaine.Append(",");
                        }
                        chaine.Append(table.NoTable);
                        i++;
                    }

                    bouton.Content = chaine;
                    bouton.Name = "Bouton" + Convert.ToString(item.IdCommande);
                    bouton.Margin = new Thickness(5);

                    // On ajoute le bouton à notre wrappannel
                    listeBoutonCommandes.Children.Add(bouton);
                }
            }
        }

        private void btnRapport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Rapport");
        }

        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjoutCommandeView>(new AjoutCommandeView());
        }

        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            // On prend le contenu du bouton et on l'ajoute
            string content = (sender as Button).Name.ToString();

            // On va chercher le id
            content = content.Substring(6);

            // On enregistre le id de la commande
            int idCommande = Convert.ToInt32(content);

            // On se définie une commande null temporaire
            Commande commandeTemp = null;

            // On boucle dans chaque commande pour trouver la commande qu'on cherche
            foreach (var item in ViewModel.Commandes)
            {
                // Si on a trouvé la commande
                if (item.IdCommande == idCommande)
                {
                    // On sauvegarde la commande
                    commandeTemp = item;

                    // On arrête l'exécution de la boucle
                    break;
                }
            }

            // On teste si la commande n'est pas null.
            if (commandeTemp != null)
            {
                // Si elle n'est pas null on redirige à la page de la commande avec la commande en cour
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CommandeView>(new CommandeView(commandeTemp));
            }
            else
            {
                // On affiche un message si la commande n'existe pas
                MessageBox.Show("La commande que vous voulez modifier n'existe pas. \n Veuillez réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Constante.LogErreur("La commande n'est pas existante dans la base de données. Il faut réessayer");
            }
        }
    }
}
