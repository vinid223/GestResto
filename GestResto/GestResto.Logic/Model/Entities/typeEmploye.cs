using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    class typeEmploye
    {

        #region Membres prives
        private virtual int? idTypeEmploye;
        private virtual string sNomType;
        #endregion


        public typeEmploye()
        {
            idTypeEmploye = null;
            sNomType = "";
        }

        public typeEmploye(int pIdEmploye, string pNomType)
        {
            idTypeEmploye = pIdEmploye;
            sNomType = pNomType;
        }
    }
}
