using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;

namespace GestResto.Logic.Services.Definitions
{
    public interface IFormatItemClientFactureService
    {
        void Create(FormatItemClientFacture formatItemClientFacture);
        IList<FormatItemClientFacture> RetrieveAll();
        FormatItemClientFacture Retrieve(RetrieveFormatItemClientFactureArgs args);
        void Update(FormatItemClientFacture formatItemClientFacture);
        void Delete(FormatItemClientFacture formatItemClientFacture);
    }
}
