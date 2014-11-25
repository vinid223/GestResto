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
INSERT INTO `categories` VALUES (2,'Menu Enfants','','\0'),(3,'Grillades','','\0'),(4,'Pâtes','\0','\0'),(5,'Burgers','','\0'),(7,'Dessert','','\0'),(25,'Extras','',''),(26,'Cuisson','',''),(27,'Accompagnements','',''),(28,'Viande fumée','','\0'),(94,'Nourriture Rapide','','\0'),(95,'Poulet','','\0'),(96,'Demandes Spéciales','','');
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
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES (21,NULL,11,13),(25,NULL,12,17),(26,NULL,21,18),(27,NULL,NULL,19),(28,NULL,11,20),(29,NULL,11,21),(30,NULL,11,22),(31,NULL,13,23),(32,NULL,11,24),(33,NULL,11,25),(35,NULL,NULL,27),(36,NULL,NULL,28),(42,NULL,23,34),(43,NULL,24,35),(44,NULL,23,36),(46,NULL,23,38),(47,NULL,23,39),(48,NULL,25,40),(49,NULL,23,41),(50,NULL,20,42),(51,NULL,14,43),(52,NULL,19,44),(53,NULL,22,45),(54,NULL,23,46),(55,NULL,26,47),(56,NULL,22,48),(57,NULL,22,49),(58,NULL,27,50),(59,NULL,28,51);
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
  `arrivee` datetime NOT NULL,
  `depart` datetime DEFAULT NULL,
  PRIMARY KEY (`idCommande`),
  KEY `Employes_Commandes_FK` (`idEmploye`),
  CONSTRAINT `Employes_Commandes_FK` FOREIGN KEY (`idEmploye`) REFERENCES `employes` (`idEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandes`
--

/*!40000 ALTER TABLE `commandes` DISABLE KEYS */;
INSERT INTO `commandes` VALUES (11,1,'Active','2014-10-11 15:55:20','2014-10-25 15:55:22'),(12,1,'Active','2014-11-12 10:24:56',NULL),(13,1,'Active','2014-11-12 10:28:05',NULL),(14,1,'Active','2014-11-12 10:32:20',NULL),(19,1,'Active','2014-11-18 11:36:14',NULL),(20,1,'Active','2014-11-18 11:37:19',NULL),(21,1,'Active','2014-11-20 10:52:06',NULL),(22,1,'Active','2014-11-20 14:24:20',NULL),(23,4,'Active','2014-11-20 14:47:08',NULL),(24,1,'Payé','2014-11-20 15:28:26','2014-11-25 10:04:54'),(25,1,'Active','2014-11-20 15:38:12',NULL),(26,1,'Active','2014-11-20 16:11:19','2014-11-23 18:04:59'),(27,1,'Active','2014-11-23 18:11:19','2014-11-23 18:11:35'),(28,1,'Active','2014-11-23 21:40:20',NULL);
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
  `idRestaurant` int(11) NOT NULL,
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
  KEY `Types_Employes_FK` (`idTypeEmploye`),
  CONSTRAINT `Types_Employes_FK` FOREIGN KEY (`idTypeEmploye`) REFERENCES `typesemploye` (`idTypeEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employes`
--

/*!40000 ALTER TABLE `employes` DISABLE KEYS */;
INSERT INTO `employes` VALUES (1,2,1,'Joe','LaMarche','11','OMW4d6HGe0apyrsXmH7P9w==','Le mot de passe est 11','Administrateur','G1Q1Q9','123456789',0.95,'1231111111',1),(2,1,0,'Admin','Admin','33','fFmdvN/EDTYyPmc/onPqxg==','33','33','f2f2f2','111111111',0,'1111111111',1),(3,1,0,'Desrosiers','Vincent','22','QG2AGfcXBsOrL6Y8OUfZQg==','861 rue du muguet','Sainte-Agathe-des-Monts','J8C2Z7','987897987',1000,'8193236308',0),(4,2,0,'Demers','Tommy','00','NZiF8Kspe3WID16QCEan/g==','660 rue filion','Saint-Jérôme','J7Z5X1','123222950',100,'4508583545',1);
/*!40000 ALTER TABLE `employes` ENABLE KEYS */;

--
-- Table structure for table `factures`
--

DROP TABLE IF EXISTS `factures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `factures` (
  `idFacture` int(11) NOT NULL AUTO_INCREMENT,
  `dateDeCreation` datetime NOT NULL,
  `pourcentageTaxe` float NOT NULL,
  PRIMARY KEY (`idFacture`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factures`
--

/*!40000 ALTER TABLE `factures` DISABLE KEYS */;
INSERT INTO `factures` VALUES (1,'2014-11-20 00:00:00',0.14975),(10,'2014-11-20 10:52:30',0.14975),(11,'2014-11-20 10:52:40',0.14975),(12,'2014-11-20 11:02:22',0.14975),(13,'2014-11-20 11:05:07',0.14975),(14,'2014-11-20 11:08:33',0.14975),(15,'2014-11-20 11:09:01',0.14975),(16,'2014-11-20 11:10:34',0.14975),(17,'2014-11-20 11:16:57',0.14975),(18,'2014-11-20 11:17:41',0.14975),(19,'2014-11-20 12:00:11',0.14975),(20,'2014-11-20 12:00:11',0.14975),(21,'2014-11-20 12:00:35',0.14975),(22,'2014-11-20 12:04:54',0.14975),(23,'2014-11-20 13:51:06',0.14975),(24,'2014-11-20 14:42:39',0.14975),(25,'2014-11-20 14:44:31',0.14975),(27,'2014-11-20 14:56:47',0.14975),(28,'2014-11-20 14:57:36',0.14975),(34,'2014-11-20 15:26:36',0.14975),(35,'2014-11-20 15:28:28',0.14975),(36,'2014-11-20 15:32:18',0.14975),(38,'2014-11-20 15:36:36',0.14975),(39,'2014-11-20 15:37:35',0.14975),(40,'2014-11-20 15:38:15',0.14975),(41,'2014-11-20 15:39:20',0.14975),(42,'2014-11-20 15:39:24',0.14975),(43,'2014-11-20 15:39:32',0.14975),(44,'2014-11-20 15:39:33',0.14975),(45,'2014-11-20 15:39:36',0.14975),(46,'2014-11-20 15:42:08',0.14975),(47,'2014-11-20 16:11:19',0.14975),(48,'2014-11-23 16:48:39',0.14975),(49,'2014-11-23 16:48:47',0.14975),(50,'2014-11-23 18:11:21',0.14975),(51,'2014-11-23 21:40:24',0.14975);
/*!40000 ALTER TABLE `factures` ENABLE KEYS */;

--
-- Table structure for table `formats`
--

DROP TABLE IF EXISTS `formats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `formats` (
  `idFormat` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  `libelle` varchar(3) NOT NULL,
  `actif` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idFormat`),
  UNIQUE KEY `nom` (`nom`),
  UNIQUE KEY `libelle` (`libelle`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formats`
--

/*!40000 ALTER TABLE `formats` DISABLE KEYS */;
INSERT INTO `formats` VALUES (1,'Petits','P',1),(4,'Moyen','M',1),(11,'Grand','G',1),(18,'Très Grand','TG',1),(26,'Extrême','EXT',0),(29,'Minuscule','Min',1),(43,'Gigantesque','GG',1),(44,'Gros comme ','Uni',0),(45,'11 onzes','11O',0);
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
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitems`
--

/*!40000 ALTER TABLE `formatsitems` DISABLE KEYS */;
INSERT INTO `formatsitems` VALUES (51,29,1,6),(56,11,72,4.5),(59,1,58,5.95),(60,4,58,6),(61,11,58,7),(62,1,72,3.1),(63,1,2,6.12),(64,4,2,10.8),(65,11,2,20.24),(66,1,30,5.95),(67,4,30,6.45),(68,11,30,7.12),(69,4,59,5),(70,11,59,6),(71,44,1,16),(72,1,74,0),(73,4,75,19),(74,4,76,12.25),(75,4,71,20),(76,4,77,10),(77,4,78,10),(78,4,79,1.55),(79,1,80,2.5),(80,11,80,5),(81,4,81,0),(82,4,73,0),(83,4,69,0),(84,4,3,0),(85,4,14,14),(86,44,22,5.25);
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
  `idClient` int(11) DEFAULT NULL,
  `idFacture` int(11) DEFAULT NULL,
  `prix` float NOT NULL,
  `idFicfParent` int(11) DEFAULT NULL,
  `complementaire` bit(1) DEFAULT b'0',
  PRIMARY KEY (`idFormatItemClientFacture`),
  KEY `FormatsItems_FormatsItemsClientsFactures_FK` (`idFormatItem`),
  KEY `Clients_FormatsItemsClientsFactures_FK` (`idClient`),
  KEY `Factures_FormatsItemsClientsFactures_FK` (`idFacture`),
  KEY `FormatsItemsClientsFactures_FormatsItemsClientsFactures_FK` (`idFicfParent`),
  CONSTRAINT `Clients_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `Factures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `FormatsItemsClientsFactures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFicfParent`) REFERENCES `formatsitemsclientsfactures` (`idFormatItemClientFacture`),
  CONSTRAINT `FormatsItems_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFormatItem`) REFERENCES `formatsitems` (`idFormatItem`)
) ENGINE=InnoDB AUTO_INCREMENT=86 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitemsclientsfactures`
--

/*!40000 ALTER TABLE `formatsitemsclientsfactures` DISABLE KEYS */;
INSERT INTO `formatsitemsclientsfactures` VALUES (12,51,21,13,6,NULL,'\0'),(13,71,21,13,16,NULL,'\0'),(14,82,21,13,14.99,13,''),(15,71,30,22,16,NULL,'\0'),(17,51,21,13,6,NULL,'\0'),(20,64,21,13,10.8,NULL,'\0'),(23,71,21,13,16,NULL,'\0'),(24,84,21,13,0,NULL,'\0'),(25,85,21,13,14,NULL,'\0'),(26,69,21,13,5,NULL,'\0'),(27,71,21,13,16,NULL,'\0'),(28,71,28,20,16,NULL,'\0'),(29,84,28,20,0,NULL,'\0'),(30,64,28,20,10.8,NULL,'\0'),(31,71,21,13,16,NULL,'\0'),(32,70,28,20,6,NULL,'\0'),(33,71,29,21,16,NULL,'\0'),(34,75,33,25,20,NULL,'\0'),(35,77,33,25,10,NULL,'\0'),(36,85,33,25,14,NULL,'\0'),(42,70,35,27,6,NULL,'\0'),(47,84,44,36,0,NULL,'\0'),(48,70,46,38,6,NULL,'\0'),(50,65,47,39,20.24,NULL,'\0'),(51,71,49,41,16,NULL,'\0'),(52,65,31,23,20.24,NULL,'\0'),(53,56,49,41,4.5,NULL,'\0'),(54,56,31,23,4.5,NULL,'\0'),(55,78,49,41,1.55,NULL,'\0'),(56,77,31,23,10,NULL,'\0'),(57,64,54,46,10.8,NULL,'\0'),(58,80,21,13,5,20,''),(59,79,21,13,2.5,20,''),(60,79,21,13,2.5,20,''),(62,71,53,45,16,NULL,'\0'),(63,65,56,48,20.24,NULL,'\0'),(64,86,57,49,5.25,NULL,'\0'),(65,76,21,13,10,NULL,'\0'),(67,81,28,20,0,30,''),(71,51,42,34,6,NULL,'\0'),(73,81,42,34,0,71,''),(74,84,42,34,0,NULL,'\0'),(75,72,28,20,0,30,''),(78,75,59,51,20,NULL,'\0'),(80,60,59,51,6,NULL,'\0'),(82,75,53,45,20,NULL,'\0'),(84,71,42,34,16,NULL,'\0');
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
INSERT INTO `items` VALUES (1,3,'Hot dog',''),(2,94,'Pizza',''),(3,2,'Poutine',''),(14,3,'Steak',''),(22,4,'Linguini',''),(30,28,'Coke',''),(58,28,'Orange Crush',''),(59,28,'Sprite',''),(69,26,'Medium',''),(71,5,'Club Sandwich',''),(72,2,'Popcorn',''),(73,26,'Saignant',''),(74,26,'Bien cuit',''),(75,27,'Entrez le nom deitem pour f','\0'),(76,4,'Lasagne','\0'),(77,95,'Quart de poulet poitrine',''),(78,95,'Quart de poulet cuisse',''),(79,28,'Eau',''),(80,25,'Bacon',''),(81,96,'Pas de glace','');
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
INSERT INTO `restaurants` VALUES (1,'GestResto','Version en cour : 1.0','0101010101','0101010101','GestResto','A1A1A1','2014-10-16 17:10:19');
/*!40000 ALTER TABLE `restaurants` ENABLE KEYS */;

--
-- Table structure for table `tables`
--

DROP TABLE IF EXISTS `tables`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tables` (
  `idTable` int(11) NOT NULL AUTO_INCREMENT,
  `noTable` varchar(20) NOT NULL,
  `actif` tinyint(1) DEFAULT NULL,
  `assigne` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idTable`),
  UNIQUE KEY `noTable` (`noTable`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tables`
--

/*!40000 ALTER TABLE `tables` DISABLE KEYS */;
INSERT INTO `tables` VALUES (1,'TEST FICF',0,0),(2,'TEST1',1,0),(3,'1',0,0),(5,'20',1,0),(6,'301',1,0),(7,'4',1,1),(8,'2',1,1),(10,'32',1,1),(14,'TEST2',1,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablescommandes`
--

/*!40000 ALTER TABLE `tablescommandes` DISABLE KEYS */;
INSERT INTO `tablescommandes` VALUES (4,6,14),(5,10,19),(6,8,19),(7,2,19),(8,10,20),(9,6,20),(10,10,21),(27,7,24),(31,7,25),(32,8,25),(37,2,13),(38,2,23),(40,1,11),(41,6,26),(42,10,26),(46,8,22),(49,6,27),(50,10,27),(51,10,28),(52,14,28);
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

-- Dump completed on 2014-11-25 10:15:48
