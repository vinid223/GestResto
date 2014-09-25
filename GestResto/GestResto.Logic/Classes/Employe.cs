//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Employe.cs
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
    public class Employe {
	    private int idEmploye ;
	    private string sTypeEmploye ;
	    private string sNom ;
	    private string sPrenom ;
	    private int noEmploye ;
	    private string sMotDePasse ;
	    private string sAdresse ;
	    private string sVille ;
	    private string sCodePostal ;
	    private string sNAS ;
	    private float sSalaire ;
	    private string sTelephone ;
	    private bool bActif ;
	    private List<Commande> ListeCommandes ;
    }
}