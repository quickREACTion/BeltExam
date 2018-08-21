CREATE DATABASE  IF NOT EXISTS `beltexamtwo` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `beltexamtwo`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: beltexamtwo
-- ------------------------------------------------------
-- Server version	8.0.11

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
-- Table structure for table `activities`
--

DROP TABLE IF EXISTS `activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activities` (
  `activitiesId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Time` time DEFAULT NULL,
  `Duration` time DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `usersId` int(11) NOT NULL,
  PRIMARY KEY (`activitiesId`),
  KEY `fk_activities_users1_idx` (`usersId`),
  CONSTRAINT `fk_activities_users1` FOREIGN KEY (`usersId`) REFERENCES `users` (`usersid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activities`
--

LOCK TABLES `activities` WRITE;
/*!40000 ALTER TABLE `activities` DISABLE KEYS */;
INSERT INTO `activities` VALUES (1,'Squirt Gun Fight','2018-08-28 00:00:00','12:00:00','14:00:00','Like shooting? Need to get drenched in water? This is the event for you! Come join us for the city wide squirt gun fight!','2018-08-21 10:29:44','2018-08-21 10:29:44',1),(2,'Soccer Event!','2018-09-01 00:00:00','13:00:00','13:00:00','Fun in the sun soccer event!','2018-08-21 10:30:31','2018-08-21 10:30:31',2);
/*!40000 ALTER TABLE `activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `participants`
--

DROP TABLE IF EXISTS `participants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `participants` (
  `participantsId` int(11) NOT NULL AUTO_INCREMENT,
  `usersId` int(11) NOT NULL,
  `activitiesId` int(11) NOT NULL,
  PRIMARY KEY (`participantsId`),
  KEY `fk_users_has_activities_activities1_idx` (`activitiesId`),
  KEY `fk_users_has_activities_users_idx` (`usersId`),
  CONSTRAINT `fk_users_has_activities_activities1` FOREIGN KEY (`activitiesId`) REFERENCES `activities` (`activitiesid`) ON DELETE CASCADE,
  CONSTRAINT `fk_users_has_activities_users` FOREIGN KEY (`usersId`) REFERENCES `users` (`usersid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `participants`
--

LOCK TABLES `participants` WRITE;
/*!40000 ALTER TABLE `participants` DISABLE KEYS */;
INSERT INTO `participants` VALUES (3,2,1);
/*!40000 ALTER TABLE `participants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `usersId` int(11) NOT NULL AUTO_INCREMENT,
  `First_Name` varchar(255) DEFAULT NULL,
  `Last_Name` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`usersId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Chris','Herrick','tester@tester1.com','AQAAAAEAACcQAAAAEHcl/MqbkbLxtFbuar29Z3JZWJiNvn5u6jPNw9I3I6aB75UdRxibi5g/JUeE5tZlnA==','2018-08-21 10:28:50','2018-08-21 10:28:50'),(2,'Liberty','Freedom','tester@tester2.com','AQAAAAEAACcQAAAAEOWSNRTpMP7jd3MZjkc0id+Aj0EKktDOXiOz5Sc1W8lLBo0toKqoboE9rR2lLTqWOQ==','2018-08-21 10:30:05','2018-08-21 10:30:05');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-08-21 11:03:56
