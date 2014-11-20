using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestResto.UI.ViewModel
{
    public class CommandeViewModel : BaseViewModel
    {
        public ICommandeService _commandeService;
        public IClientService _clientService;
        public IFactureService _factureService;

        public CommandeViewModel()
        {

            Categories = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());
            Items = new ObservableCollection<Item>(ServiceFactory.Instance.GetService<IItemService>().RetrieveAll(true));
            Commandes = new ObservableCollection<Commande>(ServiceFactory.Instance.GetService<ICommandeService>().RetrieveAll(1));

            LaCommande = new Commande();
            

            _factureService = ServiceFactory.Instance.GetService<IFactureService>();
            _clientService = ServiceFactory.Instance.GetService<IClientService>();
            _commandeService = ServiceFactory.Instance.GetService<ICommandeService>();
        }

        /// <summary>
        /// Update tous les clients de la commande.
        /// </summary>
        /// <param name="commande"></param>
        public void TousLesClientsDeLaCommande()
        {
            LaCommande = _commandeService.Retrieve((int)LaCommande.IdCommande);
        }

        public int AjouterUnFicf(FormatItemClientFacture ficf)
        {
            // On insert l'enregistrement dans la base de donnée
            _commandeService.Create(ficf);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = ficf.IdFormatItemClientFacture ?? default(int);

            return i;
        }

        public void EnregistrerUneCommande(Commande commande)
        {
            _commandeService.Update(commande);
            // Puisque je ne suis pas capable d'avoir le id du client, je reload toute la liste.
            TousLesClientsDeLaCommande();
        }

        public void EnregistrerUnNouveauClient(Commande commande)
        {
            _commandeService.Update(commande);
            // Puisque je ne suis pas capable d'avoir le id du client, je reload toute la liste.
            TousLesClientsDeLaCommande();
        }

        #region Bindables
        
        private ObservableCollection<Categorie> _categories = new ObservableCollection<Categorie>();
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        private ObservableCollection<Commande> _commandes = new ObservableCollection<Commande>();

        public Commande LaCommande;
        public Categorie _categorie;

        public ObservableCollection<Commande> Commandes
        {
            get { return _commandes; }
            set
            {
                _commandes = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }

        public Categorie Categorie
        {
            get { return _categorie; }
            set
            {
                _categorie = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        

    }
}
