/*Suppression de la base de données*/
DROP DATABASE IF EXISTS 5a5_a14_gestresto;

/*Création et utilisation de la base de données*/
CREATE DATABASE IF NOT EXISTS 5a5_a14_gestresto;
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

/******************************************Création des tables et liens des foreign key******************************************/

/*Création de la table TypesEmploye*/
CREATE TABLE IF NOT EXISTS TypesEmploye
(
	idTypeEmploye INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(30),
	UNIQUE(nom)
);

INSERT INTO `typesemploye` (`idTypeEmploye`, `nom`) VALUES
(1, 'Administrateur'),
(2, 'Serveur');

/*Création de la table Employes*/
CREATE TABLE IF NOT EXISTS Employes
(
	idEmploye INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idTypeEmploye INT NOT NULL,
	idRestaurant INT NOT NULL,
	nom VARCHAR(30) NOT NULL,
	prenom VARCHAR(30) NOT NULL,
	noEmploye VARCHAR(255) NOT NULL,
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

/*Création du lien avec la foreign key de la table TypesEmploye vers la table Employes*/
ALTER TABLE Employes
ADD CONSTRAINT Types_Employes_FK
FOREIGN KEY (idTypeEmploye) REFERENCES TypesEmploye (idTypeEmploye);

INSERT INTO `employes` (`idEmploye`, `idTypeEmploye`, `idRestaurant`, `nom`, `prenom`, `noEmploye`, `motPasse`, `adresse`, `ville`, `codePostal`, `NAS`, `tauxHoraire`, `telephone`, `actif`) VALUES
(1, 1, 1, 'Admin', 'Admin', '11', 'OMW4d6HGe0apyrsXmH7P9w==', 'Le mot de passe est 11', 'Administrateur', 'G1Q1Q9', '123456789', 0.95, '1231', 1);


/*Création de la table Restaurants*/
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


/*Création de la table Commandes*/
CREATE TABLE IF NOT EXISTS Commandes
(
	idCommande INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idEmploye INT NOT NULL,
	status ENUM('Active','Payé','En attente') DEFAULT NULL,
	arrivee DATETIME NOT NULL,
	depart DATETIME DEFAULT NULL
);

/*Création du lien avec la foreign key de la table Employes vers la table Commandes*/
ALTER TABLE Commandes
ADD CONSTRAINT Employes_Commandes_FK
FOREIGN KEY (idEmploye) REFERENCES Employes (idEmploye);

/*Création de la table Tables*/
CREATE TABLE IF NOT EXISTS Tables
(
	idTable INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	noTable INT NOT NULL,
	actif BOOL DEFAULT NULL,
	assigne BOOL DEFAULT NULL,
	UNIQUE(noTable)
);

/*Création de la table TablesCommandes*/
CREATE TABLE IF NOT EXISTS TablesCommandes
(
	idTableCommande INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idTable INT NOT NULL,
	idCommande INT NOT NULL
);

/*Création du lien avec la foreign key de la table Commandes vers la table TablesCommandes*/
ALTER TABLE TablesCommandes
ADD CONSTRAINT Commandes_TablesCommandes_FK
FOREIGN KEY (idCommande) REFERENCES Commandes (idCommande);

/*Création du lien avec la foreign key de la table Tables vers la table TablesCommandes*/
ALTER TABLE TablesCommandes
ADD CONSTRAINT Tables_TablesCommandes_FK
FOREIGN KEY (idTable) REFERENCES Tables (idTable);

/*Création de la table Categories*/
CREATE TABLE IF NOT EXISTS Categories
(
	idCategorie INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(50) NOT NULL,
	actif BIT(1) DEFAULT b'1', 
	complementaire BIT(1) DEFAULT b'0',
	UNIQUE(nom)
);

/*Création de la table Items*/
CREATE TABLE IF NOT EXISTS Items
(
	idItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idCategorie INT DEFAULT NULL,
	nom VARCHAR(30) NOT NULL,
	actif bit(1) NOT NULL DEFAULT b'1',
	UNIQUE(nom)
);

/*Création du lien avec la foreign key de la table Categories vers la table Items*/
ALTER TABLE Items
ADD CONSTRAINT Categories_Items_FK
FOREIGN KEY (idCategorie) REFERENCES Categories (idCategorie);


/*Création de la table Clients*/
CREATE TABLE IF NOT EXISTS Clients
(
	idClient INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idCommande INT DEFAULT NULL,
	idFacture INT DEFAULT NULL
);


/*Création du lien avec la foreign key de la table Commandes vers la table Clients*/
ALTER TABLE Clients
ADD CONSTRAINT Commandes_Clients_FK
FOREIGN KEY (idCommande) REFERENCES Commandes (idCommande);


/*Création de la table Factures*/
CREATE TABLE IF NOT EXISTS Factures
(
	idFacture INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	dateDeCreation DATETIME NOT NULL,
	pourcentageTaxe FLOAT NOT NULL
);

/*Création du lien avec la foreign key de la table Tables vers la table Clients*/
ALTER TABLE Clients
ADD CONSTRAINT Factures_Clients_FK
FOREIGN KEY (idFacture) REFERENCES Factures (idFacture);

/*Création de la table Formats*/
CREATE TABLE IF NOT EXISTS Formats
(
	idFormat INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	nom VARCHAR(20) NOT NULL,
	libelle VARCHAR(3) NOT NULL,
	actif tinyint(1) NOT NULL DEFAULT TRUE,
	UNIQUE(nom),
	UNIQUE(libelle)
);

/*Création de la table FormatsItems*/
CREATE TABLE IF NOT EXISTS FormatsItems
(
	idFormatItem INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFormat INT DEFAULT NULL,
	idItem INT DEFAULT NULL,
	prix FLOAT NOT NULL
);

/*Création du lien avec la foreign key de la table Items vers la table FormatsItems*/
ALTER TABLE FormatsItems
ADD CONSTRAINT Items_FormatsItems_FK
FOREIGN KEY (idItem) REFERENCES Items (idItem);

/*Création du lien avec la foreign key de la table Formats vers la table FormatsItems*/
ALTER TABLE FormatsItems
ADD CONSTRAINT Formats_FormatsItems_FK
FOREIGN KEY (idFormat) REFERENCES Formats (idFormat);

/* Création de la table Complements */
CREATE TABLE IF NOT EXISTS Complements
(
	idComplement INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFormatItem INT NOT NULL,
	idFormatItemClientFacture INT NOT NULL,
	prix FLOAT NOT NULL
);

/*Création du lien avec la foreign key de la table FormatsItems vers la table Complements*/
ALTER TABLE Complements
ADD CONSTRAINT FormatsItems_Complements_FK
FOREIGN KEY (idFormatItem) REFERENCES FormatsItems (idFormatItem);

/*Création de la table FormatsItemsClientsFactures*/
CREATE TABLE IF NOT EXISTS FormatsItemsClientsFactures
(
	idFormatItemClientFacture INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	idFicfParent INT DEFAULT NULL,
	idFormatItem INT NOT NULL,
	idClient INT NOT NULL,
	idFacture DEFAULT NULL,
	prix FLOAT NOT NULL
);

/*Création du lien avec la foreign key de la table FormatsItemsClientsFactures vers la table FormatsItemsClientsFactures */
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT FormatsItemsClientsFactures_FormatsItemsClientsFactures_FK
FOREIGN KEY (idFicfParent) REFERENCES FormatsItemsClientsFactures (idFormatItemClientFacture);

/*Création du lien avec la foreign key de la table FormatsItems vers la table FormatsItemsClientsFactures */
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT FormatsItems_FormatsItemsClientsFactures_FK
FOREIGN KEY (idFormatItem) REFERENCES FormatsItems (idFormatItem);

/*Création du lien avec la foreign key de la table Client vers la table FormatsItemsClientsFactures */
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT Clients_FormatsItemsClientsFactures_FK
FOREIGN KEY (idClient) REFERENCES Clients(idClient);

/*Création du lien avec la foreign key de la table FormatsItemsClientsFactures vers la table Factures*/
ALTER TABLE FormatsItemsClientsFactures 
ADD CONSTRAINT Factures_FormatsItemsClientsFactures_FK
FOREIGN KEY (idFacture) REFERENCES Factures (idFacture);

/*Création du lien avec la foreign key de la table FormatsItemsClientsFactures vers la table Complements*/
ALTER TABLE Complements
ADD CONSTRAINT FormatsItemsClientsFactures_Complements_FK
FOREIGN KEY (idFormatItemClientFacture) REFERENCES FormatsItemsClientsFactures (idFormatItemClientFacture);


/*Données*/
INSERT INTO `tables` VALUES (1,5,0,0),(2,7,1,0),(3,1,0,0),(4,101,1,0),(5,20,1,0),(6,301,1,0),(7,40567,1,0),(8,2,1,0),(10,30,1,0),(13,0,1,1);
INSERT INTO `categories` VALUES (2,'Enfants','','\0'),(3,'Grillades','','\0'),(4,'Pâtes','\0','\0'),(5,'Burgers','','\0'),(7,'Dessert','','\0'),(25,'Extras','',''),(26,'Cuisson','',''),(27,'Accompagnements','',''),(28,'Bacons','','\0'),(94,'Fast Food','','\0'),(95,'Poulet','','\0'),(96,'Demandes Spéciales','','');
INSERT INTO `formats` VALUES (1,'Petit','P',1),(4,'Moyen','M',1),(11,'Grand','G',1),(18,'Très Grand','TG',1),(26,'Extrême','EXT',0),(29,'Minuscule','MIN',1),(43,'Gigantesque','GG',1),(44,'Gros comme ','Uni',1),(45,'11 onzes','11O',1);
INSERT INTO `items` VALUES (1,3,'Hot dog',''),(2,94,'Pizza',''),(3,2,'Poutine',''),(14,3,'Steak',''),(22,4,'Linguini',''),(30,28,'Coke',''),(58,28,'Orange Crush',''),(59,28,'Sprite',''),(69,26,'Medium',''),(71,5,'Club Sandwich',''),(72,2,'Popcorn',''),(73,26,'Saignant',''),(74,26,'Bien cuit',''),(75,27,'Entrez le nom deitem pour f','\0'),(76,4,'Lasagne','\0'),(77,95,'Quart de poulet poitrine',''),(78,95,'Quart de poulet cuisse',''),(79,28,'Eau',''),(80,25,'Bacon',''),(81,96,'Pas de glace','');
INSERT INTO `formatsitems` VALUES (51,29,1,6),(56,11,72,4.5),(59,1,58,5.95),(60,4,58,6),(61,11,58,7),(62,1,72,3.1),(63,1,2,6.12),(64,4,2,10.8),(65,11,2,20.24),(66,1,30,5.95),(67,4,30,6.45),(68,11,30,7.12),(69,4,59,5),(70,11,59,6),(71,44,1,16),(72,1,74,0),(73,4,75,19),(74,4,76,12.25),(75,4,71,20),(76,4,77,10),(77,4,78,10),(78,4,79,1.55),(79,1,80,2.5),(80,11,80,5),(81,4,81,0);
INSERT INTO `commandes` VALUES (11,1,'Payé','2014-10-11 15:55:20','2014-10-25 15:55:22'),(12,1,'Active','2014-11-12 10:24:56',NULL),(13,1,'Active','2014-11-12 10:28:05',NULL),(14,1,'Active','2014-11-12 10:32:20',NULL);
INSERT INTO `tablescommandes` VALUES (1,13,11),(2,4,12),(3,2,13),(4,6,14);