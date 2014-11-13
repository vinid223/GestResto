CREATE DATABASE  IF NOT EXISTS `5a5_a14_gestresto` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `5a5_a14_gestresto`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: 420.cstj.qc.ca    Database: 5a5_a14_gestresto
-- ------------------------------------------------------
-- Server version	5.5.8-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categories` (
  `idCategorie` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `actif` bit(1) DEFAULT b'1',
  `complementaire` bit(1) DEFAULT b'0',
  PRIMARY KEY (`idCategorie`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB AUTO_INCREMENT=97 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (2,'Enfants','','\0'),(3,'Grillades','','\0'),(4,'Pâtes','\0','\0'),(5,'Burgers','','\0'),(7,'Dessert','','\0'),(25,'Extras','',''),(26,'Cuisson','',''),(27,'Accompagnements','',''),(28,'Bacons','','\0'),(94,'Fast Food','','\0'),(95,'Poulet','','\0'),(96,'Demandes Spéciales','','');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clients` (
  `idClient` int(11) NOT NULL AUTO_INCREMENT,
  `idTable` int(11) DEFAULT NULL,
  `idCommande` int(11) DEFAULT NULL,
  `idFacture` int(11) DEFAULT NULL,
  PRIMARY KEY (`idClient`),
  KEY `Tables_Clients_FK` (`idTable`),
  KEY `Commandes_Clients_FK` (`idCommande`),
  KEY `Factures_Clients_FK` (`idFacture`),
  CONSTRAINT `Commandes_Clients_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`),
  CONSTRAINT `Factures_Clients_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `Tables_Clients_FK` FOREIGN KEY (`idTable`) REFERENCES `tables` (`idTable`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES (1,1,11,NULL),(2,1,11,NULL),(15,NULL,NULL,NULL),(16,NULL,NULL,NULL),(17,NULL,NULL,NULL);
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;

--
-- Table structure for table `commandes`
--

DROP TABLE IF EXISTS `commandes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `commandes` (
  `idCommande` int(11) NOT NULL AUTO_INCREMENT,
  `idEmploye` int(11) NOT NULL,
  `status` enum('Active','Payé','En attente') DEFAULT NULL,
  `arrivee` datetime DEFAULT NULL,
  `depart` datetime DEFAULT NULL,
  PRIMARY KEY (`idCommande`),
  KEY `Employes_Commandes_FK` (`idEmploye`),
  CONSTRAINT `Employes_Commandes_FK` FOREIGN KEY (`idEmploye`) REFERENCES `employes` (`idEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandes`
--

/*!40000 ALTER TABLE `commandes` DISABLE KEYS */;
INSERT INTO `commandes` VALUES (11,1,'Payé','2014-10-11 15:55:20','2014-10-25 15:55:22'),(12,1,'Active','2014-11-12 10:24:56',NULL),(13,1,'Active','2014-11-12 10:28:05',NULL),(14,1,'Active','2014-11-12 10:32:20',NULL);
/*!40000 ALTER TABLE `commandes` ENABLE KEYS */;

--
-- Table structure for table `complements`
--

DROP TABLE IF EXISTS `complements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `complements` (
  `idComplement` int(11) NOT NULL AUTO_INCREMENT,
  `idFormatItem` int(11) NOT NULL,
  `idFormatItemClientFacture` int(11) NOT NULL,
  `prix` float NOT NULL,
  PRIMARY KEY (`idComplement`),
  KEY `FormatsItems_Complements_FK` (`idFormatItem`),
  KEY `FormatsItemsClientsFactures_Complements_FK` (`idFormatItemClientFacture`),
  CONSTRAINT `FormatsItemsClientsFactures_Complements_FK` FOREIGN KEY (`idFormatItemClientFacture`) REFERENCES `formatsitemsclientsfactures` (`idFormatItemClientFacture`),
  CONSTRAINT `FormatsItems_Complements_FK` FOREIGN KEY (`idFormatItem`) REFERENCES `formatsitems` (`idFormatItem`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `complements`
--

/*!40000 ALTER TABLE `complements` DISABLE KEYS */;
/*!40000 ALTER TABLE `complements` ENABLE KEYS */;

--
-- Table structure for table `employes`
--

DROP TABLE IF EXISTS `employes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employes` (
  `idEmploye` int(11) NOT NULL AUTO_INCREMENT,
  `idTypeEmploye` int(11) NOT NULL,
  `idRestaurant` int(11) DEFAULT NULL,
  `nom` varchar(30) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `noEmploye` varchar(255) NOT NULL,
  `motPasse` varchar(255) NOT NULL,
  `adresse` varchar(50) NOT NULL,
  `ville` varchar(30) NOT NULL,
  `codePostal` varchar(6) NOT NULL,
  `NAS` varchar(9) NOT NULL,
  `tauxHoraire` float NOT NULL,
  `telephone` varchar(11) DEFAULT NULL,
  `actif` tinyint(1) NOT NULL,
  PRIMARY KEY (`idEmploye`),
  UNIQUE KEY `noEmploye` (`noEmploye`),
  UNIQUE KEY `NAS` (`NAS`),
  KEY `Restaurants_Employes_FK` (`idRestaurant`),
  KEY `Types_Employes_FK` (`idTypeEmploye`),
  CONSTRAINT `Restaurants_Employes_FK` FOREIGN KEY (`idRestaurant`) REFERENCES `restaurants` (`idRestaurant`),
  CONSTRAINT `Types_Employes_FK` FOREIGN KEY (`idTypeEmploye`) REFERENCES `typesemploye` (`idTypeEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employes`
--

/*!40000 ALTER TABLE `employes` DISABLE KEYS */;
INSERT INTO `employes` VALUES (1,2,1,'Charron','Yannick','11','OMW4d6HGe0apyrsXmH7P9w==','Le mot de passe est 11','Dans-la-rue','G1Q1Q9','123456789',0.95,'0121345678',1),(3,1,1,'Desrosiers','Vincents','0001','WM40eCZk92uRSFxSozlqxw==','861 rue du Muguet','Sainte-Agathe-des-Monts','J8C2Z7','999999999',10,'8193236308',1),(5,1,1,'Evoescus','Catalin','33','fFmdvN/EDTYyPmc/onPqxg==','Le mot de passe est 33','Saint-Jérôme','H0H0H0','123123123',100.8,'1',1),(8,2,NULL,'Desrosiers','Vincents','22','QG2AGfcXBsOrL6Y8OUfZQg==','861 rue du muguet','Sainte-Agathe','J8C2Z7','000111111',1000,'8193236308',1),(11,1,NULL,'Veuillez entrer les informatio','444','4','9Aqih3ZY2sUHdNDu9DPY0A==','','','','456456456',4.47,'',0),(13,1,NULL,'Veuillez entrer les informatio','h','3','fFmdvN/EDTYyPmc/onPqxg==','','','','456789632',0,'',0),(18,1,NULL,'Veuillez entrer les informatio','','','','','','','',0,'',0);
/*!40000 ALTER TABLE `employes` ENABLE KEYS */;

--
-- Table structure for table `factures`
--

DROP TABLE IF EXISTS `factures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `factures` (
  `idFacture` int(11) NOT NULL AUTO_INCREMENT,
  `idClient` int(11) NOT NULL,
  `idCommande` int(11) DEFAULT NULL,
  `dateDeCreation` datetime NOT NULL,
  `pourcentageTaxe` float NOT NULL,
  PRIMARY KEY (`idFacture`),
  KEY `Commandes_Factures_FK` (`idCommande`),
  KEY `Clients_Factures_FK` (`idClient`),
  CONSTRAINT `Clients_Factures_FK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `Commandes_Factures_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factures`
--

/*!40000 ALTER TABLE `factures` DISABLE KEYS */;
INSERT INTO `factures` VALUES (1,1,NULL,'2014-11-05 09:36:21',0.15);
/*!40000 ALTER TABLE `factures` ENABLE KEYS */;

--
-- Table structure for table `formats`
--

DROP TABLE IF EXISTS `formats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `formats` (
  `idFormat` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) NOT NULL,
  `libelle` varchar(3) NOT NULL,
  `actif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idFormat`),
  UNIQUE KEY `nom` (`nom`),
  UNIQUE KEY `libelle` (`libelle`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formats`
--

/*!40000 ALTER TABLE `formats` DISABLE KEYS */;
INSERT INTO `formats` VALUES (1,'Petit','P',1),(4,'Moyen','M',1),(11,'Grand','G',1),(18,'Très Grand','TG',1),(26,'Extrême','EXT',0),(29,'Minuscule','MIN',1),(43,'Gigantesque','GG',1),(44,'Gros comme l\'univers','Uni',1),(45,'11 onzes','11O',1);
/*!40000 ALTER TABLE `formats` ENABLE KEYS */;

--
-- Table structure for table `formatsitems`
--

DROP TABLE IF EXISTS `formatsitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `formatsitems` (
  `idFormatItem` int(11) NOT NULL AUTO_INCREMENT,
  `idFormat` int(11) DEFAULT NULL,
  `idItem` int(11) DEFAULT NULL,
  `prix` float NOT NULL,
  PRIMARY KEY (`idFormatItem`),
  KEY `Items_FormatsItems_FK` (`idItem`),
  KEY `Formats_FormatsItems_FK` (`idFormat`),
  CONSTRAINT `Formats_FormatsItems_FK` FOREIGN KEY (`idFormat`) REFERENCES `formats` (`idFormat`),
  CONSTRAINT `Items_FormatsItems_FK` FOREIGN KEY (`idItem`) REFERENCES `items` (`idItem`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitems`
--

/*!40000 ALTER TABLE `formatsitems` DISABLE KEYS */;
INSERT INTO `formatsitems` VALUES (51,29,1,6),(56,11,72,4.5),(59,1,58,5.95),(60,4,58,6),(61,11,58,7),(62,1,72,3.1),(63,1,2,6.12),(64,4,2,10.8),(65,11,2,20.24),(66,1,30,5.95),(67,4,30,6.45),(68,11,30,7.12),(69,4,59,5),(70,11,59,6),(71,44,1,16),(72,1,74,0),(73,4,75,19),(74,4,76,12.25),(75,4,71,20),(76,4,77,10),(77,4,78,10),(78,4,79,1.55),(79,1,80,2.5),(80,11,80,5),(81,4,81,0);
/*!40000 ALTER TABLE `formatsitems` ENABLE KEYS */;

--
-- Table structure for table `formatsitemsclientsfactures`
--

DROP TABLE IF EXISTS `formatsitemsclientsfactures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `formatsitemsclientsfactures` (
  `idFormatItemClientFacture` int(11) NOT NULL AUTO_INCREMENT,
  `idFormatItem` int(11) NOT NULL,
  `idClient` int(11) NOT NULL,
  `idFacture` int(11) NOT NULL,
  `prix` float NOT NULL,
  PRIMARY KEY (`idFormatItemClientFacture`),
  KEY `FormatsItems_FormatsItemsClientsFactures_FK` (`idFormatItem`),
  KEY `Clients_FormatsItemsClientsFactures_FK` (`idClient`),
  KEY `Factures_FormatsItemsClientsFactures_FK` (`idFacture`),
  CONSTRAINT `Clients_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `Factures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `FormatsItems_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFormatItem`) REFERENCES `formatsitems` (`idFormatItem`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitemsclientsfactures`
--

/*!40000 ALTER TABLE `formatsitemsclientsfactures` DISABLE KEYS */;
INSERT INTO `formatsitemsclientsfactures` VALUES (1,51,1,1,83.55),(4,67,2,1,44),(5,68,1,1,7.12),(10,71,1,1,16),(11,75,1,1,20),(12,51,1,1,6),(13,81,1,1,44.5),(14,81,1,1,0),(15,72,1,1,0);
/*!40000 ALTER TABLE `formatsitemsclientsfactures` ENABLE KEYS */;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `items` (
  `idItem` int(11) NOT NULL AUTO_INCREMENT,
  `idCategorie` int(11) DEFAULT NULL,
  `nom` varchar(30) NOT NULL,
  `actif` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`idItem`),
  UNIQUE KEY `nom` (`nom`),
  KEY `Categories_Items_FK` (`idCategorie`),
  CONSTRAINT `Categories_Items_FK` FOREIGN KEY (`idCategorie`) REFERENCES `categories` (`idCategorie`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (1,3,'Hot dog',''),(2,94,'Pizza',''),(3,2,'Poutine',''),(14,3,'Steak',''),(22,4,'Linguini',''),(30,28,'Coke',''),(58,28,'Orange Crush',''),(59,28,'Sprite',''),(69,26,'Medium',''),(71,5,'Club Sandwich',''),(72,2,'Popcorn',''),(73,26,'Saignant',''),(74,26,'Bien cuit',''),(75,27,'Entrez le nom de l\'item pour f','\0'),(76,4,'Lasagne','\0'),(77,95,'Quart de poulet poitrine',''),(78,95,'Quart de poulet cuisse',''),(79,28,'Eau',''),(80,25,'Bacon',''),(81,96,'Pas de glace','');
/*!40000 ALTER TABLE `items` ENABLE KEYS */;

--
-- Table structure for table `restaurants`
--

DROP TABLE IF EXISTS `restaurants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `restaurants` (
  `idRestaurant` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `adresse` varchar(30) NOT NULL,
  `telephone` varchar(11) NOT NULL,
  `fax` varchar(11) DEFAULT NULL,
  `ville` varchar(50) DEFAULT NULL,
  `codePostal` varchar(10) DEFAULT NULL,
  `dateCreation` datetime NOT NULL,
  PRIMARY KEY (`idRestaurant`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `restaurants`
--

/*!40000 ALTER TABLE `restaurants` DISABLE KEYS */;
INSERT INTO `restaurants` VALUES (1,'Chez Alain','99 rue de la perfection','8193236308','8193236307','Paradis','H0H0H0','2014-10-16 17:10:19');
/*!40000 ALTER TABLE `restaurants` ENABLE KEYS */;

--
-- Table structure for table `tables`
--

DROP TABLE IF EXISTS `tables`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tables` (
  `idTable` int(11) NOT NULL AUTO_INCREMENT,
  `noTable` int(11) NOT NULL,
  `actif` tinyint(1) DEFAULT NULL,
  `assigne` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idTable`),
  UNIQUE KEY `noTable` (`noTable`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tables`
--

/*!40000 ALTER TABLE `tables` DISABLE KEYS */;
INSERT INTO `tables` VALUES (1,5,0,0),(2,7,1,0),(3,1,0,0),(4,101,1,0),(5,20,1,0),(6,301,1,0),(7,40567,1,0),(8,2,1,0),(10,30,1,0),(13,0,1,1);
/*!40000 ALTER TABLE `tables` ENABLE KEYS */;

--
-- Table structure for table `tablescommandes`
--

DROP TABLE IF EXISTS `tablescommandes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tablescommandes` (
  `idTableCommande` int(11) NOT NULL AUTO_INCREMENT,
  `idTable` int(11) NOT NULL,
  `idCommande` int(11) NOT NULL,
  PRIMARY KEY (`idTableCommande`),
  KEY `Commandes_TablesCommandes_FK` (`idCommande`),
  KEY `Tables_TablesCommandes_FK` (`idTable`),
  CONSTRAINT `Commandes_TablesCommandes_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`),
  CONSTRAINT `Tables_TablesCommandes_FK` FOREIGN KEY (`idTable`) REFERENCES `tables` (`idTable`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablescommandes`
--

/*!40000 ALTER TABLE `tablescommandes` DISABLE KEYS */;
INSERT INTO `tablescommandes` VALUES (1,13,11),(2,4,12),(3,2,13),(4,6,14);
/*!40000 ALTER TABLE `tablescommandes` ENABLE KEYS */;

--
-- Table structure for table `typesemploye`
--

DROP TABLE IF EXISTS `typesemploye`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `typesemploye` (
  `idTypeEmploye` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`idTypeEmploye`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `typesemploye`
--

/*!40000 ALTER TABLE `typesemploye` DISABLE KEYS */;
INSERT INTO `typesemploye` VALUES (1,'Administrateur'),(2,'Serveur');
/*!40000 ALTER TABLE `typesemploye` ENABLE KEYS */;

--
-- Dumping routines for database '5a5_a14_gestresto'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
