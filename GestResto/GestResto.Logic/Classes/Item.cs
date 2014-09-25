//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Item.cs
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
    public class Item {
	    private int idItem ;
	    private string sNom ;
	    private Format FormatItem ;
	    private categorie Categories ;
	    private List<Item> ListeComplements ;
    }
}