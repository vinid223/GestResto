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
    public class CategorieViewModel : BaseViewModel
    {
        private ICategorieService _categService;
        private ObservableCollection<Categorie> _categories = new ObservableCollection<Categorie>();

        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }

        private Categorie _categorie;

        public Categorie Categorie
        { 
            get 
            { 
                return _categorie; 
            } 
            
            set
            {
                _categorie = value; 
                RaisePropertyChanged(); 
            } 
        }

        public CategorieViewModel()
        {
            Categories = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());
            _categService = ServiceFactory.Instance.GetService<ICategorieService>();
        }

        public IList<Categorie> ObtenirToutesLesCategories()
        {
            IList<Categorie> listeCateg = null;
            listeCateg = _categService.RetrieveAll();
            return listeCateg;
        }

        public void EnregistrerUneCategorie(Categorie categorie)
        {
            try
            {
                _categService.Update(categorie);
            }
            catch (NHibernate.Exceptions.GenericADOException adoE)
            {
               MySql.Data.MySqlClient.MySqlException mysqlExeception = (adoE.InnerException as MySql.Data.MySqlClient.MySqlException);
               throw mysqlExeception;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int AjouterUneCategorie(Categorie categorie)
        {

            try
            {
                // On insert l'enregistrement dans la base de donnée
                _categService.Create(categorie);
            }
            catch (NHibernate.Exceptions.GenericADOException adoE)
            {
                MySql.Data.MySqlClient.MySqlException mysqlExeception = (adoE.InnerException as MySql.Data.MySqlClient.MySqlException);
                throw mysqlExeception;
            }
            catch (Exception e)
            {
                throw e;
            }

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = categorie.IdCategorie ?? default(int);

            return i;
        }
    }
}
