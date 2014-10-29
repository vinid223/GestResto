/*Suppression de la base de donn�es*/
/*DROP DATABASE IF EXISTS 5a5_a14_gestresto;*/

/*Cr�ation et utilisation de la base de donn�es*/
/*CREATE DATABASE IF NOT EXISTS 5a5_a14_gestresto;*/
USE 5a5_a14_gestresto;

/*Suppression de toutes les tables*/
DROP TABLE IF EXISTS TablesCommandes;
DROP TABLE IF EXISTS Complements;
DROP TABLE IF EXISTS FormatsItemsClientsFactures;
DROP TABLE IF EXISTS FormatsItems;
DROP TABLE IF EXISTS Formats;
DROP TABLE IF EXISTS Factures;
DROP TABLE IF EXISTS Clients;
DROP TABLE IF EXISTS Items;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Commandes;
DROP TABLE IF EXISTS Tables;
DROP TABLE IF EXISTS Employes;
DROP TABLE IF EXISTS TypesEmploye;
DROP TABLE IF EXISTS Restaurants;

/******************************************Cr�ation des tables et liens des foreign key******************************************/

/*Cr�ation de la table TypesEmploye*/
CREATE TABLE IF NOT EXISTS TypesEmploye
(
	idType INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(30),
	UNIQUE(nom)
);

/*Cr�ation de la table Employes*/
CREATE TABLE IF NOT EXISTS Employes
(
	idEmploye INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idType INT NOT NULL,
	idRestaurant INT NOT NULL,
	nom VARCHAR(30) NOT NULL,
	prenom VARCHAR(30) NOT NULL,
	noEmploye VARCHAT(255) NOT NULL,
	motPasse VARCHAR(255) NOT NULL, 
	adresse VARCHAR(50) NOT NULL,
	ville VARCHAR(30) NOT NULL,
	codePostal VARCHAR(6) NOT NULL,
	NAS VARCHAR(9) NOT NULL,
	tauxHoraire FLOAT NOT NULL,
	telephone VARCHAR(11) DEFAULT NULL,
	actif BOOL NOT NULL,
	UNIQUE(noEmploye),
	UNIQUE(NAS)
);

INSERT INTO `employes` (`idEmploye`, `idTypeEmploye`, `idRestaurant`, `nom`, `prenom`, `noEmploye`, `motPasse`, `adresse`, `ville`, `codePostal`, `NAS`, `tauxHoraire`, `telephone`, `actif`) VALUES
(1, 1, 1, 'Admin', 'Admin', '11', 'OMW4d6HGe0apyrsXmH7P9w==', 'Le mot de passe est 11', 'Administrateur', 'G1Q1Q9', '123456789', 0.95, '1231', 1);



/*Cr�ation du lien avec la foreign key de la table TypesEmploye vers la table Employes*/
ALTER TABLE Employes
ADD CONSTRAINT Types_Employes_FK
FOREIGN KEY (idTypeEmploye) REFERENCES TypesEmploye (idTypeEmploye);

/*Cr�ation de la table Restaurants*/
CREATE TABLE IF NOT EXISTS Restaurants
(
	idRestaurant INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(50) NOT NULL,
	adresse VARCHAR(30) NOT NULL,
	telephone VARCHAR(11) NOT NULL,
	fax VARCHAR(11) DEFAULT NULL ,
	ville VARCHAR(50) DEFAULT NULL,
	codePostal VARCHAR(10) DEFAULT NULL,
	dateCreation DATETIME NOT NULL
);


INSERT INTO `restaurants` (`idRestaurant`, `nom`, `adresse`, `telephone`, `fax`, `ville`, `codePostal`, `dateCreation`) VALUES
(1, 'Chez Alain', '99 rue de la perfection', '8193236308', '8193236307', 'Paradis', 'H0H0H0', '2014-10-16 17:10:19');


/*Cr�ation du lien avec la foreign key de la table Restaurants vers la table Employes*/
ALTER TABLE Employes
ADD CONSTRAINT Restaurants_Employes_FK
FOREIGN KEY (idRestaurant) REFERENCES Restaurants (idRestaurant);

/*Cr�ation de la table Commandes*/
CREATE TABLE IF NOT EXISTS Commandes
(
	idCommande INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idEmploye INT NOT NULL,
	status ENUM('Active','Pay�','En attente') DEFAULT NULL,
	arrivee DATETIME NOT NULL,
	depart DATETIME DEFAULT NULL
);

/*Cr�ation du lien avec la foreign key de la table Employes vers la table Commandes*/
ALTER TABLE Commandes
ADD CONSTRAINT Employes_Commandes_FK
FOREIGN KEY (idEmploye) REFERENCES Employes (idEmploye);

/*Cr�ation de la table Tables*/
CREATE TABLE IF NOT EXISTS Tables
(
	idTable INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	noTable INT NOT NULL,
	actif BOOL DEFAULT NULL,
	assigne BOOL DEFAULT NULL,
	UNIQUE(noTable)
);

/*Cr�ation de la table TablesCommandes*/
CREATE TABLE IF NOT EXISTS TablesCommandes
(
	idTableCommande INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idTable INT NOT NULL,
	idCommande INT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table Commandes vers la table TablesCommandes*/
ALTER TABLE TablesCommandes
ADD CONSTRAINT Commandes_TablesCommandes_FK
FOREIGN KEY (idCommande) REFERENCES Commandes (idCommande);

/*Cr�ation du lien avec la foreign key de la table Tables vers la table TablesCommandes*/
ALTER TABLE TablesCommandes
ADD CONSTRAINT Tables_TablesCommandes_FK
FOREIGN KEY (idTable) REFERENCES Tables (idTable);

/*Cr�ation de la table Categories*/
CREATE TABLE IF NOT EXISTS Categories
(
	idCategorie INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(50) NOT NULL,
	actif BIT(1) DEFAULT b'1', 
	complementaire BIT(1) DEFAULT b'0',
	UNIQUE(nom)
);

/*Cr�ation de la table Items*/
CREATE TABLE IF NOT EXISTS Items
(
	idItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idCategorie INT DEFAULT NULL,
	nom VARCHAR(30) NOT NULL,
	actif bit(1) NOT NULL DEFAULT b'1',
	UNIQUE(nom)
);

/*Cr�ation du lien avec la foreign key de la table Categories vers la table Items*/
ALTER TABLE Items
ADD CONSTRAINT Categories_Items_FK
FOREIGN KEY (idCategorie) REFERENCES Categories (idCategorie);


/*Cr�ation de la table Clients*/
CREATE TABLE IF NOT EXISTS Clients
(
	idClient INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idTable INT NOT NULL,
	idCommande INT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table Tables vers la table Clients*/
ALTER TABLE Clients
ADD CONSTRAINT Tables_Clients_FK
FOREIGN KEY (idTable) REFERENCES Tables (idTable);

/*Cr�ation du lien avec la foreign key de la table Commandes vers la table Clients*/
ALTER TABLE Clients
ADD CONSTRAINT Commandes_Clients_FK
FOREIGN KEY (idCommande) REFERENCES Commandes (idCommande);


/*Cr�ation de la table Factures*/
CREATE TABLE IF NOT EXISTS Factures
(
	idFacture INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idCommande INT NOT NULL,	
	dateDeCreation DATETIME NOT NULL,
	pourcentageTaxe FLOAT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table Commandes vers la table Factures*/
ALTER TABLE Factures
ADD CONSTRAINT Commandes_Factures_FK
FOREIGN KEY (idCommande) REFERENCES Commandes (idCommande);

/*Cr�ation de la table Formats*/
CREATE TABLE IF NOT EXISTS Formats
(
	idFormat INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(20) NOT NULL,
	libelle VARCHAR(3) NOT NULL,
	actif tinyint(1) NOT NULL DEFAULT TRUE,
	UNIQUE(nom),
	UNIQUE(libelle)
);

/*Cr�ation de la table FormatsItems*/
CREATE TABLE IF NOT EXISTS FormatsItems
(
	idFormatItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFormat INT DEFAULT NULL,
	idItem INT DEFAULT NULL,
	prix FLOAT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table Items vers la table FormatsItems*/
ALTER TABLE FormatsItems
ADD CONSTRAINT Items_FormatsItems_FK
FOREIGN KEY (idItem) REFERENCES Items (idItem);

/*Cr�ation du lien avec la foreign key de la table Formats vers la table FormatsItems*/
ALTER TABLE FormatsItems
ADD CONSTRAINT Formats_FormatsItems_FK
FOREIGN KEY (idFormat) REFERENCES Formats (idFormat);

/* Cr�ation de la table Complements */
CREATE TABLE IF NOT EXISTS Complements
(
	idComplement INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFormatItem INT NOT NULL,
	idFormatItemClientFacture INT NOT NULL,
	prix FLOAT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table FormatsItems vers la table Complements*/
ALTER TABLE Complements
ADD CONSTRAINT FormatsItems_Complements_FK
FOREIGN KEY (idFormatItem) REFERENCES FormatsItems (idFormatItem);

/*Cr�ation de la table FormatsItemsClientsFactures*/
CREATE TABLE IF NOT EXISTS FormatsItemsClientsFactures
(
	idFormatItemClientFacture INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFormatItem INT NOT NULL,
	idClient INT NOT NULL,
	idFacture INT NOT NULL,
	prix FLOAT NOT NULL
);

/*Cr�ation du lien avec la foreign key de la table FormatsItems vers la table FormatsItemsClientsFactures */
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT FormatsItems_FormatsItemsClientsFactures_FK
FOREIGN KEY (idFormatItem) REFERENCES FormatsItems (idFormatItem);

/*Cr�ation du lien avec la foreign key de la table Client vers la table FormatsItemsClientsFactures */
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT Clients_FormatsItemsClientsFactures_FK
FOREIGN KEY (idClient) REFERENCES Clients(idClient);

/*Cr�ation du lien avec la foreign key de la table FormatsItemsClientsFactures vers la table Factures*/
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT Factures_FormatsItemsClientsFactures_FK
FOREIGN KEY (idFacture) REFERENCES Factures (idFacture);

/*Cr�ation du lien avec la foreign key de la table FormatsItemsClientsFactures vers la table Complements*/
ALTER TABLE Complements
ADD CONSTRAINT FormatsItemsClientsFactures_Complements_FK
FOREIGN KEY (idFormatItemClientFacture) REFERENCES FormatsItemsClientsFactures (idFormatItemClientFacture);



