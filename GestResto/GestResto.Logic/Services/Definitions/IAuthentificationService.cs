using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;

namespace GestResto.Logic.Services.Definitions
{
    public interface IAuthentificationService
    {
        Employe Retrieve(int pNumEmploye, string pMDP);
    }
}
