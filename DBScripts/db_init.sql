-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.40 - MySQL Community Server (GPL)
-- Server OS:                    Linux
-- HeidiSQL Version:             12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for EthBlocks
CREATE DATABASE IF NOT EXISTS `EthBlocks` /*!40100 DEFAULT CHARACTER SET ascii */;
USE `EthBlocks`;

-- Dumping structure for table EthBlocks.Blocks
CREATE TABLE IF NOT EXISTS `Blocks` (
  `BlockId` int(20) NOT NULL AUTO_INCREMENT,
  `BlockNumber` int(20) NOT NULL,
  `Hash` varchar(66) NOT NULL DEFAULT '',
  `ParentHash` varchar(66) NOT NULL DEFAULT '',
  `Miner` varchar(42) NOT NULL DEFAULT '',
  `BlockReward` decimal(50,0) NOT NULL DEFAULT '0',
  `GasLimit` decimal(50,0) NOT NULL DEFAULT '0',
  `GasUsed` decimal(50,0) NOT NULL DEFAULT '0',
  PRIMARY KEY (`BlockId`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

-- Data exporting was unselected.

-- Dumping structure for table EthBlocks.Transactions
CREATE TABLE IF NOT EXISTS `Transactions` (
  `TransactionId` int(20) NOT NULL AUTO_INCREMENT,
  `BlockId` int(20) NOT NULL,
  `Hash` varchar(66) NOT NULL,
  `From` varchar(42) NOT NULL,
  `To` varchar(42) NOT NULL,
  `Value` decimal(50,0) NOT NULL DEFAULT '0',
  `Gas` decimal(50,0) NOT NULL DEFAULT '0',
  `GasPrice` decimal(50,0) NOT NULL DEFAULT '0',
  `TransactionIndex` int(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TransactionId`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
