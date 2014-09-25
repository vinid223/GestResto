//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : format.cs
//  Date : 2014-09-25
//  Auteurs : Tommy Demers, Vincent Desrosiers et Simon Turcotte
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Classes
{
    public class Format 
    {
	    private int idFormat ;
	    private string sNom ;
	    private string sLibele ;
	    private float fPrix ;

        public Format()
        {

        }

        public Format(int pidFormat, string pNom, string pLibele, float pPrix)
        {
            idFormat = pidFormat;
            sNom = pNom;
            sLibele = pLibele;
            fPrix = pPrix;
        }
    }
}