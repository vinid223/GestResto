using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    public interface ICommandeService
    {
        void Create(Commande categorie);
        IList<Commande> RetrieveAll();
        Commande Retrieve(int pIdCategorie);
        void Update(Commande categorie);
        void Delete(Commande categorie);
    }
}
