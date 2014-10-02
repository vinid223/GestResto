﻿using GestResto.Logic.Model.Args;
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
        Employe Retrive(RetrieveEmployeArgs args);
        void Update(Employe employe);
    }
}
