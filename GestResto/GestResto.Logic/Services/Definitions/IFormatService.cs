﻿using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service Format
    /// </summary>
    public interface IFormatService
    {
        void Create(Format format);
        IList<Format> RetrieveAll();
        Format Retrieve(RetrieveFormatArgs args);
        void Update(Format format);
    }
}
