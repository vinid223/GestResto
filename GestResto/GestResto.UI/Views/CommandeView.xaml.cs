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

        public CommandeView(int numeroClientEnCours = 0)
        {
            NumeroClient = numeroClientEnCours;
            
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
                Constante.commande.ListeClients.ElementAt(numeroClientEnCours).ListeFormatItemClientFacture.ToList().ForEach(x => x.ListFicf.ToList().ForEach(y => y.EstComplementaire = true));

            }

            // Si on vient de créer la commande, je dois ajouter un client au départ vide.
            if (Constante.commande != null && (ViewModel.LaCommande.ListeClients == null || ViewModel.LaCommande.ListeClients.Count == 0))
            {
                if (ViewModel.LaCommande.ListeClients == null)
                    ViewModel.LaCommande.ListeClients = new List<Client>();

                ViewModel.LaCommande.ListeClients.Add(new Client());
                ViewModel.EnregistrerUnNouveauClient(ViewModel.LaCommande);

                // Règle un bug, ces lignes devraient etre supprimées
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CommandeView>(new CommandeView(NumeroClient));
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

        #region Refresh de la liste d'item
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
        #endregion

        #region Fonctions pour la liste d'items du client




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
                            // Je dois aussi l'enlevé de la liste du parent
                            g_Client().ListeFormatItemClientFacture.ElementAt(ficfParentIndex).ListFicf.Remove(ficfTmp);
                            g_Client().FactureClient.ListeFormatItemClientFacture.ElementAt(ficfParentIndex).ListFicf.Remove(ficfTmp);
                            i -= 1;
                            ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);
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
                int ficfParentIndex = lbxItemsClient.SelectedIndex;

                List<FormatItemClientFacture> test = new List<FormatItemClientFacture>();

                // Pour trouver son parent, pour ensuite le supprimer dans sa liste
                for (int i = ficfParentIndex; i >= 0; i--)
                {
                    // S'il n'est pas complémentaire.
                    if(!g_Client().ListeFormatItemClientFacture.ElementAt(i).EstComplementaire)
                    {
                        test = g_Client().ListeFormatItemClientFacture.ElementAt(i).ListFicf.ToList();
                        g_Client().ListeFormatItemClientFacture.ElementAt(i).ListFicf.Remove(ficf);
                        g_Client().FactureClient.ListeFormatItemClientFacture.ElementAt(i).ListFicf.Remove(ficf);
                        break;
                    }
                }


                // S'il est complémentaire je suprimme de la même façon.
                g_Client().ListeFormatItemClientFacture.Remove(ficf);
                g_Client().FactureClient.ListeFormatItemClientFacture.Remove(ficf);

                ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

            }


            // Update de la variable statique
            Constante.commande = ViewModel.LaCommande;

            // Règle un bug, ces lignes devraient etre supprimées
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandeView>(new CommandeView(NumeroClient));

            refreshListeItem();
            refreshListeItemFacture();
        }


        private void btnItemsPrincipaux_Click(object sender, RoutedEventArgs e)
        {
            // J'affiche les items qui ne sont pas complémentaires.
            lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.EstComplementaire == false);

            // J'affiche les catégories qui ne sont pas complémentaires.
            lbxListeCategorie.ItemsSource = ViewModel.Categories.Where(x => x.EstComplementaire == false);
        }
        
        /// <summary>
        /// Lorsque l'utilisateur clique sur un item, je l'ajoute à sa liste d'items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)((sender as Button).CommandParameter);


            if (lbxItemsClient.SelectedItem != null && ((FormatItemClientFacture)lbxItemsClient.SelectedItem).EstComplementaire)
            {    
                return;
            }

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

                    // Ajout du ficf à la facture du client
                    g_Client().FactureClient.ListeFormatItemClientFacture.Add(ficf);

                    // Refresh de la liste d'items du client
                    refreshListeItem();
                    refreshListeItemFacture();

                    // Ajout à la BD.
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);



                }
                else if (item.Categories.EstComplementaire) // Si l'item est complémentaire.
                {
                    FormatItemClientFacture ficfTemp = new FormatItemClientFacture();
                    // Il faut que j'aille l'item sélectionné dans la liste d'item
                    ficfTemp = (FormatItemClientFacture)lbxItemsClient.SelectedItem;
                    ficf.EstComplementaire = true; // Ajouté par Simon 20/11/2014 pour savoir que c'est un complément


                    g_Client().ListeFormatItemClientFacture.Where(x => x.IdFormatItemClientFacture == ficfTemp.IdFormatItemClientFacture).First().ListFicf.Add(ficf);
                    g_Client().ListeFormatItemClientFacture.Add(ficf);

                    // Ajout à la BD.
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);
                }
            }

            // Update de la variable statique
            Constante.commande = ViewModel.LaCommande;

            // Refresh de la liste d'items du client
            refreshListeItem();
            refreshListeItemFacture();

            // Règle un bug, ces lignes devraient etre supprimées
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandeView>(new CommandeView(NumeroClient));
            
        }

        /// <summary>
        /// Retourne le client dans la commande du viewmodel qui est actuellement visionné dans l'écran.
        /// </summary>
        private Client g_Client()
        {
            return ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient);
        }


        #endregion

        #region Navigation entre clients
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

            // Si l'utilisateur veut afficher toutes les items
            if (categorie.Nom == "Tous les items")
            {
                // J'affiche les items qui ne sont pas complémentaires.
                lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.EstComplementaire == false);
            }
            else 
            {
                lbxListeItems.ItemsSource = ViewModel.Items.Where(x => x.Categories.IdCategorie == categorie.IdCategorie).ToList();
            }
            

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

            // Déplacement au dernier client
            NumeroClient = ViewModel.LaCommande.ListeClients.Count-1;
            refreshListeItem();

            // Update de la variable statique
            Constante.commande = ViewModel.LaCommande;

            // Règle un bug, ces lignes devraient etre supprimées
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandeView>(new CommandeView(NumeroClient));

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
                // S'il a seulement un client, je fais seulement le vider.
                if (ViewModel.LaCommande.ListeClients.Count == 1)
                {
                    ViewModel.LaCommande.ListeClients.First().ListeFormatItemClientFacture.Clear();
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);
                }
                else
                {
                    ViewModel.LaCommande.ListeClients.Remove(ViewModel.LaCommande.ListeClients.ElementAt(NumeroClient));
                    NumeroClient -= 1;

                    if (NumeroClient == -1)
                        NumeroClient = 0;

                    // Enregistrement en BD
                    ViewModel.EnregistrerUneCommande(ViewModel.LaCommande);

                    // Refresh du label
                    lblNumeroClient.Content = "Client #" + (NumeroClient + 1) + "/" + ViewModel.LaCommande.ListeClients.Count;

                    // Refresh du client affiché
                    refreshListeItem();

                }
                // Update de la variable statique
                Constante.commande = ViewModel.LaCommande;
            }

            // Règle un bug, ces lignes devraient etre supprimées
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<CommandeView>(new CommandeView(NumeroClient));
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

        /// <summary>
        /// Fonction permettant de fermer la commande
        /// </summary>
        private void btnFermer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e);

            // On demande une confirmations à l'utilisateur s'il veut fermer la commande 
            if (MessageBoxResult.Yes == MessageBox.Show("Êtes-vous sure de vouloir fermer la commande en cours?","Confirmation de fermeture de commande",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No))
            {
                // On indique la date de fermeture de la commande
                Constante.commande.Fin = DateTime.Now;

                // On indique le statut de payé de la commande
                Constante.commande.Statut = "Payé";

                // On change le statut d'assignation de chacune des tables de la commande pour 
                // pouvoir réassigner les tables à une autre commande
                foreach (var table in Constante.commande.ListeTables)
                {
                    table.EstAssigne = false;
                }

                // On appel le viewmodel pour enregistrer les modifications
                ViewModel.EnregistrerUneCommande(Constante.commande);

                // On retourne à la vue précédente pour afficher les commandes en cours ou permettre au serveur
                // de créer une nouvelle commande
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CommandesView>(new CommandesView());
            }
        }
    }
}
