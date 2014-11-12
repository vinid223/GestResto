using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service Commande
    /// </summary>
    public interface ICommandeService
    {
        void Create(Commande commande);
        void Create(FormatItemClientFacture commande);
        IList<Commande> RetrieveAll(int idEmploye);
        Commande Retrieve(RetrieveCommandeArgs args);
        void Update(Commande commande);
    }
}
