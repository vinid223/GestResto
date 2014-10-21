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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-10-21 10:26:55
