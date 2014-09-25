using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    interface IFactureService
    {
        void Create(Facture facture);
        IList<Facture> RetriveAll();
        Facture Retrive(int pIdFacture);
        void Update(Facture facture);
        void Delete(Facture facture);
    }
}
