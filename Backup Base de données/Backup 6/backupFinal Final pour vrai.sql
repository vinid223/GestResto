CREATE DATABASE  IF NOT EXISTS `5a5_a14_gestresto` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `5a5_a14_gestresto`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: 5a5_a14_gestresto
-- ------------------------------------------------------
-- Server version	5.6.16

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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Soupes','','\0'),(2,'Entrées','','\0'),(3,'Plat principaux','','\0'),(4,'Extra','',''),(5,'Boisson','','\0'),(6,'Pizza','','\0'),(7,'Cuisson','',''),(8,'Demandes spéciales','','');
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
  `idCommande` int(11) DEFAULT NULL,
  `idFacture` int(11) DEFAULT NULL,
  PRIMARY KEY (`idClient`),
  KEY `Commandes_Clients_FK` (`idCommande`),
  KEY `Factures_Clients_FK` (`idFacture`),
  CONSTRAINT `Factures_Clients_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `Commandes_Clients_FK` FOREIGN KEY (`idCommande`) REFERENCES `commandes` (`idCommande`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES (1,1,2),(2,2,3),(3,3,4),(4,3,5),(5,3,6),(6,4,7),(7,4,8),(8,4,9),(9,5,10),(10,4,11);
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandes`
--

LOCK TABLES `commandes` WRITE;
/*!40000 ALTER TABLE `commandes` DISABLE KEYS */;
INSERT INTO `commandes` VALUES (1,2,'Payé','2014-11-26 09:22:09','2014-11-26 09:22:15'),(2,2,'Payé','2014-11-26 09:25:35','2014-11-26 09:25:45'),(3,2,'Active','2014-11-26 09:29:27',NULL),(4,2,'Active','2014-11-26 09:31:16',NULL),(5,2,'Payé','2014-11-26 09:32:04','2014-11-26 09:32:16');
/*!40000 ALTER TABLE `commandes` ENABLE KEYS */;
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
  KEY `Types_Employes_FK` (`idTypeEmploye`),
  CONSTRAINT `Types_Employes_FK` FOREIGN KEY (`idTypeEmploye`) REFERENCES `typesemploye` (`idTypeEmploye`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employes`
--

LOCK TABLES `employes` WRITE;
/*!40000 ALTER TABLE `employes` DISABLE KEYS */;
INSERT INTO `employes` VALUES (1,1,1,'Admin','Admin','11','OMW4d6HGe0apyrsXmH7P9w==','Le mot de passe est 11','Administrateur','G1Q1Q9','123456789',0.95,'1231',1),(2,2,0,'Démonstration','Serveur','33','fFmdvN/EDTYyPmc/onPqxg==','Le mot de passe est 33','Montréal','A1A1A1','987654321',5,'8193232323',1);
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
  `dateDeCreation` datetime NOT NULL,
  `pourcentageTaxe` float NOT NULL,
  PRIMARY KEY (`idFacture`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factures`
--

LOCK TABLES `factures` WRITE;
/*!40000 ALTER TABLE `factures` DISABLE KEYS */;
INSERT INTO `factures` VALUES (1,'2014-11-25 11:17:37',0.14975),(2,'2014-11-26 09:22:09',0.14975),(3,'2014-11-26 09:25:35',0.14975),(4,'2014-11-26 09:29:27',0.14975),(5,'2014-11-26 09:29:53',0.14975),(6,'2014-11-26 09:30:45',0.14975),(7,'2014-11-26 09:31:16',0.14975),(8,'2014-11-26 09:31:28',0.14975),(9,'2014-11-26 09:31:37',0.14975),(10,'2014-11-26 09:32:04',0.14975),(11,'2014-11-26 10:05:53',0.14975);
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
  `nom` varchar(20) NOT NULL,
  `libelle` varchar(3) NOT NULL,
  `actif` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idFormat`),
  UNIQUE KEY `nom` (`nom`),
  UNIQUE KEY `libelle` (`libelle`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formats`
--

LOCK TABLES `formats` WRITE;
/*!40000 ALTER TABLE `formats` DISABLE KEYS */;
INSERT INTO `formats` VALUES (1,'Portion normale','M',1),(2,'Petit','P',1),(3,'Grand','G',1),(4,'Extra Grand','XG',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitems`
--

LOCK TABLES `formatsitems` WRITE;
/*!40000 ALTER TABLE `formatsitems` DISABLE KEYS */;
INSERT INTO `formatsitems` VALUES (1,1,1,5),(2,1,2,4),(3,1,3,4),(4,1,4,6),(5,2,5,4),(6,1,5,5),(7,3,5,6),(8,4,5,8),(9,1,6,8),(10,1,7,12),(11,1,8,15),(12,4,8,20),(13,1,9,8),(14,1,10,9),(15,1,11,13),(16,1,12,0),(17,1,13,0),(18,1,14,0),(19,2,15,1),(20,3,15,2),(21,4,15,3),(22,2,16,0.495),(23,3,16,1),(24,4,16,1.5),(25,2,17,2),(26,4,17,4),(27,2,18,1),(28,3,18,2),(29,3,19,1),(30,2,20,12),(31,3,20,15),(32,1,21,5),(33,1,22,5),(34,3,22,6),(35,1,23,6),(36,2,23,4),(37,3,23,8),(38,4,23,100),(39,1,24,0),(40,1,25,0),(41,1,26,0),(42,1,27,0);
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
  `idFicfParent` int(11) DEFAULT NULL,
  `complementaire` bit(1) DEFAULT b'0',
  `idFormatItem` int(11) NOT NULL,
  `idClient` int(11) DEFAULT NULL,
  `idFacture` int(11) DEFAULT NULL,
  `prix` float NOT NULL,
  PRIMARY KEY (`idFormatItemClientFacture`),
  KEY `FormatsItemsClientsFactures_FormatsItemsClientsFactures_FK` (`idFicfParent`),
  KEY `FormatsItems_FormatsItemsClientsFactures_FK` (`idFormatItem`),
  KEY `Clients_FormatsItemsClientsFactures_FK` (`idClient`),
  KEY `Factures_FormatsItemsClientsFactures_FK` (`idFacture`),
  CONSTRAINT `Factures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFacture`) REFERENCES `factures` (`idFacture`),
  CONSTRAINT `Clients_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `FormatsItemsClientsFactures_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFicfParent`) REFERENCES `formatsitemsclientsfactures` (`idFormatItemClientFacture`),
  CONSTRAINT `FormatsItems_FormatsItemsClientsFactures_FK` FOREIGN KEY (`idFormatItem`) REFERENCES `formatsitems` (`idFormatItem`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatsitemsclientsfactures`
--

LOCK TABLES `formatsitemsclientsfactures` WRITE;
/*!40000 ALTER TABLE `formatsitemsclientsfactures` DISABLE KEYS */;
INSERT INTO `formatsitemsclientsfactures` VALUES (1,NULL,'\0',1,3,4,5),(2,NULL,'\0',12,3,4,20),(3,NULL,'\0',30,3,4,12),(5,NULL,'\0',31,4,5,15),(6,NULL,'\0',32,4,5,5),(7,NULL,'\0',21,4,5,3),(8,NULL,'\0',6,5,6,5),(9,NULL,'\0',9,5,6,8),(10,NULL,'\0',10,6,7,12),(11,NULL,'\0',23,6,7,1),(12,NULL,'\0',23,6,7,1),(13,NULL,'\0',13,7,8,8),(14,NULL,'\0',14,7,8,9),(15,NULL,'\0',32,8,9,5),(16,NULL,'\0',21,8,9,3),(17,NULL,'\0',9,8,9,8),(18,2,'',18,3,4,0),(19,NULL,'\0',38,10,11,100),(20,NULL,'\0',29,10,11,1),(21,NULL,'\0',24,10,11,1.5),(22,NULL,'\0',32,10,11,5),(23,19,'',18,10,11,0),(24,21,'',42,10,11,0),(25,NULL,'\0',24,5,6,1.5),(30,25,'',42,5,6,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (1,1,'TOM YUM',''),(2,1,'Soupe Régulière',''),(3,2,'Rouleau Impérial',''),(4,2,'Rouleau De Printemps',''),(5,2,'Salade - Viande au choix',''),(6,2,'Raviolis au Poulet',''),(7,3,'Cari - Viande au choix',''),(8,3,'Général Thaï',''),(9,3,'Riz Frit',''),(10,3,'Sauté',''),(11,3,'Pad Thaï',''),(12,4,'Épicé',''),(13,4,'Très épicé',''),(14,4,'Extra épicé',''),(15,5,'Coke',''),(16,5,'Eau',''),(17,5,'Sprite',''),(18,5,'Thé Glacé',''),(19,5,'Jus',''),(20,6,'Pizza',''),(21,3,'Riz blanc',''),(22,6,'Peperoni',''),(23,6,'Toute garnie',''),(24,7,'Bien cuit',''),(25,7,'Medium',''),(26,7,'Saignant',''),(27,8,'Pas de glace','');
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
INSERT INTO `restaurants` VALUES (1,'GestResto','GestResto street','1231231234','1231231233','Montréal','A1A1A1','2014-11-26 10:00:05');
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

LOCK TABLES `tables` WRITE;
/*!40000 ALTER TABLE `tables` DISABLE KEYS */;
INSERT INTO `tables` VALUES (1,'VIP1',1,1),(2,'VIP2',1,0),(3,'10',1,0),(4,'11',1,1),(5,'12',1,1),(6,'20',1,1),(7,'21',1,0),(8,'22',1,0),(9,'30',1,0),(10,'31',1,0),(11,'32',1,0),(12,'40',1,0),(13,'Terrasse1',1,0),(14,'Terrasse2',1,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tablescommandes`
--

LOCK TABLES `tablescommandes` WRITE;
/*!40000 ALTER TABLE `tablescommandes` DISABLE KEYS */;
INSERT INTO `tablescommandes` VALUES (1,1,1),(2,2,1),(3,12,2),(4,11,2),(5,1,3),(6,6,4),(7,5,4),(8,4,4),(9,13,5),(10,14,5);
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

-- Dump completed on 2014-11-26 10:17:03
