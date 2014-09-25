//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Restaurant.cs
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
    public class Restaurant {
	    private int idRestaurant ;
	    private string sNom ;
	    private string sAdresse ;
	    private string sTelephone ;
	    private string sFaxe ;
	    private string sVille ;
	    private string sCodePostal ;
	    private DATETIME DateCreation ;
	    private List<Employe> ListeEmployes ;
	    private List<Item> ListeItem ;
    }
}