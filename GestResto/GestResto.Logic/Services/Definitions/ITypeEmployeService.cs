using GestResto.Logic.Model.Entities;
using GestResto.Logic.Model.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service TypeEmploye
    /// </summary>
    public interface ITypeEmployeService
    {
        TypeEmploye Retrieve(RetrieveTypeEmployeArgs args);
        IList<TypeEmploye> RetriveAll();
    }
}
