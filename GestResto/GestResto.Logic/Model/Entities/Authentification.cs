using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    public class Authentification
    {
        Employe employe;

        public Authentification()
        {
            employe = new Employe();
        }

        public Authentification(Employe pE)
        {
            employe = pE;
        }
    }
}
