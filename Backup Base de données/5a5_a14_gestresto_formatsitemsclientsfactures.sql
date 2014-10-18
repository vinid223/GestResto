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
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

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

/*!40000 ALTER TABLE `formatsitemsclientsfactures` DISABLE KEYS */;
/*!40000 ALTER TABLE `formatsitemsclientsfactures` ENABLE KEYS */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed
