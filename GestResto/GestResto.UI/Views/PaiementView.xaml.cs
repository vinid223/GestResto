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
        private double montantRestant;  // Le montant qu'il reste à payer au client
        private double sousTotal;
        private double taxes;
        private double total;
        private string modePaiement;    // Le mode de paiement du client
        
        public PaiementView(Client client)
        {
            InitializeComponent();
            lblNom.Content = Constante.employe.ToString();

            sousTotal = 0; // On initialise le montant restant à 0$

            // On va chercher tous les prix
            foreach (FormatItemClientFacture ficf in client.FactureClient.ListeFormatItemClientFacture)
            {
                sousTotal += ficf.Prix;

                foreach(FormatItemClientFacture ficfChild in ficf.ListFicf)
                {
                    sousTotal += ficf.Prix;
                }
            }

            taxes = sousTotal * client.FactureClient.PourcentageTaxe;
            total = sousTotal+taxes;
            montantRestant = total;
            montantPaye = 0;

            modePaiement = null;

            
            txtSousTotal.Text = sousTotal.ToString("C2");
            txtTaxes.Text = taxes.ToString("C2");
            txtTotal.Text = total.ToString("C2");
            txtMontantRestant.Text = montantRestant.ToString("C2");

            // On affiche le montant C indique qu'on veut signe de $ et le 2 indique le nombre de décimales
            txtPrix.Text = montantPaye.ToString("C2");

            lbxItemsFacture.ItemsSource = client.FactureClient.ListeFormatItemClientFacture;  
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

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if(modePaiement==null)
            {
                MessageBox.Show("Veuillez sélectionner un mode de paiement avant de payer", "Aucun mode de paiement sélectionné", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               montantRestant-= montantPaye;
               txtMontantRestant.Text = montantRestant.ToString("C2");
            }
        }

        // Ajoute un chiffre dans le textbox du montant
        private void AjouterChiffre(int chiffre, int multiplicateur)
        {
            double decimale = chiffre/100.00;  // On divise le chiffre par 100 pour l'envoyer dans les décimales
            montantPaye*= multiplicateur;      // On multiplie le montant par un multiplicateur. Par exemple 10, pour faire passer le montant de 00.10 à 01.00
            montantPaye += decimale;           // On ajoute la décimale au montant. Par exemple, de 01.00 à 01.05
        }

        // Enlève un chiffre dans le textbox du montant // TODO 
        private void EnleverChiffre()
        {
            // On va chercher les décimales
            int result1ereDecimale = getDecimal(montantPaye, 1, false);
            int result2eDecimale = getDecimal(montantPaye, 2, true);
            
            
            // On récrit le montant total. Ceci évite de garder des décimales aberrantes
            montantPaye = (int)(montantPaye); // On garde juste les unités, sans les décimales
            montantPaye += (Convert.ToDouble(result1ereDecimale)/10);

            montantPaye/=10;    // On divise par 10 afin de tasser les chiffres
        }

        private int getDecimal(double number, int position, bool convertAuto)
        {
            int result2Decimales;
            // Si on demande de convertir automatiquement
            if(convertAuto)
            {
                result2Decimales = Convert.ToInt32((number - (int)(number)) * Math.Pow(10, position));       // On prend toutes les décimales jusqu'à la position ex. 0.8999 -> 899.0
            }
            // Sinon, on converti manuellement
            else
            {
                result2Decimales = (int)((number - (int)(number)) * Math.Pow(10, position));       // On prend toutes les décimales jusqu'à la position ex. 0.8999 -> 899.0
            }
            
            double result2DecimalesDouble = Convert.ToDouble(result2Decimales) / 10;    // On tasse le chiffre pour avoir notre décimale ex. 899.0 -> 89.9

            int resultDerniereDecimale = Convert.ToInt32((result2DecimalesDouble - (int)(result2DecimalesDouble)) * 10); // On prend la dernière décimales ex. 89.9 -> 9
            
            return resultDerniereDecimale;
        }


        private void btnModePaiement_Click(object sender, MouseButtonEventArgs e)
        {
            // On change toutes les images
            SetPath(imgAmex,"selected.png",".png");
            SetPath(imgDebit, "selected.png", ".png");
            SetPath(imgMasterCard, "selected.png", ".png");
            SetPath(imgMoney, "selected.png", ".png");
            SetPath(imgVisa, "selected.png", ".png");

            // On change l'image sender
            Image image = (sender as Image);
            SetPath(image, ".png", "selected.png");

            // On change le nom du mode de paiement
            modePaiement = image.Name.Substring(2);
        }

        private void SetPath(Image image, string toReplace, string replaceBy)
        {
            // On change le path
            StringBuilder path = new StringBuilder();
            path.Append(image.Source.ToString());
            path.Replace(toReplace, replaceBy);

            // On vérifie si le path est différent du nouveau path avant de changer l'image
                if (path.ToString() != image.Source.ToString())
            {
                // On change l'image
                SetImage(image, path.ToString());
            }
        }

        private void SetImage(Image image,string path)
        {
            image.BeginInit();
            image.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            image.EndInit();
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
