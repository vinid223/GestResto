//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Facture.cs
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
    public class Facture {
	    private uint idFacture ;
	    private DATETIME DateCreation ;
	    private float pourcentageTaxe ;
	    private List<Item> ListeItems ;
    }
}