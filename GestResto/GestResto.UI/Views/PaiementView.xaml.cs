using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
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
using GestResto.Logic.Model.Entities;

namespace GestResto.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PaiementView.xaml
    /// </summary>
    public partial class PaiementView : UserControl
    {
        public double montantPaye;      // Le montant que le client paye
        private double montantRestant;   // Le montant qu'il reste à payer au client
        
        public PaiementView(Client client)
        {
            InitializeComponent();


            montantPaye = 0;
            montantRestant=10;

            // On affiche le montant C indique qu'on veut signe de $ et le 2 indique le nombre de décimales
            txtPrix.Text = montantPaye.ToString("C2");  
        }

        /// <summary>
        /// Cette fonction permet au bouton sélectionner d'ajouter le chiffre dans la boite de texte
        /// </summary>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string chiffreString = (sender as Button).Content.ToString();
            int chiffre = Int32.Parse(chiffreString);
            AjouterChiffre(chiffre, 10);

            // On affiche le montant C indique qu'on veut signe de $ et le 2 indique le nombre de décimales
            txtPrix.Text = montantPaye.ToString("C2");  
        }

        // Lorsque l'utilisateur clique sur le double 0
        private void btnZeroZero_Click(object sender, RoutedEventArgs e)
        {
            AjouterChiffre(0, 100);
            // On affiche le montant C indique qu'on veut signe de $ et le 2 indique le nombre de décimales
            txtPrix.Text = montantPaye.ToString("C2");  
        }

        // Lorsque l'utilisateur clique sur le bouton retour
        private void btnRetourClavier_Click(object sender, RoutedEventArgs e)
        {
            EnleverChiffre();
            // On affiche le montant C indique qu'on veut signe de $ et le 2 indique le nombre de décimales
            txtPrix.Text = montantPaye.ToString("C2");  
        }

        // Ajoute un chiffre dans le textbox du montant
        private void AjouterChiffre(int chiffre, int multiplicateur)
        {
            double decimale = chiffre/100.00;  // On divise le chiffre par 100 pour l'envoyer dans les décimales
            montantPaye*= multiplicateur;      // On multiplie le montant par un multiplicateur. Par exemple 10, pour faire passer le montant de 00.10 à 01.00
            montantPaye += decimale;           // On ajoute la décimale au montant. Par exemple, de 01.00 à 01.05
        }

        // Enlève un chiffre dans le textbox du montant
        private void EnleverChiffre()
        {
            montantPaye-=(montantPaye % 0.1);    // On enlève la dernier chiffre
            montantPaye/=10;                    // On divise par 10 afin
        }


        private void btnModePaiement_Click()
        {

        }

        // Fonction qui sert à nous déconnecter
        private void btnDeconnexion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // TODO Afficher un message afin d'aviser si la transaction n'est pas complète
            // Faire une fonction


            // On appel la fonction de la classe constante qui permet de déconnecter l'utilisateur en cour
            Constante.Deconnexion();
        }

        // Fonction qui sert à revenir à la view précédente
        private void btnRetour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Constante.onReleaseButton(sender, e); // On enlève l'effet du bouton pressé

            // Si le client a payé ou que l'utilisateur confirme le changement de fenêtre, on change la fenêtre
            if (VerifierPaiement())
            {
                // TODO : Nous devons revenir à la même commande. Il faudrait peut-être faire une variable globale ? --> Tommy propose d'envoyer le CommandeView en paramètres
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                mainVM.ChangeView<CommandeView>(new CommandeView());
            }
        }

        /// <summary>
        /// On vérifie si la transaction a été payé au complet. Si elle ne l'est pas, on demande à l'utilisateur s'il veut vraiment quitter
        /// </summary>
        /// <returns>Retourne vrai si le paiement est vérifier ou si l'utilisateur accepte le retour</returns>
        private bool VerifierPaiement()
        {

            // Si la transaction n'est pas complète, on affiche un message
            if (montantPaye < montantRestant)
            {
                StringBuilder messageErreur = new StringBuilder();
                messageErreur.Append("Il reste un total de ");
                messageErreur.Append(montantRestant.ToString("C2"));
                messageErreur.Append(" à payer sur la facture. Êtes-vous sûr de vouloir quitter ?");

                MessageBoxResult result = MessageBox.Show(messageErreur.ToString(), "Facture non-payée", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);

                // Si l'utilisateur a demandé de ne pas quitter, on renvoie faux
                if (result == MessageBoxResult.No)
                {
                    return false;
                }                          
            }
            return true;    // Dans tous les autres cas, on envoie vrai
        }

        private void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Constante.onPressButton(sender, e); // On ajoute l'effet du bouton pressé
        }

    }
}
