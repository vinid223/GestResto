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

        public CommandeView()
        {
            InitializeComponent();
            
            lblNom.Content = Constante.employe.ToString();

            DataContext = new CommandeViewModel();

            lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.EstComplementaire == false);

            // J'affiche les catégories qui ne sont pas complémentaires.
            lbxListeCategorie.ItemsSource = ViewModel.Categories.Where(x => x.EstComplementaire == false);

            ViewModel.LaCommande = Constante.commande;

            if( Constante.commande != null && 
                Constante.commande.ListeClients != null && 
                Constante.commande.ListeClients.Count > 0)
            {

                refreshListeItem();

               // On tag tous les items complémentaires
               Constante.commande.ListeClients.First().ListeFormatItemClientFacture.ToList().ForEach(x => x.ListFicf.ToList().ForEach( y => y.EstComplementaire = true));

               // On attibut 
               // TODO à enlever lbxItemsClient.ItemsSource = Constante.commande.ListeClients.First().ListeFormatItemClientFacture;
            }

            // Si on vient de créer la commande, je dois ajouter un client au départ vide.
            if (Constante.commande != null && (ViewModel.LaCommande.ListeClients == null || ViewModel.LaCommande.ListeClients.Count == 0))
            {
                if (ViewModel.LaCommande.ListeClients == null)
                    ViewModel.LaCommande.ListeClients = new List<Client>();

                ViewModel.LaCommande.ListeClients.Add(new Client());
                ViewModel.EnregistrerUnNouveauClient(ViewModel.LaCommande);
            }

             if (Constante.commande != null)
             {
                // On change le numéro de client sur l'écran
                lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;
             }

            // Update de la variable statique
             Constante.commande = ViewModel.LaCommande;
        }

        #region Fonctions pour la gestion des items complémentaires

        // Affichage des catégories complémentaires.
        private void lbxItemsClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatItemClientFacture tempFicf = new FormatItemClientFacture();
            tempFicf = (FormatItemClientFacture)lbxItemsClient.SelectedItem;

            // Si l'item sélectionné ne fait pas partie d'une catégorie complémentaire, j'affiche les catégories et leurs items complémentaires.
            if (tempFicf != null && tempFicf.FormatItemAssocie.ItemAssocie.Categories.EstComplementaire == false)
            {
                // Sélectionne les catégories complémentaires
                lbxListeCategorie.ItemsSource = ViewModel.Categories.Where(x => x.EstComplementaire);
                lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.EstComplementaire);
            }
        }

        #endregion

        #region Fonctions pour la liste d'items du client
        /// <summary>
        /// Rafraîchit la liste de ficf de la facture du client et la remet en ordre pour les compléments.
        /// </summary>
        public void refreshListeItemFacture()
        {
            List<FormatItemClientFacture> ficfTemp = new List<FormatItemClientFacture>();

            foreach (FormatItemClientFacture ficf in Constante.commande.ListeClients.ElementAt(NumeroClient).FactureClient.ListeFormatItemClientFacture)
            {
                if (!ficf.EstComplementaire)
                {

                    // On ajoute le ficf
                    ficfTemp.Add(ficf);
                }

                // On parcours la liste de ficf du ficf principal
                foreach (FormatItemClientFacture ficfChild in ficf.ListFicf)
                {
                    // On ajoute le ficfChild à la list en déterminant le style de l'élément
                    ficfTemp.Add(ficfChild);
                }
            }

            ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).FactureClient.ListeFormatItemClientFacture = ficfTemp;

        }
        /// <summary>
        /// Rafraîchit la liste de ficf du client et la remet en ordre pour les compléments.
        /// </summary>
        public void refreshListeItem()
        {
            List<FormatItemClientFacture> ficfTemp = new List<FormatItemClientFacture>();

            foreach (FormatItemClientFacture ficf in Constante.commande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture)
            {
                if (!ficf.EstComplementaire)
                {

                    // On ajoute le ficf
                    ficfTemp.Add(ficf);
                }

                // On parcours la liste de ficf du ficf principal
                foreach (FormatItemClientFacture ficfChild in ficf.ListFicf)
                {
                    // On ajoute le ficfChild à la list en déterminant le style de l'élément
                    ficfTemp.Add(ficfChild);
                }
            }

            ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture = ficfTemp;
            lbxItemsClient.ItemsSource = ficfTemp;
            lbxItemsClient.Items.Refresh();
        }



        /// <summary>
        /// Suppression de l'item sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerItem_Click(object sender, RoutedEventArgs e)
        {
            FormatItemClientFacture ficf = (FormatItemClientFacture)lbxItemsClient.SelectedItem;

            if (ficf != null && ficf.EstComplementaire == false)
            {
                // S'il possède une liste de ficf, il possède des complémentaire qui doivent aussi être suprimé, et avant la suppression de celui-ci.
                if (ficf.ListFicf.Count > 0)
                {
                    // Je dois mettre la liste en ordre avec cette fonction
                    refreshListeItem();
                    refreshListeItemFacture();

                    // Si le ficf est complémentaire
                    int ficfParentIndex = lbxItemsClient.SelectedIndex;
                    FormatItemClientFacture ficfTmp = new FormatItemClientFacture();

                    // Je dois remonter la liste pour trouver le ficf parent qui n'est pas complémentaire.
                    for (int i = ficfParentIndex+1; i < g_Client().ListeFormatItemClientFacture.Count; i++)
                    {
                        //lbxItemsClient.
                        if (ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture.ElementAt(i).EstComplementaire)
                        {
                            ficfTmp = g_Client().ListeFormatItemClientFacture.ElementAt(i);
                            g_Client().ListeFormatItemClientFacture.Remove(ficfTmp);
                            g_Client().FactureClient.ListeFormatItemClientFacture.Remove(ficfTmp);
                        }
                        else if (!g_Client().ListeFormatItemClientFacture.ElementAt(i).EstComplementaire)
                            break;// Si je rencontre un non complémentaire j'arrète la boucle car je vais supprimer tous les complémentaire sinon.

                    }
                }

                g_Client().ListeFormatItemClientFacture.Remove(ficf);
                g_Client().FactureClient.ListeFormatItemClientFacture.Remove(ficf);
                ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

            }
            else if (ficf != null && ficf.EstComplementaire)
            {
                // S'il est complémentaire je suprimme de la même façon.
                g_Client().ListeFormatItemClientFacture.Remove(ficf);
                g_Client().FactureClient.ListeFormatItemClientFacture.Remove(ficf);
                ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

            }
            refreshListeItem();
        }

        /// <summary>
        /// Lorsque l'utilisateur clique sur un item, je l'ajoute à sa liste d'items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)((sender as Button).CommandParameter);

            FormatsView view = new FormatsView(item.Formats);

            // S'il a seulement un formatItem je n'affiche pas la liste de format disponible.
            if (item.Formats.Count > 1)
            {
                // Je dois afficher les formats disponibles de l'item
                view.ShowDialog();
            }

            if (view.formatItemChoisi != null || item.Formats.Count == 1)
            {
                // On initialise le nouveau ficf
                FormatItemClientFacture ficf = new FormatItemClientFacture();

                if (item.Formats.Count == 1)
                {
                    // Copie de l'item associé.
                    ficf.FormatItemAssocie = item.Formats.First();
                    // Copie du client
                    ficf.client = g_Client();
                    // Copie de la facture.
                    ficf.facture = ficf.client.FactureClient;
                    // Enregistrement du prix.
                    ficf.Prix = ficf.FormatItemAssocie.Prix;
                }
                else
                {
                    // Copie de l'item associé.
                    ficf.FormatItemAssocie = view.formatItemChoisi;
                    // Copie du client
                    ficf.client = g_Client();
                    // Copie de la facture.
                    ficf.facture = ficf.client.FactureClient;
                    // Enregistrement du prix.
                    ficf.Prix = ficf.FormatItemAssocie.Prix;
                }

                // Si l'utilisateur a choisi un format ou il y avait seulement un format dans l'item.
                if (!item.Categories.EstComplementaire && (view.formatItemChoisi != null || item.Formats.Count == 1))
                {
                    g_Client().ListeFormatItemClientFacture.Add(ficf);

                    // Refresh de la liste d'items du client
                    refreshListeItem();

                    // Ajout du ficf à la facture du client
                    g_Client().FactureClient.ListeFormatItemClientFacture.Add(ficf);

                    // Ajout à la BD.
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

                    // Update de la variable statique
                    Constante.commande = ViewModel.LaCommande;

                }
                else if (item.Categories.EstComplementaire) // Si l'item est complémentaire.
                {
                    FormatItemClientFacture ficfTemp = new FormatItemClientFacture();
                    // Il faut que j'aille l'item sélectionné dans la liste d'item
                    ficfTemp = (FormatItemClientFacture)lbxItemsClient.SelectedItem;
                    ficf.EstComplementaire = true; // Ajouté par Simon 20/11/2014 pour savoir que c'est un complément


                    g_Client().ListeFormatItemClientFacture.Where(x => x.IdFormatItemClientFacture == ficfTemp.IdFormatItemClientFacture).First().ListFicf.Add(ficf);

                    // Refresh de la liste d'items du client
                    refreshListeItem();

                    // Ajout à la BD.
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);
                }
            }
            // Refresh de la liste d'items du client
            refreshListeItem();
        }

        /// <summary>
        /// Retourne le client dans la commande du viewmodel qui est actuellement visionné dans l'écran.
        /// </summary>
        private Client g_Client()
        {
            return ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient);
        }

        private void btnSuivantClient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Si cette condition est respectée je peux afficher le client suivant
            if (NumeroClient < ViewModel.LaCommande.ListeClients.Count - 1)
            {
                // Si on avance de client, on s'assure que le bouton précédent est Enabled.
                btnClientPrecedent.IsEnabled = true;

                NumeroClient += 1;

                // On change le numéro de client sur l'écran
                lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;

                refreshListeItem();
            }

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

                refreshListeItem();

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


            ViewModel.LaCommande.ListeClients.Add(new Client());

            ViewModel.EnregistrerUnNouveauClient(ViewModel.LaCommande);
            // Refresh des clients pour obtenir leurs ID
            ViewModel.TousLesClientsDeLaCommande();

            lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;


            // Update de la variable statique
            Constante.commande = ViewModel.LaCommande;

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

            MessageBoxResult result = MessageBoxResult.None;

            // Si la liste d'items du client est vide je n'affiche pas le message de confirmation.
            if (ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient).ListeFormatItemClientFacture.Count != 0)
            {
                result = MessageBox.Show("Voulez-vous vraiment supprimer le client #" + (NumeroClient + 1) + " et tous ses items ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            }

            // Si l'utilisateur a demandé de ne pas quitter, on renvoie faux
            if (result == MessageBoxResult.Yes || g_Client().ListeFormatItemClientFacture.Count == 0)
            {
                ViewModel.LaCommande.ListeClients.Remove(ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient));
                NumeroClient -= 1;

                // Enregistrement en BD
                ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

                // Refresh du label
                lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;

                // Refresh du client affiché
                refreshListeItem();


                // Update de la variable statique
                Constante.commande = ViewModel.LaCommande;
            }    
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
