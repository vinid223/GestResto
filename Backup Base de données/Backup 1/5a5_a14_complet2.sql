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
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'test','','\0'),(2,'Enfants','','\0'),(3,'Grillades','','\0'),(4,'Pâtes','\0','\0'),(5,'Burgers','','\0'),(7,'Dessert','','\0'),(25,'Extras','',''),(26,'Cuisson','',''),(27,'Accompagnement','',''),(28,'Boisson','',''),(30,'YOLO','','');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clients` (
  `idClient` int(11) NOT NULL AUTO_INCREMENT,
  `idTable` int(11) NOT NULL,
  `idCommande` int(11) NOT NULL,
  PRIMARY KEY (`idClient`),
  KEY `Tables_Clients_FK` (`idTable`),
  KEY `Commandes_Clients_FK` (`idCommande`),
  CONSTRAINT `Commandes_Clients_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`),
  CONSTRAINT `Tables_Clients_FK` FOREIGN KEY (`idTable`) REFERENCES `tables` (`idTable`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandes`
--

LOCK TABLES `commandes` WRITE;
/*!40000 ALTER TABLE `commandes` DISABLE KEYS */;
/*!40000 ALTER TABLE `commandes` ENABLE KEYS */;
UNLOCK TABLES;

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

LOCK TABLES `complements` WRITE;
/*!40000 ALTER TABLE `complements` DISABLE KEYS */;
/*!40000 ALTER TABLE `complements` ENABLE KEYS */;
UNLOCK TABLES;

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
  KEY `Restaurants_Employes_FK` (`idRestaurant`),
  KEY `Types_Employes_FK` (`idTypeEmploye`),
  CONSTRAINT `Types_Employes_FK` FOREIGN KEY (`idTypeEmploye`) REFERENCES `typesemploye` (`idTypeEmploye`),
  CONSTRAINT `Restaurants_Employes_FK` FOREIGN KEY (`idRestaurant`) REFERENCES `restaurants` (`idRestaurant`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employes`
--

LOCK TABLES `employes` WRITE;
/*!40000 ALTER TABLE `employes` DISABLE KEYS */;
INSERT INTO `employes` VALUES (1,2,1,'Charron','Yannick','11','OMW4d6HGe0apyrsXmH7P9w==','Le mot de passe est 11','Dans-la-rue','G1Q1Q9','123456789',0.95,NULL,1),(3,1,1,'Test','Test','0001','WM40eCZk92uRSFxSozlqxw==','test','test','test','test',19,'test',1),(5,1,1,'Evoescu','Catalin','33','fFmdvN/EDTYyPmc/onPqxg==','Le mot de passe est 33','Saint-Jérôme','H0H0H0','123123123',100.8,NULL,1);
/*!40000 ALTER TABLE `employes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `factures`
--

DROP TABLE IF EXISTS `factures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `factures` (
  `idFacture` int(11) NOT NULL AUTO_INCREMENT,
  `idCommande` int(11) NOT NULL,
  `dateDeCreation` datetime NOT NULL,
  `pourcentageTaxe` float NOT NULL,
  PRIMARY KEY (`idFacture`),
  KEY `Commandes_Factures_FK` (`idCommande`),
  CONSTRAINT `Commandes_Factures_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factures`
--

LOCK TABLES `factures` WRITE;
/*!40000 ALTER TABLE `factures` DISABLE KEYS */;
/*!40000 ALTER TABLE `factures` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formats`
--

LOCK TABLES `formats` WRITE;
/*!40000 ALTER TABLE `formats` DISABLE KEYS */;
INSERT INTO `formats` VALUES (1,'Petit','P',1),(4,'Moyen','M',1),(11,'Grand','G',1),(18,'Très Grand','TG',1),(26,'Extrême','EXT',0),(29,'Minuscule','MIN',1),(43,'Gigantesque','GIG',1);
/*!40000 ALTER TABLE `formats` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitems`
--

LOCK TABLES `formatsitems` WRITE;
/*!40000 ALTER TABLE `formatsitems` DISABLE KEYS */;
INSERT INTO `formatsitems` VALUES (10,18,22,0),(12,4,1,10);
/*!40000 ALTER TABLE `formatsitems` ENABLE KEYS */;
UNLOCK TABLES;

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
  CONSTRAINT `Factures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `Clients_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `FormatsItems_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFormatItem`) REFERENCES `formatsitems` (`idFormatItem`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitemsclientsfactures`
--

LOCK TABLES `formatsitemsclientsfactures` WRITE;
/*!40000 ALTER TABLE `formatsitemsclientsfactures` DISABLE KEYS */;
/*!40000 ALTER TABLE `formatsitemsclientsfactures` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (1,2,'testTomm',''),(2,2,'Pizza',''),(3,2,'Poutine',''),(8,3,'test4',''),(14,3,'Steak',''),(16,4,'Manger',''),(18,4,'Muffin',''),(21,3,'Entrez le nom de l\'',''),(22,4,'Linguini','\0'),(24,3,'kjgvkgh',''),(26,NULL,'Entrez le nom de l\'item',''),(28,NULL,'Entrez le nom de l\'ite',''),(30,NULL,'Entrez le nom de l\'item.','');
/*!40000 ALTER TABLE `items` ENABLE KEYS */;
UNLOCK TABLES;

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

LOCK TABLES `restaurants` WRITE;
/*!40000 ALTER TABLE `restaurants` DISABLE KEYS */;
INSERT INTO `restaurants` VALUES (1,'Bozo','99 rue de la perfection','4500000000','654654654','Paradis','H0H0H0','2014-10-16 17:10:19');
/*!40000 ALTER TABLE `restaurants` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tables`
--

LOCK TABLES `tables` WRITE;
/*!40000 ALTER TABLE `tables` DISABLE KEYS */;
INSERT INTO `tables` VALUES (1,4,1,0),(2,6,1,0);
/*!40000 ALTER TABLE `tables` ENABLE KEYS */;
UNLOCK TABLES;

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
  CONSTRAINT `Tables_TablesCommandes_FK` FOREIGN KEY (`idTable`) REFERENCES `tables` (`idTable`),
  CONSTRAINT `Commandes_TablesCommandes_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablescommandes`
--

LOCK TABLES `tablescommandes` WRITE;
/*!40000 ALTER TABLE `tablescommandes` DISABLE KEYS */;
/*!40000 ALTER TABLE `tablescommandes` ENABLE KEYS */;
UNLOCK TABLES;

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

LOCK TABLES `typesemploye` WRITE;
/*!40000 ALTER TABLE `typesemploye` DISABLE KEYS */;
INSERT INTO `typesemploye` VALUES (1,'Administrateur'),(2,'Serveur');
/*!40000 ALTER TABLE `typesemploye` ENABLE KEYS */;
UNLOCK TABLES;

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

-- Dump completed on 2014-10-21 10:25:29
