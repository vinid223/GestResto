using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service Facture
    /// </summary>
    public interface IFactureService
    {
        void Create(Facture facture);
        IList<Facture> RetriveAll();
        Facture Retrive(RetrieveFactureArgs args);
        void Update(Facture facture);
    }
}
