-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.1.10-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeidiSQL Version:             9.1.0.4867
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for devsocioboard
CREATE DATABASE IF NOT EXISTS `devsocioboard` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `devsocioboard`;


-- Dumping structure for table devsocioboard.admin
CREATE TABLE IF NOT EXISTS `admin` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(350) CHARACTER SET utf8 NOT NULL,
  `Password` varchar(350) CHARACTER SET utf8 NOT NULL,
  `Image` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `TimeZone` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FirstName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.ads
CREATE TABLE IF NOT EXISTS `ads` (
  `Id` binary(16) DEFAULT NULL,
  `ImageUrl` varchar(750) DEFAULT NULL,
  `Advertisment` varchar(750) DEFAULT NULL,
  `Script` text,
  `EntryDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.affiliates
CREATE TABLE IF NOT EXISTS `affiliates` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `FriendUserId` binary(16) NOT NULL,
  `AffiliateDate` datetime NOT NULL,
  `Amount` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.archivemessage
CREATE TABLE IF NOT EXISTS `archivemessage` (
  `Id` binary(16) NOT NULL,
  `SocialGroup` varchar(50) DEFAULT '0',
  `ImgUrl` varchar(500) DEFAULT '0',
  `ProfileId` varchar(500) DEFAULT '0',
  `UserId` binary(16) DEFAULT '0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `UserName` varchar(50) DEFAULT '0',
  `MessageId` varchar(50) DEFAULT '0',
  `Message` text CHARACTER SET utf8,
  `CreatedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.blog_comments
CREATE TABLE IF NOT EXISTS `blog_comments` (
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


-- Dumping structure for table devsocioboard.blog_posts
CREATE TABLE IF NOT EXISTS `blog_posts` (
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


-- Dumping structure for table devsocioboard.businesssetting
CREATE TABLE IF NOT EXISTS `businesssetting` (
  `Id` binary(16) NOT NULL,
  `BusinessName` varchar(350) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `AssigningTasks` tinyint(1) DEFAULT NULL COMMENT '0 means Disable',
  `TaskNotification` tinyint(1) DEFAULT NULL COMMENT '0 means Disable',
  `FbPhotoUpload` int(11) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.companyprofiles
CREATE TABLE IF NOT EXISTS `companyprofiles` (
  `CompanyName` varchar(500) NOT NULL,
  `InstagramProfileId` varchar(500) DEFAULT NULL,
  `FbProfileId` varchar(500) DEFAULT NULL,
  `TwitterProfileId` varchar(500) DEFAULT NULL,
  `LinkedinProfileId` varchar(500) DEFAULT NULL,
  `YoutubeProfileId` varchar(500) DEFAULT NULL,
  `GPlusProfileId` varchar(500) DEFAULT NULL,
  `TumblrProfileId` varchar(500) DEFAULT NULL,
  `UserId` varchar(500) DEFAULT NULL,
  `Id` binary(16) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.coupon
CREATE TABLE IF NOT EXISTS `coupon` (
  `Id` binary(16) NOT NULL,
  `CouponCode` varchar(350) NOT NULL,
  `EntryCouponDate` datetime DEFAULT NULL,
  `ExpCouponDate` datetime DEFAULT NULL,
  `Status` varchar(10) DEFAULT '0',
  `Discount` int(11) DEFAULT '0' COMMENT 'range is 0 - 99',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.demorequest
CREATE TABLE IF NOT EXISTS `demorequest` (
  `Id` binary(16) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `Company` varchar(500) DEFAULT NULL,
  `Skype` varchar(50) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Message` varchar(5000) DEFAULT NULL,
  `DemoPlanType` int(11) NOT NULL,
  `Designation` varchar(50) DEFAULT NULL,
  `Location` varchar(50) DEFAULT NULL,
  `CompanyWebsite` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.discoveryleads
CREATE TABLE IF NOT EXISTS `discoveryleads` (
  `Id` binary(16) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Keyword` varchar(3502) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.discoverysearch
CREATE TABLE IF NOT EXISTS `discoverysearch` (
  `Id` binary(16) NOT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Message` text CHARACTER SET utf8,
  `CreatedTime` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Network` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileImageUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `MessageId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `SearchKeyword` varchar(3502) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.drafts
CREATE TABLE IF NOT EXISTS `drafts` (
  `Id` binary(16) NOT NULL,
  `Message` tinytext,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `GroupId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.dropboxaccount
CREATE TABLE IF NOT EXISTS `dropboxaccount` (
  `Id` binary(16) NOT NULL,
  `UserId` varchar(50) DEFAULT NULL,
  `DropboxUserId` varchar(50) DEFAULT NULL,
  `DropboxUserName` varchar(50) DEFAULT NULL,
  `DropboxEmail` varchar(50) DEFAULT NULL,
  `OauthToken` varchar(500) DEFAULT NULL,
  `OauthTokenSecret` varchar(500) DEFAULT NULL,
  `AccessToken` varchar(500) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.ewalletwithdrawrequest
CREATE TABLE IF NOT EXISTS `ewalletwithdrawrequest` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `UserEmail` varchar(50) NOT NULL,
  `WithdrawAmount` varchar(50) NOT NULL,
  `PaymentMethod` varchar(50) NOT NULL,
  `PaypalEmail` varchar(50) NOT NULL,
  `IbanCode` varchar(50) NOT NULL,
  `SwiftCode` varchar(50) NOT NULL,
  `Other` varchar(50) NOT NULL,
  `Status` int(11) NOT NULL COMMENT 'Received=0, processing=1, paid=2',
  `RequestDate` datetime NOT NULL,
  `UserID` binary(16) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.facebookaccount
CREATE TABLE IF NOT EXISTS `facebookaccount` (
  `Id` binary(16) NOT NULL,
  `FbUserId` varchar(350) CHARACTER SET utf8 NOT NULL,
  `FbUserName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `AccessToken` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Friends` int(11) DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `LastScraped` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.facebookcredits
CREATE TABLE IF NOT EXISTS `facebookcredits` (
  `Id` binary(16) NOT NULL,
  `FacebookUserId` varchar(500) NOT NULL,
  `FacebookPassword` varchar(500) NOT NULL,
  `IsCreditsValid` bit(1) NOT NULL,
  `UserId` varchar(500) NOT NULL,
  `IsGroupScraped` bit(1) NOT NULL,
  `Proxyip` varchar(50) DEFAULT NULL,
  `Proxyport` varchar(50) DEFAULT NULL,
  `Proxyusername` varchar(50) DEFAULT NULL,
  `proxypassword` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.facebookfeed
CREATE TABLE IF NOT EXISTS `facebookfeed` (
  `Id` binary(16) NOT NULL,
  `FeedDescription` text CHARACTER SET utf8,
  `FeedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `FromId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Picture` varchar(350) COLLATE utf8_unicode_ci DEFAULT '0',
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FbComment` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FbLike` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FeedId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `ReadStatus` int(11) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserFeedUnique` (`UserId`,`FeedId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `FromId` (`FromId`),
  KEY `FeedId` (`FeedId`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.facebookgroup
CREATE TABLE IF NOT EXISTS `facebookgroup` (
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


-- Dumping structure for table devsocioboard.facebookinsightpoststats
CREATE TABLE IF NOT EXISTS `facebookinsightpoststats` (
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


-- Dumping structure for table devsocioboard.facebookinsightstats
CREATE TABLE IF NOT EXISTS `facebookinsightstats` (
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


-- Dumping structure for table devsocioboard.facebookpagepost
CREATE TABLE IF NOT EXISTS `facebookpagepost` (
  `Id` binary(16) NOT NULL,
  `PageId` varchar(50) DEFAULT NULL,
  `PageName` varchar(50) DEFAULT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `Message` text,
  `Picture` text,
  `Link` text,
  `Name` text,
  `Description` text,
  `Type` varchar(50) DEFAULT NULL,
  `Likes` varchar(10) DEFAULT NULL,
  `Comments` varchar(10) DEFAULT NULL,
  `Shares` varchar(10) DEFAULT NULL,
  `Reach` varchar(10) DEFAULT NULL,
  `Talking` varchar(10) DEFAULT NULL,
  `EngagedUsers` varchar(10) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.facebookstats
CREATE TABLE IF NOT EXISTS `facebookstats` (
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


-- Dumping structure for table devsocioboard.fbgroupdetails
CREATE TABLE IF NOT EXISTS `fbgroupdetails` (
  `Id` binary(16) NOT NULL,
  `GroupId` varchar(50) NOT NULL,
  `MemberCount` varchar(50) DEFAULT NULL,
  `GroupAdminId` varchar(50) NOT NULL,
  `GroupAdminName` varchar(50) DEFAULT NULL,
  `GroupType` varchar(50) DEFAULT NULL,
  `GroupName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `GroupId` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpagecomment
CREATE TABLE IF NOT EXISTS `fbpagecomment` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL,
  `CommentId` varchar(50) DEFAULT NULL,
  `Likes` int(5) DEFAULT NULL,
  `UserLikes` int(5) DEFAULT NULL,
  `PictureUrl` varchar(500) DEFAULT NULL,
  `Comment` text,
  `CommentDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpageliker
CREATE TABLE IF NOT EXISTS `fbpageliker` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpagepost
CREATE TABLE IF NOT EXISTS `fbpagepost` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `FromId` varchar(50) DEFAULT NULL,
  `PageId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `Type` varchar(100) DEFAULT NULL,
  `StatusType` varchar(100) DEFAULT NULL,
  `PictureUrl` varchar(500) DEFAULT NULL,
  `LinkUrl` varchar(500) DEFAULT NULL,
  `IconUrl` varchar(500) DEFAULT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `Post` text,
  `PostDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Likes` int(11) DEFAULT NULL,
  `Comments` int(11) DEFAULT NULL,
  `Shares` int(11) DEFAULT NULL,
  `UserLikes` int(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpagepostcommentliker
CREATE TABLE IF NOT EXISTS `fbpagepostcommentliker` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `CommentId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpagesharer
CREATE TABLE IF NOT EXISTS `fbpagesharer` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PostId` varchar(50) NOT NULL,
  `FromName` varchar(100) NOT NULL,
  `FromId` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.fbpublicpagereports
CREATE TABLE IF NOT EXISTS `fbpublicpagereports` (
  `Id` binary(16) NOT NULL,
  `Date` date NOT NULL,
  `PageId` varchar(50) NOT NULL,
  `LikesCount` float NOT NULL DEFAULT '0',
  `PostsCount` float NOT NULL DEFAULT '0',
  `CommentsCount` float NOT NULL DEFAULT '0',
  `SharesCount` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.feedsentimentalanalysis
CREATE TABLE IF NOT EXISTS `feedsentimentalanalysis` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `AssigneUserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Positive` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Negative` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Network` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `TicketNo` int(10) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Ticket No` (`TicketNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.googleanalyticsaccount
CREATE TABLE IF NOT EXISTS `googleanalyticsaccount` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `EmailId` varchar(350) DEFAULT NULL,
  `GaAccountId` varchar(50) DEFAULT NULL,
  `GaAccountName` varchar(250) DEFAULT NULL,
  `GaWebPropertyId` varchar(250) DEFAULT NULL,
  `GaProfileId` varchar(50) DEFAULT NULL,
  `GaProfileName` varchar(350) DEFAULT NULL,
  `WebsiteUrl` varchar(350) DEFAULT NULL,
  `RefreshToken` varchar(350) DEFAULT NULL,
  `AccessToken` varchar(550) DEFAULT NULL,
  `ProfilePicUrl` varchar(250) DEFAULT NULL,
  `Visits` double DEFAULT NULL,
  `Views` double DEFAULT NULL,
  `TwitterPosts` double DEFAULT NULL,
  `WebMentions` double DEFAULT NULL,
  `IsActive` tinyint(1) unsigned DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.googleanalyticsstats
CREATE TABLE IF NOT EXISTS `googleanalyticsstats` (
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


-- Dumping structure for table devsocioboard.googleplusaccount
CREATE TABLE IF NOT EXISTS `googleplusaccount` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL COMMENT 'Id of Registered User',
  `GpUserId` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Google Plus User Id',
  `GpUserName` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Google Plus User Name',
  `GpProfileImage` varchar(350) CHARACTER SET latin1 DEFAULT NULL COMMENT 'Profile Image',
  `InYourCircles` int(11) DEFAULT NULL COMMENT 'Profile Image',
  `AccessToken` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `RefreshToken` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET latin1 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL COMMENT 'Entry Date of Record',
  `HaveYouInCircles` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserId_GpUserId` (`UserId`,`GpUserId`(255))
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.googleplusactivities
CREATE TABLE IF NOT EXISTS `googleplusactivities` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ActivityId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `GpUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Title` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ActivityUrl` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromUserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileImage` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Content` text COLLATE utf8_unicode_ci,
  `Attachment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AttachmentType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RepliesCount` int(11) DEFAULT NULL,
  `PlusonersCount` int(11) DEFAULT NULL,
  `ResharersCount` int(11) DEFAULT NULL,
  `PublishedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Link` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ArticleContent` text COLLATE utf8_unicode_ci,
  `ArticleDisplayname` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.groupmembers
CREATE TABLE IF NOT EXISTS `groupmembers` (
  `Id` binary(16) NOT NULL,
  `UserId` varchar(50) DEFAULT NULL,
  `Status` int(11) NOT NULL,
  `EmailId` varchar(50) NOT NULL,
  `GroupId` varchar(50) NOT NULL,
  `IsAdmin` bit(1) NOT NULL,
  `Membercode` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.groupprofile
CREATE TABLE IF NOT EXISTS `groupprofile` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `GroupOwnerId` binary(16) DEFAULT NULL COMMENT 'This is UserId',
  `ProfileType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileName` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfilePic` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.groupreports
CREATE TABLE IF NOT EXISTS `groupreports` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) DEFAULT NULL,
  `inbox_15` bigint(20) DEFAULT NULL,
  `perday_inbox_15` varchar(300) DEFAULT NULL,
  `inbox_30` bigint(20) DEFAULT NULL,
  `perday_inbox_30` varchar(300) DEFAULT NULL,
  `inbox_60` bigint(20) DEFAULT NULL,
  `perday_inbox_60` varchar(300) DEFAULT NULL,
  `inbox_90` bigint(20) DEFAULT NULL,
  `perday_inbox_90` varchar(300) DEFAULT NULL,
  `sent_15` bigint(20) DEFAULT NULL,
  `perday_sent_15` varchar(300) DEFAULT NULL,
  `sent_30` bigint(20) DEFAULT NULL,
  `perday_sent_30` varchar(300) DEFAULT NULL,
  `sent_60` bigint(20) DEFAULT NULL,
  `perday_sent_60` varchar(300) DEFAULT NULL,
  `sent_90` bigint(20) DEFAULT NULL,
  `perday_sent_90` varchar(300) DEFAULT NULL,
  `twitterfollower_15` bigint(20) DEFAULT NULL,
  `perday_twitterfollower_15` varchar(300) DEFAULT NULL,
  `twitterfollower_30` bigint(20) DEFAULT NULL,
  `perday_twitterfollower_30` varchar(300) DEFAULT NULL,
  `twitterfollower_60` bigint(20) DEFAULT NULL,
  `perday_twitterfollower_60` varchar(300) DEFAULT NULL,
  `twitterfollower_90` bigint(20) DEFAULT NULL,
  `perday_twitterfollower_90` varchar(300) DEFAULT NULL,
  `fbfan_15` bigint(20) DEFAULT NULL,
  `perday_fbfan_15` varchar(300) DEFAULT NULL,
  `fbfan_30` bigint(20) DEFAULT NULL,
  `perday_fbfan_30` varchar(300) DEFAULT NULL,
  `fbfan_60` bigint(20) DEFAULT NULL,
  `perday_fbfan_60` varchar(300) DEFAULT NULL,
  `fbfan_90` bigint(20) DEFAULT NULL,
  `perday_fbfan_90` varchar(300) DEFAULT NULL,
  `interaction_15` bigint(20) DEFAULT NULL,
  `perday_interaction_15` varchar(300) DEFAULT NULL,
  `interaction_30` bigint(20) DEFAULT NULL,
  `perday_interaction_30` varchar(300) DEFAULT NULL,
  `interaction_60` bigint(20) DEFAULT NULL,
  `perday_interaction_60` varchar(300) DEFAULT NULL,
  `interaction_90` bigint(20) DEFAULT NULL,
  `perday_interaction_90` varchar(300) DEFAULT NULL,
  `twtmentions_15` bigint(20) DEFAULT NULL,
  `twtmentions_30` bigint(20) DEFAULT NULL,
  `twtmentions_60` bigint(20) DEFAULT NULL,
  `twtmentions_90` bigint(20) DEFAULT NULL,
  `twtretweets_15` bigint(20) DEFAULT NULL,
  `twtretweets_30` bigint(20) DEFAULT NULL,
  `twtretweets_60` bigint(20) DEFAULT NULL,
  `twtretweets_90` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `GroupId` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.groups
CREATE TABLE IF NOT EXISTS `groups` (
  `Id` binary(16) NOT NULL,
  `GroupName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.groupschedulemessage
CREATE TABLE IF NOT EXISTS `groupschedulemessage` (
  `Id` binary(16) NOT NULL,
  `ScheduleMessageId` binary(16) NOT NULL,
  `GroupId` varchar(350) CHARACTER SET utf8 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.inboxmessages
CREATE TABLE IF NOT EXISTS `inboxmessages` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `MessageId` varchar(50) DEFAULT NULL,
  `ProfileId` varchar(50) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL,
  `FromName` varchar(50) DEFAULT NULL,
  `RecipientId` varchar(50) DEFAULT NULL,
  `RecipientName` varchar(50) DEFAULT NULL,
  `Message` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `FromImageUrl` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecipientImageUrl` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(50) DEFAULT NULL,
  `MessageType` varchar(50) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `EntryTime` datetime DEFAULT NULL,
  `FollowerCount` varchar(50) DEFAULT NULL,
  `FollowingCount` varchar(50) DEFAULT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT '0',
  `Positive` double NOT NULL DEFAULT '0',
  `Negative` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `FromId` (`FromId`),
  KEY `UserId_ProfileId` (`UserId`,`ProfileId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.instagramaccount
CREATE TABLE IF NOT EXISTS `instagramaccount` (
  `Id` binary(16) NOT NULL,
  `InstagramId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `AccessToken` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `InsUserName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Followers` int(11) DEFAULT NULL,
  `FollowedBy` int(11) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `TotalImages` int(11) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.instagramcomment
CREATE TABLE IF NOT EXISTS `instagramcomment` (
  `ID` binary(16) NOT NULL,
  `FeedId` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `InstagramId` varchar(350) DEFAULT NULL,
  `Comment` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `CommentDate` varchar(50) DEFAULT NULL,
  `CommentId` varchar(350) DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfilePic` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.instagramfeed
CREATE TABLE IF NOT EXISTS `instagramfeed` (
  `Id` binary(16) NOT NULL,
  `FeedId` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `InstagramId` varchar(350) DEFAULT NULL,
  `FeedImageUrl` varchar(500) CHARACTER SET utf8 DEFAULT NULL,
  `LikeCount` int(11) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `FeedDate` varchar(50) DEFAULT NULL,
  `IsLike` int(10) DEFAULT NULL,
  `CommentCount` int(10) DEFAULT NULL,
  `AdminUser` varchar(50) DEFAULT NULL,
  `Feed` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `ImageUrl` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `FeedUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.instagrampostcomments
CREATE TABLE IF NOT EXISTS `instagrampostcomments` (
  `Id` binary(16) NOT NULL,
  `Profile_Id` text,
  `Feed_Id` text,
  `Commented_By_Id` text,
  `Commented_By_Name` text,
  `Comment` text,
  `Created_Time` datetime DEFAULT NULL,
  `Comment_Id` text,
  `Feed_Type` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.invitation
CREATE TABLE IF NOT EXISTS `invitation` (
  `Id` binary(16) NOT NULL,
  `SenderEmail` varchar(100) DEFAULT NULL,
  `SenderUserId` binary(16) NOT NULL,
  `FriendEmail` varchar(100) DEFAULT NULL,
  `FriendUserId` binary(16) NOT NULL,
  `InvitationCode` varchar(50) DEFAULT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT '0',
  `SendInvitationDate` datetime DEFAULT NULL,
  `AcceptInvitationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.linkedinaccount
CREATE TABLE IF NOT EXISTS `linkedinaccount` (
  `Id` binary(16) NOT NULL,
  `LinkedinUserId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `LinkedinUserName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `OAuthToken` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `OAuthSecret` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `OAuthVerifier` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Connections` int(10) DEFAULT NULL,
  `ProfileImageUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.linkedincompanypage
CREATE TABLE IF NOT EXISTS `linkedincompanypage` (
  `Id` binary(16) NOT NULL,
  `LinkedinPageId` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LinkedinPageName` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmailDomains` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthToken` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthSecret` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `OAuthVerifier` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `FoundedYear` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EndYear` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Locations` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Specialties` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `WebsiteUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `EmployeeCountRange` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Industries` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `CompanyType` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LogoUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `SquareLogoUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `BlogRssUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `UniversalName` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `NumFollowers` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.linkedincompanypageposts
CREATE TABLE IF NOT EXISTS `linkedincompanypageposts` (
  `Id` binary(16) NOT NULL,
  `Posts` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `PostDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PostId` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `UpdateKey` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PageId` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PostImageUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Likes` int(11) DEFAULT NULL,
  `Comments` int(11) DEFAULT NULL,
  `IsLiked` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.linkedinfeed
CREATE TABLE IF NOT EXISTS `linkedinfeed` (
  `Id` binary(16) NOT NULL,
  `Feeds` text CHARACTER SET utf8,
  `FeedsDate` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FeedId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromPicUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.linkedinmessage
CREATE TABLE IF NOT EXISTS `linkedinmessage` (
  `Id` binary(16) NOT NULL,
  `FromId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `CreatedDate` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Message` text CHARACTER SET utf8,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.log
CREATE TABLE IF NOT EXISTS `log` (
  `Id` binary(16) DEFAULT NULL,
  `ModuleName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Exception` text CHARACTER SET utf8,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.loginlogs
CREATE TABLE IF NOT EXISTS `loginlogs` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.mandrillaccount
CREATE TABLE IF NOT EXISTS `mandrillaccount` (
  `Id` binary(16) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Total` int(11) DEFAULT NULL,
  `Status` varchar(11) DEFAULT '1',
  `EntryDate` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.news
CREATE TABLE IF NOT EXISTS `news` (
  `Id` binary(16) DEFAULT NULL,
  `NewsDetail` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.newsletter
CREATE TABLE IF NOT EXISTS `newsletter` (
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


-- Dumping structure for table devsocioboard.newslettersetting
CREATE TABLE IF NOT EXISTS `newslettersetting` (
  `Id` binary(16) NOT NULL,
  `userId` varchar(50) NOT NULL,
  `groupReport_Daily` tinyint(4) NOT NULL DEFAULT '1',
  `groupReport_7` tinyint(4) NOT NULL DEFAULT '1',
  `groupReport_15` tinyint(4) NOT NULL DEFAULT '1',
  `groupReport_30` tinyint(4) NOT NULL DEFAULT '1',
  `groupReport_60` tinyint(4) NOT NULL DEFAULT '1',
  `groupReport_90` tinyint(4) NOT NULL DEFAULT '1',
  `others` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.package
CREATE TABLE IF NOT EXISTS `package` (
  `Id` binary(16) NOT NULL,
  `PackageName` varchar(100) DEFAULT NULL,
  `Pricing` double DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `TotalProfiles` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.paymenttransaction
CREATE TABLE IF NOT EXISTS `paymenttransaction` (
  `Id` binary(16) NOT NULL,
  `PayPalTransactionId` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `AmountPaid` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `PaymentDate` datetime DEFAULT NULL,
  `PaymentStatus` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `PayerId` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `ReceiverId` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `PayerEmail` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `PaypalPaymentDate` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `IPNTrackId` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `VersionType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `DetailsInfo` text CHARACTER SET utf8,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.paypaltransactions
CREATE TABLE IF NOT EXISTS `paypaltransactions` (
  `Id` binary(16) NOT NULL,
  `Token` varchar(100) NOT NULL,
  `TimeStamp` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `SBAmount` float NOT NULL DEFAULT '0',
  `ClientAmount` float NOT NULL DEFAULT '0',
  `TransactionId` varchar(100) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `Correlatedid` varchar(50) DEFAULT NULL,
  `Payerid` varchar(50) DEFAULT NULL,
  `Status` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.plugininfo
CREATE TABLE IF NOT EXISTS `plugininfo` (
  `id` binary(16) DEFAULT NULL,
  `url` text,
  `imageurl` text,
  `description` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.replymessage
CREATE TABLE IF NOT EXISTS `replymessage` (
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


-- Dumping structure for table devsocioboard.rssfeeds
CREATE TABLE IF NOT EXISTS `rssfeeds` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Message` varchar(900) DEFAULT NULL,
  `Duration` varchar(350) DEFAULT NULL,
  `ProfileScreenName` varchar(350) DEFAULT NULL,
  `FeedUrl` varchar(350) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '0 means running, 1 means not running',
  `Title` varchar(500) DEFAULT NULL,
  `PublishingDate` datetime DEFAULT NULL,
  `ProfileId` varchar(500) DEFAULT NULL,
  `Profiletype` varchar(500) DEFAULT NULL,
  `Link` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.rssreader
CREATE TABLE IF NOT EXISTS `rssreader` (
  `Id` binary(16) NOT NULL,
  `Title` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  `CreatedDate` datetime DEFAULT NULL,
  `FeedsUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PublishedDate` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` tinyint(1) DEFAULT '1' COMMENT '0 - running and 1 means not',
  `Link` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.sbgroupprofiles
CREATE TABLE IF NOT EXISTS `sbgroupprofiles` (
  `Id` binary(16) NOT NULL,
  `ProfileId` varchar(50) NOT NULL,
  `ProfileType` int(11) NOT NULL,
  `GroupId` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.sbgroups
CREATE TABLE IF NOT EXISTS `sbgroups` (
  `Id` binary(16) NOT NULL,
  `GroupName` varchar(500) NOT NULL,
  `AdminUserId` varchar(50) NOT NULL,
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsDefault` bit(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.scheduledmessage
CREATE TABLE IF NOT EXISTS `scheduledmessage` (
  `Id` binary(16) NOT NULL,
  `ShareMessage` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ClientTime` datetime DEFAULT NULL,
  `ScheduleTime` datetime DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT 'by default 0 and after success 1',
  `UserId` binary(16) DEFAULT NULL,
  `ProfileType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `PicUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Url` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.searchkeyword
CREATE TABLE IF NOT EXISTS `searchkeyword` (
  `Id` binary(32) NOT NULL,
  `UserId` binary(32) DEFAULT NULL,
  `KeyWord` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.shareathon
CREATE TABLE IF NOT EXISTS `shareathon` (
  `Id` binary(16) NOT NULL,
  `FacebookAccountId` binary(16) NOT NULL,
  `FacebookPageId` varchar(500) NOT NULL,
  `TimeIntervalMinutes` int(11) NOT NULL,
  `LastPostId` varchar(50) DEFAULT NULL,
  `LastShareTimeStamp` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserId` binary(16) NOT NULL,
  `IsHidden` bit(1) NOT NULL DEFAULT b'1',
  `FacebookStatus` int(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.shareathongroup
CREATE TABLE IF NOT EXISTS `shareathongroup` (
  `Id` binary(16) NOT NULL,
  `FacebookPageId` text NOT NULL,
  `FacebookPageUrl` text NOT NULL,
  `FacebookGroupId` text NOT NULL,
  `FacebookNameId` text NOT NULL,
  `FacebookAccountid` binary(16) NOT NULL,
  `AccessToken` text NOT NULL,
  `TimeIntervalMinutes` int(11) NOT NULL,
  `LastPostId` varchar(50) DEFAULT NULL,
  `LastShareTimeStamp` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserId` binary(16) NOT NULL,
  `IsHidden` bit(1) NOT NULL,
  `FacebookStatus` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.sharethongrouppost
CREATE TABLE IF NOT EXISTS `sharethongrouppost` (
  `Id` binary(16) DEFAULT NULL,
  `Facebookgroupid` varchar(50) DEFAULT NULL,
  `Facebookaccountid` varchar(50) DEFAULT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `PostedTime` datetime DEFAULT NULL,
  `Facebookgroupname` varchar(350) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.sharethonpost
CREATE TABLE IF NOT EXISTS `sharethonpost` (
  `Id` binary(16) DEFAULT NULL,
  `Facebookpageid` varchar(50) DEFAULT NULL,
  `Facebookaccountid` varchar(50) DEFAULT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `PostedTime` datetime DEFAULT NULL,
  `Facebookpagename` varchar(350) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.socialprofile
CREATE TABLE IF NOT EXISTS `socialprofile` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileDate` datetime DEFAULT NULL,
  `ProfileStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.taskcomment
CREATE TABLE IF NOT EXISTS `taskcomment` (
  `Id` binary(16) DEFAULT NULL,
  `Comment` text CHARACTER SET latin1,
  `UserId` binary(16) DEFAULT NULL,
  `TaskId` binary(16) DEFAULT NULL,
  `CommentDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.tasks
CREATE TABLE IF NOT EXISTS `tasks` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `TaskMessage` text CHARACTER SET utf8,
  `UserId` binary(16) DEFAULT NULL,
  `AssignTaskTo` binary(16) DEFAULT NULL,
  `TaskStatus` tinyint(1) DEFAULT NULL COMMENT '0 means incomplete',
  `AssignDate` datetime DEFAULT NULL,
  `CompletionDate` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ReadStatus` tinyint(1) DEFAULT '0',
  `TaskMessageDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.team
CREATE TABLE IF NOT EXISTS `team` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET utf8 NOT NULL,
  `FirstName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `InviteDate` datetime DEFAULT NULL,
  `StatusUpdateDate` datetime DEFAULT NULL,
  `InviteStatus` tinyint(1) DEFAULT NULL COMMENT '1=invited,2=accepted',
  `AccessLevel` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.teammemberprofile
CREATE TABLE IF NOT EXISTS `teammemberprofile` (
  `Id` binary(16) NOT NULL,
  `TeamId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '1 = invited,2=accepted',
  `StatusUpdateDate` datetime DEFAULT NULL,
  `ProfilePicUrl` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileName` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.ticketassigneestatus
CREATE TABLE IF NOT EXISTS `ticketassigneestatus` (
  `Id` binary(16) DEFAULT NULL,
  `AssigneeUserId` binary(16) DEFAULT NULL,
  `AssignedTicketCount` int(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.tmp
CREATE TABLE IF NOT EXISTS `tmp` (
  `Id` binary(16) NOT NULL,
  `Link` varchar(500) DEFAULT NULL,
  `ImageUrl` varchar(500) DEFAULT NULL,
  `tags` varchar(5000) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `InstagramAccountId` binary(16) NOT NULL,
  `FeedId` varchar(500) DEFAULT NULL,
  `IsVisible` bit(1) NOT NULL,
  `FromId` varchar(500) DEFAULT NULL,
  `FromName` varchar(500) DEFAULT NULL,
  `FromPicUrl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.tumblraccount
CREATE TABLE IF NOT EXISTS `tumblraccount` (
  `Id` binary(16) DEFAULT NULL,
  `tblrUserName` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `tblrAccessToken` varchar(350) DEFAULT NULL,
  `tblrAccessTokenSecret` varchar(350) DEFAULT NULL,
  `tblrProfilePicUrl` varchar(350) DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.tumblrfeed
CREATE TABLE IF NOT EXISTS `tumblrfeed` (
  `Id` binary(16) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(50) DEFAULT NULL,
  `imageurl` varchar(350) DEFAULT NULL,
  `videourl` varchar(350) DEFAULT NULL,
  `blogname` varchar(100) DEFAULT NULL,
  `blogId` varchar(100) DEFAULT NULL,
  `blogposturl` varchar(350) DEFAULT NULL,
  `description` text CHARACTER SET utf8,
  `slug` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `date` timestamp NULL DEFAULT NULL,
  `reblogkey` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `liked` int(11) DEFAULT NULL,
  `followed` int(11) DEFAULT NULL,
  `canreply` int(11) DEFAULT NULL,
  `notes` int(11) DEFAULT NULL,
  `sourceurl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `sourcetitle` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `timestamp` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.twitteraccount
CREATE TABLE IF NOT EXISTS `twitteraccount` (
  `Id` binary(16) NOT NULL,
  `TwitterUserId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `TwitterScreenName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `OAuthToken` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `OAuthSecret` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `FollowersCount` int(11) DEFAULT NULL,
  `FollowingCount` int(11) DEFAULT NULL,
  `ProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileImageUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `TwitterName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.twitterdirectmessages
CREATE TABLE IF NOT EXISTS `twitterdirectmessages` (
  `Id` binary(16) NOT NULL,
  `RecipientId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `RecipientProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Message` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `RecipientScreenName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `SenderId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `SenderScreenName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `SenderProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `MessageId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Image` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.twitterfeed
CREATE TABLE IF NOT EXISTS `twitterfeed` (
  `Id` binary(16) NOT NULL,
  `TwitterFeed` text CHARACTER SET utf8,
  `FeedDate` datetime DEFAULT NULL,
  `FromId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ScreenName` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `MessageId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `SourceUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromScreenName` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FromId` (`FromId`),
  KEY `MessageId` (`MessageId`),
  KEY `FromScreenName` (`FromScreenName`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `ScreenName` (`ScreenName`),
  KEY `FromId_UserId` (`FromId`,`UserId`),
  KEY `FeedDate_UserId` (`FeedDate`,`UserId`),
  KEY `FeedDate_ProfileId_UserId` (`FeedDate`,`ProfileId`,`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.twittermessage
CREATE TABLE IF NOT EXISTS `twittermessage` (
  `Id` binary(16) NOT NULL,
  `TwitterMessage` text CHARACTER SET utf8,
  `MessageDate` datetime DEFAULT NULL,
  `MessageId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `FromName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `FromProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileId` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ScreenName` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `SourceUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `FromScreenName` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `ReadStatus` int(1) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  `IsArchived` int(1) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId` (`MessageId`),
  KEY `FromId` (`FromId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `UserId` (`UserId`),
  KEY `ScreenName` (`ScreenName`),
  KEY `FromScreenName` (`FromScreenName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.twitterstats
CREATE TABLE IF NOT EXISTS `twitterstats` (
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


-- Dumping structure for table devsocioboard.user
CREATE TABLE IF NOT EXISTS `user` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `EmailId` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ProfileUrl` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `AccountType` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `UserStatus` tinyint(1) DEFAULT NULL COMMENT '0 = InActive,1 = Active|| unpaid,2=Active||paid',
  `Password` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `TimeZone` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `PaymentStatus` varchar(6) CHARACTER SET utf8 DEFAULT 'unpaid' COMMENT 'default value = unpaid',
  `ActivationStatus` varchar(5) CHARACTER SET utf8 DEFAULT '0' COMMENT '0 = InActive,1 = Active',
  `CouponCode` varchar(350) CHARACTER SET utf8 DEFAULT NULL,
  `ReferenceStatus` varchar(5) CHARACTER SET utf8 DEFAULT '0',
  `RefereeStatus` varchar(5) CHARACTER SET utf8 DEFAULT '0',
  `UserType` varchar(350) CHARACTER SET utf8 DEFAULT 'user' COMMENT 'default value=user',
  `ChangePasswordKey` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsKeyUsed` tinyint(1) DEFAULT '0',
  `ChangeEmailKey` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsEmailKeyUsed` tinyint(4) DEFAULT '0',
  `UserCode` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Ewallet` varchar(50) COLLATE utf8_unicode_ci DEFAULT '0',
  `SocialLogin` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastLoginTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `Mail_Daily` bit(1) NOT NULL DEFAULT b'1',
  `Mail_15Days` bit(1) NOT NULL DEFAULT b'1',
  `Mail_30Days` bit(1) NOT NULL DEFAULT b'1',
  `Mail_60Days` bit(1) NOT NULL DEFAULT b'1',
  `Mail_90Days` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.useractivation
CREATE TABLE IF NOT EXISTS `useractivation` (
  `ID` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `ActivationStatus` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.userfacebook
CREATE TABLE IF NOT EXISTS `userfacebook` (
  `UserId` binary(16) NOT NULL,
  `FacebookAccountId` binary(16) NOT NULL,
  `Id` binary(16) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `FacebookAccountId` (`FacebookAccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.usergroup
CREATE TABLE IF NOT EXISTS `usergroup` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `UserEmail` varchar(50) NOT NULL,
  `GroupName` varchar(50) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.userpackagerelation
CREATE TABLE IF NOT EXISTS `userpackagerelation` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PackageId` binary(16) NOT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `PackageStatus` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.userrefrelation
CREATE TABLE IF NOT EXISTS `userrefrelation` (
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


-- Dumping structure for table devsocioboard.vineaccount
CREATE TABLE IF NOT EXISTS `vineaccount` (
  `Id` binary(16) NOT NULL,
  `VineEmail` varchar(50) DEFAULT NULL,
  `UserProfilePic` varchar(200) DEFAULT NULL,
  `VineUserName` varchar(50) DEFAULT NULL,
  `VineUserId` varchar(50) DEFAULT NULL,
  `VineKey` varchar(100) DEFAULT NULL,
  `LoopCount` int(10) DEFAULT NULL,
  `FollowerCount` int(10) DEFAULT NULL,
  `FollowingCount` int(10) DEFAULT NULL,
  `PostCount` int(10) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.vineuservideos
CREATE TABLE IF NOT EXISTS `vineuservideos` (
  `Id` binary(16) NOT NULL,
  `Likes` varchar(10) DEFAULT '0',
  `Loops` varchar(10) DEFAULT '0',
  `Comments` varchar(10) DEFAULT '0',
  `VideoUrl` varchar(200) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `VineUserId` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.wordpressaccount
CREATE TABLE IF NOT EXISTS `wordpressaccount` (
  `Id` binary(16) NOT NULL,
  `WpUserId` varchar(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `DisplayName` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `WpUserName` varchar(50) CHARACTER SET ucs2 COLLATE ucs2_unicode_ci DEFAULT NULL,
  `EmailId` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PrimaryBlogId` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `TokenSiteId` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserAvtar` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileUrl` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `SiteCount` int(11) DEFAULT '0',
  `UserId` binary(16) DEFAULT NULL,
  `AccessToken` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.wordpressfeeds
CREATE TABLE IF NOT EXISTS `wordpressfeeds` (
  `Id` binary(16) NOT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `Title` varchar(50) DEFAULT NULL,
  `PostUrl` varchar(200) DEFAULT NULL,
  `PostContent` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `CommentCount` varchar(50) DEFAULT NULL,
  `LikeCount` varchar(50) DEFAULT NULL,
  `ILike` varchar(50) DEFAULT NULL,
  `SiteId` varchar(50) DEFAULT NULL,
  `WPUserId` varchar(50) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `ModifiedTime` datetime NOT NULL,
  `CreatedTime` datetime NOT NULL,
  `EntryTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.wordpresssites
CREATE TABLE IF NOT EXISTS `wordpresssites` (
  `Id` binary(16) NOT NULL,
  `SiteId` varchar(50) DEFAULT NULL,
  `SiteName` varchar(50) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `SiteURL` varchar(100) DEFAULT NULL,
  `Post_Count` int(11) DEFAULT '0',
  `Subscribers_Count` int(11) DEFAULT '0',
  `UserId` binary(16) DEFAULT NULL,
  `WPUserId` varchar(50) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `EntryTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.youtubeaccount
CREATE TABLE IF NOT EXISTS `youtubeaccount` (
  `Id` binary(16) DEFAULT NULL,
  `YtUserId` varchar(350) DEFAULT NULL,
  `YtUserName` varchar(350) DEFAULT NULL,
  `YtProfileImage` varchar(350) DEFAULT NULL,
  `AccessToken` varchar(500) DEFAULT NULL,
  `RefreshToken` varchar(500) DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `EmailId` varchar(350) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.youtubechannel
CREATE TABLE IF NOT EXISTS `youtubechannel` (
  `Id` binary(16) DEFAULT NULL,
  `ChannelId` varchar(500) DEFAULT NULL,
  `LikesId` varchar(500) DEFAULT NULL,
  `FavoritesId` varchar(500) DEFAULT NULL,
  `UploadsId` varchar(500) DEFAULT NULL,
  `WatchHistoryId` varchar(500) DEFAULT NULL,
  `WatchLaterId` varchar(500) DEFAULT NULL,
  `GooglePlusUserId` varchar(500) DEFAULT NULL,
  `ViewCount` int(10) DEFAULT NULL,
  `CommentCount` int(10) DEFAULT NULL,
  `SubscriberCount` int(10) DEFAULT NULL,
  `HiddenSubscriberCount` int(10) DEFAULT NULL,
  `VideoCount` int(10) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table devsocioboard.youtubesubscription
CREATE TABLE IF NOT EXISTS `youtubesubscription` (
  `Id` binary(16) DEFAULT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `ResourceChannelId` varchar(250) DEFAULT NULL,
  `ProfileId` varchar(250) DEFAULT NULL,
  `EntryDate` varchar(250) DEFAULT NULL,
  `Thumbnail` varchar(250) DEFAULT NULL,
  `ViewCount` varchar(250) DEFAULT NULL,
  `VideoCount` varchar(250) DEFAULT NULL,
  `SubscriberCount` varchar(250) DEFAULT NULL,
  `HiddenSubscriberCount` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
