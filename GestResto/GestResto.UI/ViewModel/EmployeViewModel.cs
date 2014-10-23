using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GestResto.UI.ViewModel
{
    public class EmployeViewModel : BaseViewModel
    {
        // On instensie l'interface d'employé
        private IEmployeService _employeService;

        // On a nos liste de la view
        private ObservableCollection<Employe> _employes = new ObservableCollection<Employe>();
        private ObservableCollection<TypeEmploye> _typesEmployes = new ObservableCollection<TypeEmploye>();

        // Définion de la liste d'employé
        public ObservableCollection<Employe> Employes
        {
            get { return _employes; }
            set
            {
                _employes = value;
                RaisePropertyChanged();
            }
        }

        // Définition de la liste de typeEmployé
        public ObservableCollection<TypeEmploye> TypesEmployes
        {
            get { return _typesEmployes; }
            set
            {
                _typesEmployes = value;
                RaisePropertyChanged();
            }
        }

        // Définition des propriétés avec leur définitions
        private Employe _employe;

        public Employe employe
        {
            get
            {
                return _employe;
            }

            set
            {
                _employe = value;
                RaisePropertyChanged();
            }
        }


        // Constructeur de la classe
        public EmployeViewModel()
        {
            // On va charger nos listes à partir de la base de donnée
            Employes = new ObservableCollection<Employe>(ServiceFactory.Instance.GetService<IEmployeService>().RetriveAll());
            TypesEmployes = new ObservableCollection<TypeEmploye>(ServiceFactory.Instance.GetService<ITypeEmployeService>().RetriveAll());

            // Initialisation de l'instance d'employé
            _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
        }

        /// <summary>
        /// Fonction permettant d'obtenir un employé pour une authentification
        /// </summary>
        /// <param name="PNoEmploye">Numéro de l'employé</param>
        /// <param name="PMDPEmploye">Mot de passe de l'employé</param>
        /// <returns></returns>
        public Employe ObtenirEmployeAuthentification(string PNoEmploye, string PMDPEmploye)
        {
            // On définie les variables d'arguments
            RetrieveEmployeArgs args = new RetrieveEmployeArgs();
            args.NoEmploye = PNoEmploye;
            args.MDP = Employe.Encrypt(PMDPEmploye);

            // On éfectue la requête et on retourne le résultat
            return _employeService.Retrive(args);
        }


        /// <summary>
        /// Fonction permettant de modifier un employé dans la base de données
        /// </summary>
        /// <param name="employe">Employé à modifier</param>
        public void EnregistrerUnEmployer(Employe employe)
        {
            // On effectue l'update dans la base de données
            _employeService.Update(employe);
        }

        /// <summary>
        /// Fonction permettant d'ajouter un employé dans la base de données
        /// </summary>
        /// <param name="employe">Employé à ajouter</param>
        /// <returns>Retourne le ID de l'employé inséré</returns>
        public int AjouterUnEmployer(Employe employe)
        {
            // On insert l'enregistrement dans la base de donnée
            _employeService.Create(employe);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = employe.IdEmploye ?? default(int);

            return i;
        }
    }
}
