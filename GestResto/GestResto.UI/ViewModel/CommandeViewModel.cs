﻿using GestResto.Logic.Model.Args;
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
            // Possède toutes les catégories et la catégorie Tous les items. On fait ca car on ne veut pas l'ajouter à la BD.
            Categories2 = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());

            // Insère une catégorie seulement dans la liste en mémoire.
            categorieTousItems = new Categorie(-1, "Tous les items", true, false);

            Categories.Insert(0, categorieTousItems);

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
            try
            {
                LaCommande = _commandeService.Retrieve((int)LaCommande.IdCommande);
            }
            catch (Exception e)
            {
                string exceptionMessage = e.Message;
                StringBuilder messageErreur = new StringBuilder();

                messageErreur.Append("Erreur inconnue déconnectez et reconnectez vous, si le problème persiste contactez le soutient technique en notant cette erreur : ");
                messageErreur.Append(exceptionMessage);
                Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un F.I.C.F.");
            }
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
            try
            {
                _commandeService.Update(commande);
            }
            catch(Exception e)
            {
                string exceptionMessage = e.Message;
                StringBuilder messageErreur = new StringBuilder();

                messageErreur.Append("Erreur inconnue déconnectez et reconnectez vous, si le problème persiste contactez le soutient technique en notant cette erreur : ");
                messageErreur.Append(exceptionMessage);
                Constante.LogErreur("Erreur inconnue : " + exceptionMessage + " lors de l'enregistrement d'un F.I.C.F.");
            }
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
        private ObservableCollection<Categorie> _categories2 = new ObservableCollection<Categorie>();

        public Commande LaCommande;
        public Categorie _categorie;
        public Categorie categorieTousItems;
           
        public ObservableCollection<Categorie> Categories2
        {
            get { return _categories2; }
            set
            {
                _categories2 = value;
                RaisePropertyChanged();
            }
        }

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
