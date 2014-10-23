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
        private IEmployeService _employeService;
        private ObservableCollection<Employe> _employes = new ObservableCollection<Employe>();
        private ObservableCollection<TypeEmploye> _typesEmployes = new ObservableCollection<TypeEmploye>();

        public ObservableCollection<Employe> Employes
        {
            get { return _employes; }
            set
            {
                _employes = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TypeEmploye> TypesEmployes
        {
            get { return _typesEmployes; }
            set
            {
                _typesEmployes = value;
                RaisePropertyChanged();
            }
        }

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

        public EmployeViewModel()
        {
            Employes = new ObservableCollection<Employe>(ServiceFactory.Instance.GetService<IEmployeService>().RetriveAll());
            TypesEmployes = new ObservableCollection<TypeEmploye>(ServiceFactory.Instance.GetService<ITypeEmployeService>().RetriveAll());
            _employeService = ServiceFactory.Instance.GetService<IEmployeService>();
        }

        public Employe ObtenirEmployeAuthentification(string PNoEmploye, string PMDPEmploye)
        {
            RetrieveEmployeArgs args = new RetrieveEmployeArgs();
            args.NoEmploye = PNoEmploye;
            args.MDP = Employe.Encrypt(PMDPEmploye);
            return _employeService.Retrive(args);
        }

        public void EnregistrerUnEmployer(Employe employe)
        {
            _employeService.Update(employe);
        }
    }
}
