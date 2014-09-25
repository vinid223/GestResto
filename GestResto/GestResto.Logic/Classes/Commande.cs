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


public class Commande {
	private uint idCommande ;
	private string Statut ;
	private DATETIME Debut ;
	private DATETIME Fin ;
	private List<Table> ListeTables ;
	private List<Facture> ListeFactures ;
}
