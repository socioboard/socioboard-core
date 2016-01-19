-- ----------------------------------------------------------
-- Host:                         109.169.46.210
-- Server version:               5.6.22-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             7.0.0.4390
-- ----------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for socioboard
CREATE DATABASE IF NOT EXISTS `socioboard` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `socioboard`;


-- Dumping structure for table socioboard.admin
CREATE TABLE IF NOT EXISTS `admin` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `Image` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TimeZone` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.ads
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


-- Dumping structure for table socioboard.affiliates
CREATE TABLE IF NOT EXISTS `affiliates` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `FriendUserId` binary(16) NOT NULL,
  `AffiliateDate` datetime NOT NULL,
  `Amount` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.archivemessage
CREATE TABLE IF NOT EXISTS `archivemessage` (
  `Id` binary(16) NOT NULL,
  `SocialGroup` varchar(50) DEFAULT '0',
  `ImgUrl` varchar(500) DEFAULT '0',
  `ProfileId` varchar(500) DEFAULT '0',
  `UserId` binary(16) DEFAULT '0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0',
  `UserName` varchar(50) DEFAULT '0',
  `MessageId` varchar(50) DEFAULT '0',
  `Message` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `CreatedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.blog_comments
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


-- Dumping structure for table socioboard.blog_posts
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


-- Dumping structure for table socioboard.boardfbfeeds
CREATE TABLE IF NOT EXISTS `boardfbfeeds` (
  `Id` binary(16) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Link` varchar(5000) DEFAULT NULL,
  `Message` varchar(5000) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Description` varchar(5000) DEFAULT NULL,
  `FeedId` varchar(500) NOT NULL,
  `IsVisible` bit(1) NOT NULL,
  `VideoLink` varchar(500) DEFAULT NULL,
  `Story` varchar(5000) DEFAULT NULL,
  `FbPageProfileId` binary(16) NOT NULL,
  `CoverPhoto` varchar(500) DEFAULT NULL,
  `Image` varchar(500) DEFAULT NULL,
  `Likes` varchar(500) DEFAULT NULL,
  `FromId` varchar(500) DEFAULT NULL,
  `FromName` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardfbpage
CREATE TABLE IF NOT EXISTS `boardfbpage` (
  `Id` binary(16) NOT NULL,
  `FbPageId` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `TalkingAboutCount` varchar(50) DEFAULT NULL,
  `Likes` varchar(50) DEFAULT NULL,
  `Checkins` varchar(50) DEFAULT NULL,
  `fbPageName` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `ProfileImageUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `BoardId` binary(16) NOT NULL,
  `PageUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Country` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `City` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Phone` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Website` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Founded` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Mission` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `About` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Companyname` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `Entrydate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardgplusaccount
CREATE TABLE IF NOT EXISTS `boardgplusaccount` (
  `Id` binary(16) NOT NULL,
  `PageId` varchar(500) NOT NULL,
  `CircledByCount` varchar(50) DEFAULT NULL,
  `PlusOneCount` varchar(50) DEFAULT NULL,
  `DisplayName` varchar(500) DEFAULT NULL,
  `BoardId` binary(16) NOT NULL,
  `Nickname` varchar(500) DEFAULT NULL,
  `AboutMe` varchar(500) DEFAULT NULL,
  `PageUrl` varchar(500) DEFAULT NULL,
  `ProfileImageUrl` varchar(500) DEFAULT NULL,
  `Tagline` varchar(500) DEFAULT NULL,
  `CoverPhotoUrl` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardgplusfeeds
CREATE TABLE IF NOT EXISTS `boardgplusfeeds` (
  `Id` binary(16) NOT NULL,
  `FeedLink` varchar(500) DEFAULT NULL,
  `gplusboardaccProfileId` binary(16) NOT NULL,
  `PublishedTime` datetime DEFAULT NULL,
  `ImageUrl` varchar(500) DEFAULT NULL,
  `Title` varchar(500) DEFAULT NULL,
  `FeedId` varchar(50) DEFAULT NULL,
  `FromId` varchar(500) DEFAULT NULL,
  `FromName` varchar(500) DEFAULT NULL,
  `FromPicUrl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardinstagramaccount
CREATE TABLE IF NOT EXISTS `boardinstagramaccount` (
  `Id` binary(16) NOT NULL,
  `UserName` varchar(500) NOT NULL,
  `FollowedByCount` varchar(50) NOT NULL,
  `FollowsCount` varchar(50) NOT NULL,
  `BoardId` binary(16) NOT NULL,
  `ProfilePicUrl` varchar(500) DEFAULT NULL,
  `ProfileId` varchar(500) DEFAULT NULL,
  `Url` varchar(500) DEFAULT NULL,
  `Bio` varchar(500) DEFAULT NULL,
  `Media` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardinstagramfeeds
CREATE TABLE IF NOT EXISTS `boardinstagramfeeds` (
  `Id` binary(16) NOT NULL,
  `Link` varchar(500) DEFAULT NULL,
  `ImageUrl` varchar(500) DEFAULT NULL,
  `tags` varchar(5000) DEFAULT NULL,
  `CreatedTime` datetime DEFAULT NULL,
  `InstagramAccountId` binary(16) NOT NULL,
  `FeedId` varchar(100) DEFAULT NULL,
  `IsVisible` bit(1) NOT NULL,
  `FromId` varchar(500) DEFAULT NULL,
  `FromName` varchar(500) DEFAULT NULL,
  `FromPicUrl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `InstagramAccountId_FeedId` (`FeedId`,`InstagramAccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boards
CREATE TABLE IF NOT EXISTS `boards` (
  `BoardId` binary(16) NOT NULL,
  `BoardName` varchar(500) DEFAULT NULL,
  `Instagramprofileid` varchar(500) DEFAULT NULL,
  `Fbprofileid` varchar(500) DEFAULT NULL,
  `Twitterprofileid` varchar(500) DEFAULT NULL,
  `Linkedinprofileid` varchar(500) DEFAULT NULL,
  `Youtubeprofileid` varchar(500) DEFAULT NULL,
  `Gplusprofileid` varchar(500) DEFAULT NULL,
  `Tumblrprofileid` varchar(500) DEFAULT NULL,
  `UserId` binary(16) NOT NULL,
  `CreateDate` datetime NOT NULL DEFAULT '2015-05-26 18:18:43',
  `IsHidden` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`BoardId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardtwitteraccount
CREATE TABLE IF NOT EXISTS `boardtwitteraccount` (
  `Id` binary(16) NOT NULL,
  `ScreenName` varchar(500) NOT NULL,
  `StatusCount` varchar(50) NOT NULL,
  `FriendsCount` varchar(50) NOT NULL,
  `FollowersCount` varchar(50) NOT NULL,
  `FavouritesCount` varchar(50) NOT NULL,
  `BoardId` binary(16) NOT NULL,
  `TwitterProfileId` varchar(500) NOT NULL,
  `ProfileImageUrl` varchar(5000) NOT NULL,
  `Url` varchar(500) DEFAULT NULL,
  `Tweet` varchar(500) DEFAULT NULL,
  `PhotosVideos` varchar(500) DEFAULT NULL,
  `FollowingsCount` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.boardtwitterfeeds
CREATE TABLE IF NOT EXISTS `boardtwitterfeeds` (
  `Id` binary(16) NOT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `FeedId` varchar(500) NOT NULL,
  `FeedUrl` varchar(5000) DEFAULT NULL,
  `ImageUrl` varchar(5000) DEFAULT NULL,
  `Text` varchar(5000) DEFAULT NULL,
  `RetweetCount` int(11) DEFAULT NULL,
  `FavoritedCount` int(11) DEFAULT NULL,
  `HashTags` varchar(5000) DEFAULT NULL,
  `TwitterProfileId` binary(16) NOT NULL,
  `IsVisible` bit(1) NOT NULL,
  `FromId` varchar(500) DEFAULT NULL,
  `FromName` varchar(500) DEFAULT NULL,
  `FromPicUrl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.businesssetting
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


-- Dumping structure for table socioboard.companydashboardprofiles
CREATE TABLE IF NOT EXISTS `companydashboardprofiles` (
  `Id` binary(16) NOT NULL,
  `DashboardName` varchar(500) NOT NULL,
  `FacebookId` varchar(500) NOT NULL,
  `TwitterId` varchar(500) NOT NULL,
  `YoutubeId` varchar(500) NOT NULL,
  `GplusId` varchar(500) NOT NULL,
  `LinkedinId` varchar(500) NOT NULL,
  `TumblrId` varchar(500) NOT NULL,
  `InstagramId` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.companyprofiles
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


-- Dumping structure for table socioboard.coupon
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


-- Dumping structure for table socioboard.discoverysearch
CREATE TABLE IF NOT EXISTS `discoverysearch` (
  `Id` binary(16) NOT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Message` text COLLATE utf8_unicode_ci,
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


-- Dumping structure for table socioboard.drafts
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


-- Dumping structure for table socioboard.ewalletwithdrawrequest
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


-- Dumping structure for table socioboard.facebookaccount
CREATE TABLE IF NOT EXISTS `facebookaccount` (
  `Id` binary(16) NOT NULL,
  `FbUserId` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `FbUserName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccessToken` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Friends` int(11) DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FbUserId_UserId` (`FbUserId`,`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.facebookfanpage
CREATE TABLE IF NOT EXISTS `facebookfanpage` (
  `Id` binary(16) DEFAULT NULL,
  `ProfilePageId` varchar(50) DEFAULT NULL,
  `FanpageCount` varchar(50) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.facebookfeed
CREATE TABLE IF NOT EXISTS `facebookfeed` (
  `Id` binary(16) NOT NULL,
  `FeedDescription` text COLLATE utf8_unicode_ci,
  `FeedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbComment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbLike` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReadStatus` int(11) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  `Picture` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserFeedUnique` (`UserId`,`FeedId`),
  KEY `FromId` (`FromId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `FeedId` (`FeedId`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.facebookgroup
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


-- Dumping structure for table socioboard.facebookinsightpoststats
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


-- Dumping structure for table socioboard.facebookinsightstats
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


-- Dumping structure for table socioboard.facebookmessage
CREATE TABLE IF NOT EXISTS `facebookmessage` (
  `Id` binary(16) NOT NULL,
  `Message` text COLLATE utf8_unicode_ci,
  `MessageDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbComment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FbLike` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Picture` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsArchived` int(1) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId_UserId` (`MessageId`,`UserId`),
  KEY `FromId` (`FromId`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `MessageDate_ProfileId_UserId` (`UserId`,`ProfileId`,`MessageDate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.facebookstats
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
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `FbUserId` (`FbUserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.fbpagecomment
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


-- Dumping structure for table socioboard.fbpageliker
CREATE TABLE IF NOT EXISTS `fbpageliker` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PostId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.fbpagepost
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


-- Dumping structure for table socioboard.fbpagepostcommentliker
CREATE TABLE IF NOT EXISTS `fbpagepostcommentliker` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `CommentId` varchar(50) DEFAULT NULL,
  `FromName` varchar(100) DEFAULT NULL,
  `FromId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.fbpagesharer
CREATE TABLE IF NOT EXISTS `fbpagesharer` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PostId` varchar(50) NOT NULL,
  `FromName` varchar(100) NOT NULL,
  `FromId` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.feedsentimentalanalysis
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


-- Dumping structure for table socioboard.googleanalyticsaccount
CREATE TABLE IF NOT EXISTS `googleanalyticsaccount` (
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


-- Dumping structure for table socioboard.googleanalyticsstats
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


-- Dumping structure for table socioboard.googleplusaccount
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


-- Dumping structure for table socioboard.googleplusactivities
CREATE TABLE IF NOT EXISTS `googleplusactivities` (
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
  `Attachment` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AttachmentType` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RepliesCount` int(11) DEFAULT NULL,
  `PlusonersCount` int(11) DEFAULT NULL,
  `ResharersCount` int(11) DEFAULT NULL,
  `PublishedDate` varchar(80) CHARACTER SET latin1 DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.groupprofile
CREATE TABLE IF NOT EXISTS `groupprofile` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `GroupOwnerId` binary(16) DEFAULT NULL COMMENT 'This is UserId',
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.groups
CREATE TABLE IF NOT EXISTS `groups` (
  `Id` binary(16) NOT NULL,
  `GroupName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.groupschedulemessage
CREATE TABLE IF NOT EXISTS `groupschedulemessage` (
  `Id` binary(16) NOT NULL,
  `ScheduleMessageId` binary(16) NOT NULL,
  `GroupId` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ScheduleMessageId` (`ScheduleMessageId`),
  KEY `GroupId` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.inboxmessages
CREATE TABLE IF NOT EXISTS `inboxmessages` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `MessageId` varchar(50) DEFAULT NULL,
  `ProfileId` varchar(50),
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
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `FromId` (`FromId`),
  KEY `UserId_ProfileId` (`UserId`,`ProfileId`),
  KEY `CreatedTime_UserId_ProfileId_MessageType_Status` (`CreatedTime`,`UserId`,`ProfileId`,`MessageType`,`Status`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.instagramaccount
CREATE TABLE IF NOT EXISTS `instagramaccount` (
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


-- Dumping structure for table socioboard.instagramcomment
CREATE TABLE IF NOT EXISTS `instagramcomment` (
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
  PRIMARY KEY (`ID`),
  KEY `UserId` (`UserId`),
  KEY `FeedId` (`FeedId`),
  KEY `InstagramId` (`InstagramId`),
  KEY `CommentId` (`CommentId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.instagramfeed
CREATE TABLE IF NOT EXISTS `instagramfeed` (
  `Id` binary(16) NOT NULL,
  `FeedId` varchar(200) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `InstagramId` varchar(200) DEFAULT NULL,
  `FeedImageUrl` varchar(500) DEFAULT NULL,
  `LikeCount` int(11) DEFAULT '0',
  `EntryDate` datetime DEFAULT NULL,
  `FeedDate` varchar(50) DEFAULT NULL,
  `CommentCount` int(11) DEFAULT '0',
  `IsLike` int(11) DEFAULT '0',
  `AdminUser` text,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `FeedId` (`FeedId`),
  KEY `InstagramId` (`InstagramId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.invitation
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


-- Dumping structure for table socioboard.linkedinaccount
CREATE TABLE IF NOT EXISTS `linkedinaccount` (
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


-- Dumping structure for table socioboard.linkedincompanypage
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


-- Dumping structure for table socioboard.linkedincompanypageposts
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


-- Dumping structure for table socioboard.linkedinfeed
CREATE TABLE IF NOT EXISTS `linkedinfeed` (
  `Id` binary(16) NOT NULL,
  `Feeds` text COLLATE utf8_unicode_ci,
  `FeedsDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromPicUrl` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ImageUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `FeedId` (`FeedId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `FromId` (`FromId`),
  KEY `FeedsDate_UserId` (`FeedsDate`,`UserId`),
  KEY `UserId_ProfileId_FromId` (`UserId`,`ProfileId`,`FromId`),
  KEY `FeedsDate_UserId_FeedId` (`UserId`,`FeedId`,`FeedsDate`),
  KEY `FeedsDate` (`FeedsDate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.linkedinmessage
CREATE TABLE IF NOT EXISTS `linkedinmessage` (
  `Id` binary(16) NOT NULL,
  `ProfileId` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileUrl` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FeedId` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Message` text COLLATE utf8_unicode_ci,
  `ImageUrl` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Likes` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Comments` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileImageUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `ProfileId_UserId_CreatedDate` (`ProfileId`,`UserId`,`CreatedDate`),
  KEY `UserId_FeedId` (`UserId`,`FeedId`),
  KEY `ProfileId_UserId` (`ProfileId`,`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.log
CREATE TABLE IF NOT EXISTS `log` (
  `Id` binary(16) DEFAULT NULL,
  `ModuleName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Exception` text COLLATE utf8_unicode_ci,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.loginlogs
CREATE TABLE IF NOT EXISTS `loginlogs` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.mandrillaccount
CREATE TABLE IF NOT EXISTS `mandrillaccount` (
  `Id` binary(16) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Total` int(11) DEFAULT NULL,
  `Status` varchar(11) DEFAULT '1',
  `EntryDate` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.news
CREATE TABLE IF NOT EXISTS `news` (
  `Id` binary(16) DEFAULT NULL,
  `NewsDetail` varchar(500) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.newsletter
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


-- Dumping structure for table socioboard.package
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


-- Dumping structure for table socioboard.paymenttransaction
CREATE TABLE IF NOT EXISTS `paymenttransaction` (
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


-- Dumping structure for table socioboard.replymessage
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


-- Dumping structure for table socioboard.rssfeeds
CREATE TABLE IF NOT EXISTS `rssfeeds` (
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


-- Dumping structure for table socioboard.rssreader
CREATE TABLE IF NOT EXISTS `rssreader` (
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


-- Dumping structure for table socioboard.scheduledmessage
CREATE TABLE IF NOT EXISTS `scheduledmessage` (
  `Id` binary(16) NOT NULL,
  `ShareMessage` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ClientTime` datetime DEFAULT NULL,
  `ScheduleTime` datetime DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT 'by default 0 and after success 1',
  `UserId` binary(16) DEFAULT NULL,
  `ProfileType` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PicUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ProfileType` (`ProfileType`),
  KEY `UserId_ProfileType` (`UserId`,`ProfileType`),
  KEY `ScheduleTime_ProfileType` (`ScheduleTime`,`ProfileType`),
  KEY `ScheduleTime_UserId_ProfileType` (`ScheduleTime`,`UserId`,`ProfileType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.searchkeyword
CREATE TABLE IF NOT EXISTS `searchkeyword` (
  `Id` binary(32) NOT NULL,
  `UserId` binary(32) DEFAULT NULL,
  `KeyWord` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.socialprofile
CREATE TABLE IF NOT EXISTS `socialprofile` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileDate` datetime DEFAULT NULL,
  `ProfileStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileType` (`ProfileType`),
  KEY `ProfileId` (`ProfileId`),
  KEY `ProfileType_ProfileDate` (`ProfileType`,`ProfileDate`),
  KEY `ProfileType_ProfileDate_ProfileStatus` (`ProfileType`,`ProfileDate`,`ProfileStatus`),
  KEY `ProfileType_ProfileStatus` (`ProfileType`,`ProfileStatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.taskcomment
CREATE TABLE IF NOT EXISTS `taskcomment` (
  `Id` binary(16) DEFAULT NULL,
  `Comment` text CHARACTER SET latin1,
  `UserId` binary(16) DEFAULT NULL,
  `TaskId` binary(16) DEFAULT NULL,
  `CommentDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.tasks
CREATE TABLE IF NOT EXISTS `tasks` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `TaskMessage` text COLLATE utf8_unicode_ci,
  `UserId` binary(16) DEFAULT NULL,
  `AssignTaskTo` binary(16) DEFAULT NULL,
  `TaskStatus` tinyint(1) DEFAULT NULL COMMENT '0 means incomplete',
  `AssignDate` datetime DEFAULT NULL,
  `CompletionDate` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReadStatus` tinyint(1) DEFAULT NULL,
  `TaskMessageDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.team
CREATE TABLE IF NOT EXISTS `team` (
  `Id` binary(16) NOT NULL,
  `GroupId` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `EmailId` varchar(350) COLLATE utf8_unicode_ci NOT NULL,
  `FirstName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InviteDate` datetime DEFAULT NULL,
  `StatusUpdateDate` datetime DEFAULT NULL,
  `InviteStatus` tinyint(1) DEFAULT NULL COMMENT '1=invited,2=accepted',
  `AccessLevel` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.teammemberprofile
CREATE TABLE IF NOT EXISTS `teammemberprofile` (
  `Id` binary(16) NOT NULL,
  `TeamId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileType` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL COMMENT '1 = invited,2=accepted',
  `StatusUpdateDate` datetime DEFAULT NULL,
  `ProfilePicUrl` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileName` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.ticketassigneestatus
CREATE TABLE IF NOT EXISTS `ticketassigneestatus` (
  `Id` binary(16) DEFAULT NULL,
  `AssigneeUserId` binary(16) DEFAULT NULL,
  `AssignedTicketCount` int(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.tumblraccount
CREATE TABLE IF NOT EXISTS `tumblraccount` (
  `Id` binary(16) NOT NULL,
  `tblrUserName` varchar(350) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `tblrAccessToken` varchar(350) DEFAULT NULL,
  `tblrAccessTokenSecret` varchar(350) DEFAULT NULL,
  `tblrProfilePicUrl` varchar(350) DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.tumblrfeed
CREATE TABLE IF NOT EXISTS `tumblrfeed` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(50) DEFAULT NULL,
  `imageurl` varchar(350) DEFAULT NULL,
  `videourl` varchar(350) DEFAULT NULL,
  `blogname` varchar(100) DEFAULT NULL,
  `blogId` varchar(100) DEFAULT NULL,
  `blogposturl` varchar(350) DEFAULT NULL,
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `slug` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `type` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `date` timestamp NULL DEFAULT NULL,
  `reblogkey` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `liked` int(11) DEFAULT NULL,
  `followed` int(11) DEFAULT NULL,
  `canreply` int(11) DEFAULT NULL,
  `notes` int(11) DEFAULT NULL,
  `sourceurl` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `sourcetitle` varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `timestamp` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twitteraccount
CREATE TABLE IF NOT EXISTS `twitteraccount` (
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


-- Dumping structure for table socioboard.twitteraccountfollowers
CREATE TABLE IF NOT EXISTS `twitteraccountfollowers` (
  `Id` binary(16) DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `FollowersCount` varchar(50) DEFAULT NULL,
  `FollowingsCount` varchar(50) DEFAULT NULL,
  `ProfileId` varchar(50) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twitterdirectmessages
CREATE TABLE IF NOT EXISTS `twitterdirectmessages` (
  `Id` binary(16) NOT NULL,
  `RecipientId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecipientProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Message` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecipientScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderId` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderScreenName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Image` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `SenderId` (`SenderId`),
  KEY `CreatedDate_RecipientId_Type_UserId` (`CreatedDate`,`RecipientId`,`Type`,`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twitterengagement
CREATE TABLE IF NOT EXISTS `twitterengagement` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `ProfileId` varchar(50) DEFAULT NULL,
  `Engagement` varchar(50) DEFAULT '0',
  `RetweetCount` varchar(50) DEFAULT NULL,
  `ReplyCount` varchar(50) DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `Influence` varchar(50) DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `EntryDate` (`EntryDate`),
  KEY `UserId_ProfileId_EntryDate` (`UserId`,`ProfileId`,`EntryDate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twitterfeed
CREATE TABLE IF NOT EXISTS `twitterfeed` (
  `Id` binary(16) NOT NULL,
  `TwitterFeed` text COLLATE utf8_unicode_ci,
  `FeedDate` datetime DEFAULT NULL,
  `FromId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ProfileId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ScreenName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MessageId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SourceUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromScreenName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FromId_UserId` (`FromId`,`UserId`),
  KEY `FeedDate_UserId` (`FeedDate`,`UserId`),
  KEY `ProfileId_UserId` (`ProfileId`,`UserId`),
  KEY `ProfileId_UserId_MessageId` (`MessageId`,`ProfileId`,`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twittermessage
CREATE TABLE IF NOT EXISTS `twittermessage` (
  `Id` binary(16) NOT NULL,
  `TwitterMessage` text COLLATE utf8_unicode_ci,
  `MessageDate` datetime DEFAULT NULL,
  `MessageId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromName` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromProfileUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProfileId` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` binary(16) DEFAULT NULL,
  `Type` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `EntryDate` datetime DEFAULT NULL,
  `ScreenName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SourceUrl` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InReplyToStatusUserId` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FromScreenName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReadStatus` int(1) DEFAULT '0' COMMENT '0 means unread and 1 means read',
  `IsArchived` int(1) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId` (`MessageId`),
  KEY `FromId` (`FromId`),
  KEY `ProfileId` (`ProfileId`),
  KEY `ScreenName` (`ScreenName`),
  KEY `FromScreenName` (`FromScreenName`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.twitterstats
CREATE TABLE IF NOT EXISTS `twitterstats` (
  `ID` binary(16) NOT NULL,
  `TwitterId` varchar(100) DEFAULT NULL,
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
  `EntryDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserId` (`UserId`),
  KEY `TwitterId` (`TwitterId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.user
CREATE TABLE IF NOT EXISTS `user` (
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
  `ChangePasswordKey` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsKeyUsed` tinyint(1) DEFAULT '0',
  `PaymentStatus` varchar(6) COLLATE utf8_unicode_ci DEFAULT 'unpaid' COMMENT 'default value = unpaid',
  `ActivationStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0' COMMENT '0 = InActive,1 = Active',
  `CouponCode` varchar(350) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReferenceStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0',
  `RefereeStatus` varchar(5) COLLATE utf8_unicode_ci DEFAULT '0',
  `UserType` varchar(350) COLLATE utf8_unicode_ci DEFAULT 'user' COMMENT 'default value=user',
  `ChangeEmailKey` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsEmailKeyUsed` tinyint(4) DEFAULT '0',
  `Ewallet` varchar(50) COLLATE utf8_unicode_ci DEFAULT '0',
  `UserCode` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SocialLogin` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.useractivation
CREATE TABLE IF NOT EXISTS `useractivation` (
  `ID` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `ActivationStatus` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.usergroup
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


-- Dumping structure for table socioboard.userpackagerelation
CREATE TABLE IF NOT EXISTS `userpackagerelation` (
  `Id` binary(16) NOT NULL,
  `UserId` binary(16) NOT NULL,
  `PackageId` binary(16) NOT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `PackageStatus` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table socioboard.userrefrelation
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


-- Dumping structure for table socioboard.youtubeaccount
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


-- Dumping structure for table socioboard.youtubechannel
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


-- Dumping structure for table socioboard.youtubesubscription
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


-- Dumping structure for table Socioboard.shareathon
CREATE TABLE `shareathon` (
	`Id` BINARY(16) NOT NULL,
	`FacebookAccountId` BINARY(16) NOT NULL,
	`FacebookPageId` VARCHAR(500) NOT NULL,
	`TimeIntervalMinutes` INT(11) NOT NULL,
	`LastPostId` VARCHAR(50) NULL DEFAULT NULL,
	`LastShareTimeStamp` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`UserId` BINARY(16) NOT NULL,
	`IsHidden` BIT(1) NOT NULL DEFAULT b'1',
	PRIMARY KEY (`Id`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


-- Dumping structure for table Socioboard.shareathongroup
CREATE TABLE `shareathongroup` (
	`Id` BINARY(16) NOT NULL,
	`FacebookPageId` TEXT NOT NULL,
	`FacebookPageUrl` TEXT NOT NULL,
	`FacebookGroupId` TEXT NOT NULL,
	`FacebookNameId` TEXT NOT NULL,
	`FacebookAccountid` BINARY(16) NOT NULL,
	`AccessToken` TEXT NOT NULL,
	`TimeIntervalMinutes` INT(11) NOT NULL,
	`LastPostId` VARCHAR(50) NULL DEFAULT NULL,
	`LastShareTimeStamp` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`UserId` BINARY(16) NOT NULL,
	`IsHidden` BIT(1) NOT NULL,
	PRIMARY KEY (`Id`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
