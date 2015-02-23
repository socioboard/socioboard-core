using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Model
{
    public class CompanySocialProfilesRepository
    {
        public CompanySocialProfilesRepository() 
        {
            //this.RegistrationDate = DateTime.UtcNow;
        }


        #region
        //public facebookpageinfo getfbpage() 
        //{
        //    facebookpageinfo fbpginfo = null;
        //    if (this.facebookpageinfoID == null)
        //    {
        //        return fbpginfo;
        //    }
        //    else 
        //    {
        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<facebookpageinfo> alst = session.CreateQuery("from facebookpageinfo where Id = :fbpgid")
        //                .SetParameter("fbpgid", facebookpageinfoID)
        //                .List<facebookpageinfo>()
        //                .ToList<facebookpageinfo>();
        //                    fbpginfo = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session


        //    }
        //    return fbpginfo;
        //}
        //public googleplusinfo getgpulsinfo() 
        //{
        //    googleplusinfo gpluspginfo = null;
        //    if (this.googleplusinfoID== null)
        //    {
        //        return gpluspginfo;
        //    }
        //    else
        //    {

        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<googleplusinfo> alst = session.CreateQuery("from googleplusinfo where Id = :gpluspgid")
        //                .SetParameter("gpluspgid", this.googleplusinfoID)
        //                .List<googleplusinfo>()
        //                .ToList<googleplusinfo>();
        //                    gpluspginfo = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session


        //    }
        //    return gpluspginfo;
        //}
        //public instagrampage getinstagraminfo()
        //{
        //    instagrampage instpg = null;
        //    if (this.instagrampageID == null)
        //    {
        //        return instpg;
        //    }
        //    else
        //    {

        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<instagrampage> alst = session.CreateQuery("from instagrampage where Id = :instpgid")
        //                .SetParameter("instpg", this.instagrampageID)
        //                .List<instagrampage>()
        //                .ToList<instagrampage>();
        //                    instpg = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session
        //    }
        //    return instpg;

        //}
        //public linkedinpage getlinkedinpageinfo() 
        //{
        //    linkedinpage linkpg = null;
        //    if (this.linkedinpageID == null)
        //    {
        //        return linkpg;
        //    }
        //    else
        //    {

        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<linkedinpage> alst = session.CreateQuery("from linkedinpage where Id = :linkpgid")
        //                .SetParameter("linkpgid", this.linkedinpageID)
        //                .List<linkedinpage>()
        //                .ToList<linkedinpage>();
        //                    linkpg = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session
        //    }
        //    return linkpg;
        //}
        //public twitterpage gettwitterpageinfo()
        //{
        //    twitterpage twitterpg = null;
        //    if (this.twitterpageID == null)
        //    {
        //        return twitterpg;
        //    }
        //    else
        //    {

        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<twitterpage> alst = session.CreateQuery("from twitterpage where Id = :twitterpgid")
        //                .SetParameter("twitterpgid", this.twitterpageID)
        //                .List<twitterpage>()
        //                .ToList<twitterpage>();
        //                    twitterpg = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session
        //    }
        //    return twitterpg;
        //}
        //public youtubepage getyoutubepageinfo()
        //{
        //    youtubepage ytpg = null;
        //    if (this.twitterpageID == null)
        //    {
        //        return ytpg;
        //    }
        //    else
        //    {

        //        using (NHibernate.ISession session = SessionFactory.GetNewSession())
        //        {
        //            //Begin session trasaction and opens up.
        //            using (NHibernate.ITransaction transaction = session.BeginTransaction())
        //            {
        //                try
        //                {
        //                    List<youtubepage> alst = session.CreateQuery("from youtubepage where Id = :ytpgid")
        //                .SetParameter("ytpg", this.twitterpageID)
        //                .List<youtubepage>()
        //                .ToList<youtubepage>();
        //                    ytpg = alst[0];
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.StackTrace);
        //                    //return 0;
        //                }

        //            }//End Transaction  
        //        }//End Session
        //    }
        //    return ytpg;
        //}

        //public facebookpageinfo setfbpage()
        //{
        //    facebookpageinfo fbpginfo = null;
        //    return fbpginfo;
        //}
        //public googleplusinfo setgpulsinfo()
        //{
        //    googleplusinfo gplusinfo = null;
        //    return gplusinfo;
        //}
        //public instagrampage setinstagraminfo()
        //{
        //    instagrampage instpg = null;
        //    return instpg;
        //}
        //public linkedinpage setlinkedinpageinfo()
        //{
        //    linkedinpage linkpg = null;
        //    return linkpg;
        //}
        //public twitterpage settwitterpageinfo()
        //{
        //    twitterpage twitterpg = null;
        //    return twitterpg;
        //}
        //public youtubepage setyoutubepageinfo()
        //{
        //    youtubepage ytpg = null;
        //    return ytpg;
        //}
        #endregion
        public bool AddcompanyProfileName(Domain.Socioboard.Domain.CompanyProfiles companyProfiles)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Save data.
                    try
                    {
                        session.Save(companyProfiles);
                        transaction.Commit();
                        IsSuccess = true;
                        return IsSuccess;
                    }
                    catch (Exception ex)
                    {
                                                                 
                        Console.WriteLine(ex.StackTrace);
                        return IsSuccess;
                    }
                    
                }//End Transaction
            }//End Session
        }
        public int deletecompanyProfileName(Guid id)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a FacebookAccount by FbUserId and UserId.
                        NHibernate.IQuery query = session.CreateQuery("delete from CompanyProfiles where Id = :id")
                                                 .SetParameter("id", id);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }//End Transaction
            }//End session
        }
        public bool updatecompanyProfileName(Domain.Socioboard.Domain.CompanyProfiles companyProfiles)
        {
            bool isUpdate = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update companyprofiles set CompanyName=:companyName,FbProfileId=:fbPfofileId,LinkediProfileId=:linkedinPfofileId,GPlusProfileId=:gplusProfileId,InstagramProfileId=:instagramProfileId,TwitterProfileId=:twitterProfileId,TumblrProfileId=:tumblrProfileId,YoutubeProfileId=:youtubeProfileId where Id = :id and UserId = :userid")
                            .SetParameter("companyName", companyProfiles.Companyname)
                            .SetParameter("id", companyProfiles.Id)
                            .SetParameter("fbPfofileId", companyProfiles.Fbprofileid)
                            .SetParameter("gplusProfileId", companyProfiles.Gplusprofileid)
                            .SetParameter("userid", companyProfiles.Userid)
                            .SetParameter("instagramProfileId", companyProfiles.Instagramprofileid)
                            .SetParameter("linkedinPfofileId", companyProfiles.Linkedinprofileid)
                            .SetParameter("twitterPfofileId", companyProfiles.Twitterprofileid)
                            .SetParameter("tumblrProfileId", companyProfiles.Tumblrprofileid)
                            .SetParameter("youtubeProfileId", companyProfiles.Youtubeprofileid)
                            .ExecuteUpdate();
                        transaction.Commit();
                        isUpdate = true;
                        return isUpdate;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return isUpdate;
                    }
                }//End Transaction
            }//End session
        }
        public Domain.Socioboard.Domain.CompanyProfiles getCompanyProfiles(Guid  Id)
        {
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from CompanyProfiles where Id = :ID");
                        query.SetParameter("ID", Id);
                        Domain.Socioboard.Domain.CompanyProfiles result = (Domain.Socioboard.Domain.CompanyProfiles)query.UniqueResult();
                        companyProfile = result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }//End Transaction
            }//End session

            return companyProfile;
        }
        public List<string> getCompanyNames()
        {
            List<string> companyNames = new List<string>();

            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to Get all FacebookAccount.
                    NHibernate.IQuery query = session.CreateQuery("from CompanyProfiles");

                    foreach (Domain.Socioboard.Domain.CompanyProfiles Profile in query.Enumerable())
                    {
                        companyNames.Add(Profile.Companyname);
                    }
                }//End Transaction
            }//End session

            return companyNames;
        }
        public Domain.Socioboard.Domain.CompanyProfiles SearchCompanyName(string Keywords)
        {
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.CompanyProfiles> result = session.CreateQuery("from CompanyProfiles where Companyname=:CompanyName").SetParameter("CompanyName", Keywords).List<Domain.Socioboard.Domain.CompanyProfiles>().ToList<Domain.Socioboard.Domain.CompanyProfiles>();
                    try
                    {
                        companyProfile = result[0];
                    }
                    catch (Exception e) { }
                }//End Transaction
            }//End session

            return companyProfile;
        }
    }

}