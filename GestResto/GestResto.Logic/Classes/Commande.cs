//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Commande.cs
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
    public class Commande {
	    private int idCommande ;
	    private string Statut ;
	    private DATETIME Debut ;
	    private DATETIME Fin ;
	    private List<Table> ListeTables ;
	    private List<Facture> ListeFactures ;
    }
}