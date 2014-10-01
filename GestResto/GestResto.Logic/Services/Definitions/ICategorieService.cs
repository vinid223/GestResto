using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;

namespace GestResto.Logic.Services.Defenitions
{
    public interface ICategorieService
    {
        void Create(Categorie categorie);
        IList<Categorie> RetrieveAll();
        Categorie Retrieve(RetrieveCategorieArgs args);
        void Update(Categorie categorie);
        void Delete(Categorie categorie);
    }
}
