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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-10-21 10:26:55
