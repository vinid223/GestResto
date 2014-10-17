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
    /// Interface permettant de définir les fonctions du service FormatItem
    /// </summary>
    public interface IFormatItemService
    {
        void Create(FormatItem formatItem);
        IList<FormatItem> RetrieveAll();
        FormatItem Retrieve(RetrieveFormatItemArgs args);
        void Update(FormatItem formatItem);
    }
}
