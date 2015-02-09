-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.6.19 - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             7.0.0.4390
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for abhay4
DROP DATABASE IF EXISTS `abhay4`;
CREATE DATABASE IF NOT EXISTS `abhay4` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `abhay4`;


-- Dumping structure for table abhay4.package
DROP TABLE IF EXISTS `package`;
CREATE TABLE IF NOT EXISTS `package` (
  `Id` binary(16) NOT NULL,
  `PackageName` varchar(100) DEFAULT NULL,
  `Pricing` double DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `TotalProfiles` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table abhay4.package: ~8 rows (approximately)
/*!40000 ALTER TABLE `package` DISABLE KEYS */;
INSERT INTO `package` (`Id`, `PackageName`, `Pricing`, `EntryDate`, `Status`, `TotalProfiles`) VALUES
	(_binary 0x01498C6815A7D34DA75044C770EFACD5, 'Standard', 4.99, '2013-12-18 12:40:08', 1, 2),
	(_binary 0x31ADC0750661BE45BED1521291EC7203, 'SocioBasic', 29.99, '2013-12-18 12:40:08', 1, 0),
	(_binary 0x96DF5CA3AB2BFE4D8E245A44095C1FC5, 'SocioPremium', 79.99, '2013-12-18 13:00:37', 1, 0),
	(_binary 0x9BF4785DB60B18449AAE0D107F27956E, 'SocioDeluxe', 99.99, '2013-12-18 12:40:04', 1, 0),
	(_binary 0xA2AC0E869B153E4D8089E9DDCAD4D146, 'SocioStandard', 49.99, '2013-12-18 11:37:32', 1, 0),
	(_binary 0xC82C363EE615BB448C9B120E70C1A4A2, 'Free', 0, '2013-12-18 11:37:32', 1, 2),
	(_binary 0xF63AC93B241CAB41A316B44A61A6756A, 'Deluxe', 19.99, '2013-12-18 13:00:37', 1, 2),
	(_binary 0xFDD6DAABE26F8845A6F4C93C2CF9F067, 'Premium', 9.99, '2013-12-18 12:40:04', 1, 50);
/*!40000 ALTER TABLE `package` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
