-- --------------------------------------------------------
-- Host:                         50.57.219.58
-- Server version:               5.1.61-log - Percona Server (GPL), 13.2, Revision 2
-- Server OS:                    unknown-linux-gnu
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2014-03-29 21:00:44
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for 692531_devsoc
DROP DATABASE IF EXISTS `692531_devsoc`;
CREATE DATABASE IF NOT EXISTS `692531_devsoc` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `692531_devsoc`;


-- Dumping structure for table 692531_devsoc.Admin
DROP TABLE IF EXISTS `Admin`;
CREATE TABLE IF NOT EXISTS `Admin` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `Image` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TimeZone` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
INSERT INTO `Admin` (`Id`, `UserName`, `Password`, `Image`, `TimeZone`, `FirstName`, `LastName`) VALUES
	(_binary 0x30783143433234354131373143463438, 'admin', 'admin', '../Contents/img/user_img/300x70.png', '(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi', 'fname', 'lname');
-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Ads
DROP TABLE IF EXISTS `Ads`;
CREATE TABLE IF NOT EXISTS `Ads` (
  `Id` binary(16) DEFAULT NULL,
  `ImageUrl` varchar(750) DEFAULT NULL,
  `Advertisment` varchar(750) DEFAULT NULL,
  `Script` text,
  `EntryDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.ArchiveMessage
DROP TABLE IF EXISTS `ArchiveMessage`;
CREATE TABLE IF NOT EXISTS `ArchiveMessage` (
  `Id` binary(16) NOT NULL,
  `SocialGroup` varchar(50) DEFAULT '0',
  `ImgUrl` varchar(500) DEFAULT '0',
  `ProfileId` varchar(500) DEFAULT '0',
  `UserId` binary(16) DEFAULT '0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `UserName` varchar(50) DEFAULT '0',
  `MessageId` varchar(50) DEFAULT '0',
  `Message` text,
  `CreatedDateTime` varchar(50) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Blog_Comments
DROP TABLE IF EXISTS `Blog_Comments`;
CREATE TABLE IF NOT EXISTS `Blog_Comments` (
  `Id` binary(16) NOT NULL,
  `CommentPostId` binary(16) NOT NULL,
  `CommentAuthor` varchar(200) DEFAULT NULL,
  `CommentAuthorEmail` varchar(200) DEFAULT NULL,
  `CommentAuthorUrl` varchar(200) DEFAULT NULL,
  `CommentAuthorIp` varchar(200) DEFAULT NULL,
  `CommentDate` datetime NOT NULL,
  `CommentContent` text,
  `CommentApproved` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Blog_Posts
DROP TABLE IF EXISTS `Blog_Posts`;
CREATE TABLE IF NOT EXISTS `Blog_Posts` (
  `Id` binary(16) NOT NULL,
  `PostAuthor` binary(16) NOT NULL,
  `PostDate` datetime DEFAULT NULL,
  `PostContent` text,
  `PostTitle` text,
  `PostStatus` varchar(20) DEFAULT NULL,
  `CommentStatus` varchar(20) DEFAULT NULL,
  `PostName` varchar(200) DEFAULT NULL,
  `PostModifiedDate` datetime NOT NULL,
  `CommentCount` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Coupon
DROP TABLE IF EXISTS `Coupon`;
CREATE TABLE IF NOT EXISTS `Coupon` (
  `Id` binary(16) NOT NULL,
  `CouponCode` varchar(350) NOT NULL,
  `EntryCouponDate` datetime DEFAULT NULL,
  `ExpCouponDate` datetime DEFAULT NULL,
  `Status` varchar(10) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.DiscoverySearch
DROP TABLE IF EXISTS `DiscoverySearch`;
CREATE TABLE IF NOT EXISTS `DiscoverySearch` (
  `Id` binary(16) NOT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Message` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Network` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileImageUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `SearchKeyword` varchar(3502) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Drafts
DROP TABLE IF EXISTS `Drafts`;
CREATE TABLE IF NOT EXISTS `Drafts` (
  `Id` binary(16) NOT NULL,
  `Message` varchar(140) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookAccount
DROP TABLE IF EXISTS `FacebookAccount`;
CREATE TABLE IF NOT EXISTS `FacebookAccount` (
  `Id` binary(16) NOT NULL,
  `FbUserId` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `FbUserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccessToken` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Friends` int(11) DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookFeed
DROP TABLE IF EXISTS `FacebookFeed`;
CREATE TABLE IF NOT EXISTS `FacebookFeed` (
  `Id` binary(16) NOT NULL,
  `FeedDescription` text COLLATE utf8_unicode_ci,
  `FeedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbComment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbLike` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReadStatus` int(11) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserId` (`UserId`,`FeedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookGroup
DROP TABLE IF EXISTS `FacebookGroup`;
CREATE TABLE IF NOT EXISTS `FacebookGroup` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) DEFAULT NULL,
  `GroupId` varchar(350) DEFAULT NULL,
  `Icon` varchar(350) DEFAULT NULL,
  `Cover` varchar(350) DEFAULT NULL,
  `Owner` varchar(350) DEFAULT NULL,
  `Name` varchar(350) DEFAULT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `Link` varchar(350) DEFAULT NULL,
  `Privacy` varchar(350) DEFAULT NULL,
  `UpdatedTime` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookInsightPostStats
DROP TABLE IF EXISTS `FacebookInsightPostStats`;
CREATE TABLE IF NOT EXISTS `FacebookInsightPostStats` (
  `Id` binary(16) NOT NULL,
  `PageId` varchar(300) NOT NULL,
  `PostId` varchar(300) NOT NULL,
  `PostMessage` text NOT NULL,
  `PostLikes` int(11) DEFAULT NULL,
  `PostComments` int(11) DEFAULT NULL,
  `PostShares` int(11) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `EntryDate` datetime NOT NULL,
  `PostDate` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookInsightStats
DROP TABLE IF EXISTS `FacebookInsightStats`;
CREATE TABLE IF NOT EXISTS `FacebookInsightStats` (
  `Id` binary(16) NOT NULL DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `FbUserId` varchar(350) DEFAULT NULL COMMENT 'Facebook Profile Id',
  `UserId` binary(16) DEFAULT NULL COMMENT 'Registered User',
  `Gender` varchar(50) DEFAULT NULL COMMENT 'Registered User',
  `AgeDiff` varchar(20) DEFAULT NULL COMMENT 'Age Difference of the User of the Fan Page',
  `Location` varchar(50) DEFAULT NULL COMMENT 'Location of the User of the Fan Page',
  `PeopleCount` int(11) DEFAULT NULL COMMENT 'Likes count of the Fan Page',
  `StoriesCount` int(11) DEFAULT NULL COMMENT 'Likes count of the Fan Page',
  `EntryDate` datetime DEFAULT NULL COMMENT 'Entry Date',
  `PageImpressionCount` int(11) DEFAULT NULL COMMENT 'Page Impression',
  `CountDate` varchar(50) DEFAULT NULL COMMENT 'Entry Date',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookMessage
DROP TABLE IF EXISTS `FacebookMessage`;
CREATE TABLE IF NOT EXISTS `FacebookMessage` (
  `Id` binary(16) NOT NULL,
  `Message` text COLLATE utf8_unicode_ci,
  `MessageDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbComment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbLike` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Picture` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId` (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.FacebookStats
DROP TABLE IF EXISTS `FacebookStats`;
CREATE TABLE IF NOT EXISTS `FacebookStats` (
  `Id` binary(16) NOT NULL DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `FbUserId` varchar(350) DEFAULT NULL COMMENT 'facebook profile Id',
  `UserId` binary(16) DEFAULT NULL COMMENT 'registered user of the Site',
  `MaleCount` int(11) DEFAULT NULL COMMENT 'number of males',
  `FemaleCount` int(11) DEFAULT NULL COMMENT 'number of females',
  `ReachCount` int(11) DEFAULT NULL,
  `PeopleTalkingCount` int(11) DEFAULT NULL COMMENT 'number of People talking about page',
  `LikeCount` int(11) DEFAULT NULL COMMENT 'number of Likes count of the Fan Page',
  `CommentCount` int(11) DEFAULT NULL COMMENT 'number of comment Count on Fan Page',
  `ShareCount` int(11) DEFAULT NULL COMMENT 'number of share on fan Page ',
  `FanCount` int(11) DEFAULT NULL COMMENT 'number of fans of fan Page',
  `EntryDate` datetime DEFAULT NULL COMMENT 'Entry Date',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.GoogleAnalyticsAccount
DROP TABLE IF EXISTS `GoogleAnalyticsAccount`;
CREATE TABLE IF NOT EXISTS `GoogleAnalyticsAccount` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `EmailId` varchar(350) DEFAULT NULL,
  `GaAccountId` varchar(50) NOT NULL,
  `GaAccountName` varchar(600) DEFAULT NULL,
  `AccessToken` varchar(550) NOT NULL,
  `RefreshToken` varchar(550) NOT NULL,
  `GaProfileId` varchar(50) NOT NULL,
  `GaProfileName` varchar(350) NOT NULL,
  `Visits` int(11) NOT NULL,
  `AvgVisits` double NOT NULL,
  `NewVisits` int(11) NOT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `GaAccountId_GaProfileId` (`GaAccountId`,`GaProfileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.GoogleAnalyticsStats
DROP TABLE IF EXISTS `GoogleAnalyticsStats`;
CREATE TABLE IF NOT EXISTS `GoogleAnalyticsStats` (
  `Id` binary(16) NOT NULL DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `GaAccountId` varchar(50) DEFAULT NULL,
  `GaProfileId` varchar(50) DEFAULT NULL,
  `gaDate` varchar(50) DEFAULT NULL,
  `gaMonth` varchar(50) DEFAULT NULL,
  `gaYear` varchar(50) DEFAULT NULL,
  `gaCountry` varchar(50) DEFAULT NULL,
  `gaRegion` varchar(50) DEFAULT NULL,
  `gaVisits` varchar(50) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.GooglePlusAccount
DROP TABLE IF EXISTS `GooglePlusAccount`;
CREATE TABLE IF NOT EXISTS `GooglePlusAccount` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL COMMENT 'Id of Registered User',
  `GpUserId` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Google Plus User Id',
  `GpUserName` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Google Plus User Name',
  `GpProfileImage` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Profile Image',
  `PeopleCount` int(11) DEFAULT NULL COMMENT 'Profile Image',
  `AccessToken` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `RefreshToken` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL COMMENT 'Entry Date of Record',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserId_GpUserId` (`UserId`,`GpUserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.GooglePlusActivities
DROP TABLE IF EXISTS `GooglePlusActivities`;
CREATE TABLE IF NOT EXISTS `GooglePlusActivities` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ActivityId` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `GpUserId` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `Title` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `ActivityUrl` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `FromId` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `FromUserName` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `FromProfileImage` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `Content` varchar(800) CHARACTER SET latin1 DEFAULT NULL,
  `RepliesCount` int(11) DEFAULT NULL,
  `PlusonersCount` int(11) DEFAULT NULL,
  `ResharersCount` int(11) DEFAULT NULL,
  `PublishedDate` varchar(80) CHARACTER SET latin1 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.GroupProfile
DROP TABLE IF EXISTS `GroupProfile`;
CREATE TABLE IF NOT EXISTS `GroupProfile` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `GroupOwnerId` binary(16) DEFAULT NULL COMMENT 'This is UserId',
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Groups
DROP TABLE IF EXISTS `Groups`;
CREATE TABLE IF NOT EXISTS `Groups` (
  `Id` binary(16) NOT NULL,
  `GroupName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.InstagramAccount
DROP TABLE IF EXISTS `InstagramAccount`;
CREATE TABLE IF NOT EXISTS `InstagramAccount` (
  `Id` binary(16) NOT NULL,
  `InstagramId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccessToken` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InsUserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Followers` int(11) DEFAULT NULL,
  `FollowedBy` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `TotalImages` int(11) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.InstagramComment
DROP TABLE IF EXISTS `InstagramComment`;
CREATE TABLE IF NOT EXISTS `InstagramComment` (
  `ID` binary(16) NOT NULL DEFAULT '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `FeedId` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `InstagramId` varchar(350) DEFAULT NULL,
  `Comment` varchar(500) DEFAULT NULL,
  `CommentDate` varchar(50) DEFAULT NULL,
  `CommentId` varchar(350) DEFAULT NULL,
  `FromName` varchar(350) DEFAULT NULL,
  `FromProfilePic` varchar(350) DEFAULT NULL,
  `EntryDate` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.InstagramFeed
DROP TABLE IF EXISTS `InstagramFeed`;
CREATE TABLE IF NOT EXISTS `InstagramFeed` (
  `Id` binary(16) NOT NULL,
  `FeedId` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `InstagramId` varchar(350) DEFAULT NULL,
  `FeedImageUrl` varchar(500) DEFAULT NULL,
  `LikeCount` int(11) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `FeedDate` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Invitation
DROP TABLE IF EXISTS `Invitation`;
CREATE TABLE IF NOT EXISTS `Invitation` (
  `Id` binary(16) NOT NULL,
  `InvitationBody` longtext NOT NULL,
  `Subject` varchar(350) NOT NULL,
  `SenderName` varchar(350) DEFAULT NULL,
  `FriendName` varchar(350) DEFAULT NULL,
  `SenderEmail` varchar(350) DEFAULT NULL,
  `FriendEmail` varchar(350) DEFAULT NULL,
  `Status` varchar(5) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `SenderEmail_FriendEmail` (`SenderEmail`,`FriendEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.LinkedInAccount
DROP TABLE IF EXISTS `LinkedInAccount`;
CREATE TABLE IF NOT EXISTS `LinkedInAccount` (
  `Id` binary(16) NOT NULL,
  `LinkedinUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LinkedinUserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthToken` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthSecret` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `OAuthVerifier` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Connections` int(10) DEFAULT NULL,
  `ProfileImageUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.LinkedInFeed
DROP TABLE IF EXISTS `LinkedInFeed`;
CREATE TABLE IF NOT EXISTS `LinkedInFeed` (
  `Id` binary(16) NOT NULL,
  `Feeds` text COLLATE utf8_unicode_ci,
  `FeedsDate` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromPicUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.LinkedInMessage
DROP TABLE IF EXISTS `LinkedInMessage`;
CREATE TABLE IF NOT EXISTS `LinkedInMessage` (
  `Id` binary(16) NOT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `CreatedDate` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Message` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Log
DROP TABLE IF EXISTS `Log`;
CREATE TABLE IF NOT EXISTS `Log` (
  `Id` binary(16) DEFAULT NULL,
  `ModuleName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Exception` text COLLATE utf8_unicode_ci,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.LoginLogs
DROP TABLE IF EXISTS `LoginLogs`;
CREATE TABLE IF NOT EXISTS `LoginLogs` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.News
DROP TABLE IF EXISTS `News`;
CREATE TABLE IF NOT EXISTS `News` (
  `Id` binary(16) DEFAULT NULL,
  `NewsDetail` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.NewsLetter
DROP TABLE IF EXISTS `NewsLetter`;
CREATE TABLE IF NOT EXISTS `NewsLetter` (
  `Id` binary(16) NOT NULL,
  `NewsLetterBody` longtext NOT NULL,
  `Subject` varchar(250) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `SendStatus` tinyint(1) DEFAULT '0' COMMENT 'If Email Sent then Status is 1 else 0',
  `SendDate` datetime NOT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Package
DROP TABLE IF EXISTS `Package`;
CREATE TABLE IF NOT EXISTS `Package` (
  `Id` binary(16) NOT NULL,
  `PackageName` varchar(100) DEFAULT NULL,
  `Pricing` double DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `TotalProfiles` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `Package` (`Id`, `PackageName`, `Pricing`, `EntryDate`, `Status`, `TotalProfiles`) VALUES
	(_binary 0x01498C6815A7D34DA75044C770EFACD5, 'Standard', 29, '2013-12-18 12:40:08', 1, 2),
	(_binary 0xC82C363EE615BB448C9B120E70C1A4A2, 'Free', 0, '2013-12-18 11:37:32', 1, 2),
	(_binary 0xF63AC93B241CAB41A316B44A61A6756A, 'Deluxe', 89, '2013-12-18 13:00:37', 1, 2),
	(_binary 0xFDD6DAABE26F8845A6F4C93C2CF9F067, 'Premium', 49, '2013-12-18 12:40:04', 1, 50);



-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.PaymentTransaction
DROP TABLE IF EXISTS `PaymentTransaction`;
CREATE TABLE IF NOT EXISTS `PaymentTransaction` (
  `Id` binary(16) NOT NULL,
  `PayPalTransactionId` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AmountPaid` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `PaymentDate` datetime DEFAULT NULL,
  `PaymentStatus` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PayerId` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReceiverId` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PayerEmail` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PaypalPaymentDate` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IPNTrackId` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `VersionType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DetailsInfo` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.ReplyMessage
DROP TABLE IF EXISTS `ReplyMessage`;
CREATE TABLE IF NOT EXISTS `ReplyMessage` (
  `Id` binary(16) NOT NULL,
  `FromUserId` varchar(50) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `MessageId` binary(16) NOT NULL,
  `Message` varchar(200) NOT NULL,
  `Type` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.RssFeeds
DROP TABLE IF EXISTS `RssFeeds`;
CREATE TABLE IF NOT EXISTS `RssFeeds` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Message` varchar(900) DEFAULT NULL,
  `Duration` varchar(350) DEFAULT NULL,
  `ProfileScreenName` varchar(350) DEFAULT NULL,
  `FeedUrl` varchar(350) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '0 means running, 1 means not running',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.RssReader
DROP TABLE IF EXISTS `RssReader`;
CREATE TABLE IF NOT EXISTS `RssReader` (
  `Id` binary(16) NOT NULL,
  `Title` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  `CreatedDate` datetime DEFAULT NULL,
  `FeedsUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PublishedDate` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '0 - running and 1 means not',
  `Link` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.ScheduledMessage
DROP TABLE IF EXISTS `ScheduledMessage`;
CREATE TABLE IF NOT EXISTS `ScheduledMessage` (
  `Id` binary(16) NOT NULL,
  `ShareMessage` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientTime` datetime DEFAULT NULL,
  `ScheduleTime` datetime DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT 'by default 0 and after success 1',
  `UserId` binary(16) DEFAULT NULL,
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PicUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.SearchKeyword
DROP TABLE IF EXISTS `SearchKeyword`;
CREATE TABLE IF NOT EXISTS `SearchKeyword` (
  `Id` binary(32) NOT NULL,
  `UserId` binary(32) DEFAULT NULL,
  `KeyWord` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.SocialProfile
DROP TABLE IF EXISTS `SocialProfile`;
CREATE TABLE IF NOT EXISTS `SocialProfile` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileDate` datetime DEFAULT NULL,
  `ProfileStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TaskComment
DROP TABLE IF EXISTS `TaskComment`;
CREATE TABLE IF NOT EXISTS `TaskComment` (
  `Id` binary(16) DEFAULT NULL,
  `Comment` text CHARACTER SET latin1,
  `UserId` binary(16) DEFAULT NULL,
  `TaskId` binary(16) DEFAULT NULL,
  `CommentDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Tasks
DROP TABLE IF EXISTS `Tasks`;
CREATE TABLE IF NOT EXISTS `Tasks` (
  `Id` binary(16) NOT NULL,
  `TaskMessage` text COLLATE utf8_unicode_ci,
  `UserId` binary(16) DEFAULT NULL,
  `AssignTaskTo` binary(16) DEFAULT NULL,
  `TaskStatus` tinyint(1) DEFAULT NULL COMMENT '0 means incomplete',
  `AssignDate` datetime DEFAULT NULL,
  `CompletionDate` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.Team
DROP TABLE IF EXISTS `Team`;
CREATE TABLE IF NOT EXISTS `Team` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `FirstName` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `LastName` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `InviteDate` datetime DEFAULT NULL,
  `StatusUpdateDate` datetime DEFAULT NULL,
  `InviteStatus` tinyint(1) DEFAULT NULL COMMENT '1=invited,2=accepted',
  `AccessLevel` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TeamMemberProfile
DROP TABLE IF EXISTS `TeamMemberProfile`;
CREATE TABLE IF NOT EXISTS `TeamMemberProfile` (
  `Id` binary(16) NOT NULL,
  `TeamId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '1 = invited,2=accepted',
  `StatusUpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TwitterAccount
DROP TABLE IF EXISTS `TwitterAccount`;
CREATE TABLE IF NOT EXISTS `TwitterAccount` (
  `Id` binary(16) NOT NULL,
  `TwitterUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TwitterScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthToken` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthSecret` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `FollowersCount` int(11) DEFAULT NULL,
  `FollowingCount` int(11) DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileImageUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TwitterName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TwitterDirectMessages
DROP TABLE IF EXISTS `TwitterDirectMessages`;
CREATE TABLE IF NOT EXISTS `TwitterDirectMessages` (
  `Id` binary(16) NOT NULL,
  `RecipientId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecipientProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Message` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecipientScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TwitterFeed
DROP TABLE IF EXISTS `TwitterFeed`;
CREATE TABLE IF NOT EXISTS `TwitterFeed` (
  `Id` binary(16) NOT NULL,
  `TwitterFeed` text COLLATE utf8_unicode_ci,
  `FeedDate` datetime DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SourceUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TwitterMessage
DROP TABLE IF EXISTS `TwitterMessage`;
CREATE TABLE IF NOT EXISTS `TwitterMessage` (
  `Id` binary(16) NOT NULL,
  `TwitterMessage` text COLLATE utf8_unicode_ci,
  `MessageDate` datetime DEFAULT NULL,
  `MessageId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SourceUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReadStatus` int(1) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId` (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.TwitterStats
DROP TABLE IF EXISTS `TwitterStats`;
CREATE TABLE IF NOT EXISTS `TwitterStats` (
  `ID` binary(16) DEFAULT NULL,
  `TwitterId` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `FollowingCount` int(11) DEFAULT NULL,
  `FollowerCount` int(11) DEFAULT NULL,
  `DMRecievedCount` int(11) DEFAULT NULL,
  `DMSentCount` int(11) DEFAULT NULL,
  `Engagement` double DEFAULT NULL,
  `Influence` double DEFAULT NULL,
  `Age1820` int(11) DEFAULT NULL,
  `Age2124` int(11) DEFAULT NULL,
  `Age2534` int(11) DEFAULT NULL,
  `Age3544` int(11) DEFAULT NULL,
  `Age4554` int(11) DEFAULT NULL,
  `Age5564` int(11) DEFAULT NULL,
  `Age65` int(11) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.User
DROP TABLE IF EXISTS `User`;
CREATE TABLE IF NOT EXISTS `User` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccountType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `UserStatus` tinyint(1) DEFAULT NULL COMMENT '0 = InActive,1 = Active|| unpaid,2=Active||paid',
  `Password` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TimeZone` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PaymentStatus` varchar(6) COLLATE utf8_unicode_ci DEFAULT 'unpaid' COMMENT 'default value = unpaid',
  `ActivationStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0' COMMENT '0 = InActive,1 = Active',
  `CouponCode` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReferenceStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0',
  `RefereeStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0',
  `UserType` varchar(350) COLLATE utf8_unicode_ci DEFAULT 'user' COMMENT 'default value=user',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.UserActivation
DROP TABLE IF EXISTS `UserActivation`;
CREATE TABLE IF NOT EXISTS `UserActivation` (
  `ID` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `ActivationStatus` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.UserPackageRelation
DROP TABLE IF EXISTS `UserPackageRelation`;
CREATE TABLE IF NOT EXISTS `UserPackageRelation` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PackageId` binary(16) NOT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `PackageStatus` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table 692531_devsoc.UserRefRelation
DROP TABLE IF EXISTS `UserRefRelation`;
CREATE TABLE IF NOT EXISTS `UserRefRelation` (
  `Id` binary(16) NOT NULL,
  `ReferenceUserId` binary(16) NOT NULL,
  `RefereeUserId` binary(16) NOT NULL,
  `ReferenceUserEmail` varchar(300) DEFAULT NULL,
  `RefereeUserEmail` varchar(300) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Status` varchar(5) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
