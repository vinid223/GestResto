using GestResto.Logic.Model.Entities;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using GestResto.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour CommandeView.xaml
    /// </summary>
    public partial class CommandeView : UserControl
    {
        
        public CommandeViewModel ViewModel { get { return (CommandeViewModel)DataContext; } }
        int Scroll = 0;
        int HauteurListView;
        List<Item> ListeItemsTemp = new List<Item>();
        /// <summary>
        /// NuméroClient permet de sauvegarder la liste du client actuellement affichée. Au départ, on affiche la liste du premier client.
        /// </summary>
        int NumeroClient = 0;
        /// <summary>
        /// Permet de sauvegarder la commande passée en paramètre dans le constructeur de la commandeView.
        /// </summary>

        public CommandeView(Commande uneCommande = null)
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();

            DataContext = new CommandeViewModel();

            lbxListeItems.ItemsSource = ViewModel.Items;

            ViewModel.LaCommande = uneCommande;
            if(uneCommande.ListeClients != null && uneCommande.ListeClients.Count > 0)
            { 
                lbxItemsClient.ItemsSource = uneCommande.ListeClients.First().ListeFormatItemClientFacture;
            }

            // On change le numéro de client sur l'écran
            lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;
        }



        #region Fonctions pour la liste d'items du client

        /// <summary>
        /// Lorsque l'utilisateur clique sur un item, je l'ajoute à sa liste d'items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)((sender as Button).CommandParameter);
            
            // Je dois afficher les formats disponibles de l'item.
            FormatsView view = new FormatsView(item.Formats);
            view.ShowDialog();

            // On initialise le nouveau ficf
            FormatItemClientFacture ficf = new FormatItemClientFacture();
            // Copie de l'item associé.
            ficf.FormatItemAssocie = view.formatItemChoisi;
            // Copie du client
            ficf.client = g_Client();
            // Copie de la facture.
            ficf.facture = ficf.client.FactureClient;
            // Enregistrement du prix.
            ficf.Prix = ficf.FormatItemAssocie.Prix;

            ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture.Add(ficf);

            // Refresh de la liste d'items du client
            lbxItemsClient.ItemsSource = g_Client().ListeFormatItemClientFacture;
            lbxItemsClient.Items.Refresh();

            // Ajout à la BD.
            ViewModel.AjouterUnFicf(ficf);
        }

        /// <summary>
        /// Retourne le client qui est actuellement visionné dans l'écran.
        /// </summary>
        private Client g_Client()
        {
            return ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient);
        }

        private void btnSuivantClient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si cette condition est respectée je peux afficher le client suivant
            if (NumeroClient <= ViewModel.LaCommande.ListeClients.Count - 1)
            {
                // Si on avance de client, on s'assure que le bouton précédent est Enabled.
                btnClientPrecedent.IsEnabled = true;

                NumeroClient += 1;

                // On change le numéro de client sur l'écran
                lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;

                lbxItemsClient.ItemsSource = ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture;
                lbxItemsClient.Items.Refresh();

                // Je vérifie si on a atteint la fin de la liste des clients.
                if (NumeroClient == ViewModel.LaCommande.ListeClients.Count - 1)
                    btnClientSuivant.IsEnabled = false;
                
            }
            else
                btnClientSuivant.IsEnabled = false;

        }
        private void btnPrecedentClient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si le numéroClient est supérieur à 0, on peut reculer de client.
            if (NumeroClient > 0)
            {
                // Si on recule de client, on s'assure que le bouton suivant est Enabled.
                btnClientSuivant.IsEnabled = true;

                NumeroClient -= 1;

                // On change le numéro de client sur l'écran
                lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;

                lbxItemsClient.ItemsSource = ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture;
                lbxItemsClient.Items.Refresh();

                // Je vérifie si on a atteint le début de la liste des clients.
                if (NumeroClient <= 0)
                    btnClientPrecedent.IsEnabled = false;
            }
            else
                btnClientPrecedent.IsEnabled = false;
        }
        #endregion

        #region Fonctions pour la liste de catégories à droite de l'écran

        /// <summary>
        /// Lorsque l'utilisateur clique sur une catégorie, j'affiche les items de la catégorie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCategorie_Click(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)((sender as Button).CommandParameter);
            ViewModel.Categorie = categorie;
            lbxListeCategorie.SelectedItem = categorie;


            lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.IdCategorie == categorie.IdCategorie).ToList();

        }

        private void btnMonterCategorie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Categorie categ = (Categorie)lbxListeCategorie.SelectedItem;

            // J'obtiens environ le nombre de boutons actuellement visibles
            HauteurListView = (int)lbxListeCategorie.ActualHeight / 40;

            if (Scroll >= HauteurListView)
                Scroll -= HauteurListView;
            else
                Scroll = 0;

            lbxListeCategorie.SelectedIndex = Scroll;
            lbxListeCategorie.ScrollIntoView(lbxListeCategorie.SelectedItem);

        }

        private void btnDescendreCategorie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Categorie categ = (Categorie)lbxListeCategorie.SelectedItem;

            // J'obtiens environ le nombre de boutons actuellement visibles
            HauteurListView = (int)lbxListeCategorie.ActualHeight / 40;


            if (Scroll < lbxListeCategorie.Items.Count - HauteurListView)
                Scroll += HauteurListView;
            else
                Scroll = lbxListeCategorie.Items.Count-1;

            lbxListeCategorie.SelectedIndex = Scroll;
            lbxListeCategorie.ScrollIntoView(lbxListeCategorie.SelectedItem);

        }
        #endregion

        #region Boutons au bas de la pages pour le contrôle de la navigation et autre.

        private void btnAjouter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); /// On enlève l'effet du bouton pressé
            MessageBox.Show("Ajouter");
        }

        private void btnDiviser_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); /// On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<DivisionFactureView>(new DivisionFactureView());
        }

        private void btnImprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Imprimer");
        }

        private void btnSupprimer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            MessageBox.Show("Supprimer");
        }

        private void btnPayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<PaiementView>(new PaiementView(ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient)));
        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandesView>(new CommandesView());
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }
        #endregion

        
    }
}
