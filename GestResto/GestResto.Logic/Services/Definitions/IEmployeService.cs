using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    public interface IEmployeService
    {
        void Create(Employe employe);
        IList<Employe> RetriveAll();
        Employe Retrive(int pIdEmploye);
        void Update(Employe employe);
        void Delete(Employe employe);
    }
}
