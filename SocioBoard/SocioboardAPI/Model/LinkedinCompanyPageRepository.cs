using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using System.Collections;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Model
{
    public class LinkedinCompanyPageRepository
    {
        public void addLinkenCompanyPage(Domain.Socioboard.Domain.LinkedinCompanyPage linkedincompanypage)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data 
                    session.Save(linkedincompanypage);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }

        public bool checkLinkedinPageExists(string lipageid, Guid Userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("from LinkedinCompanyPage where UserId = :Userid and LinkedinPageId = :lipageid");
                        query.SetParameter("Userid", Userid);
                        query.SetParameter("lipageid", lipageid);
                        var result = query.UniqueResult();

                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End Session
        }

        public void updateLinkedinPage(Domain.Socioboard.Domain.LinkedinCompanyPage linkedinPage)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.CreateQuery("Update LinkedinCompanyPage set LinkedinPageName =:LinkedinPageName,EmailDomains =:EmailDomains,OAuthToken =:OAuthToken,OAuthSecret=:OAuthSecret,OAuthVerifier=:OAuthVerifier,Description=:Description,FoundedYear=:FoundedYear,EndYear =:EndYear,Locations=:Locations,Specialties=:Specialties,WebsiteUrl=:WebsiteUrl,Status=:Status,EmployeeCountRange=:EmployeeCountRange,Industries=:Industries,CompanyType=:CompanyType,LogoUrl=:LogoUrl,SquareLogoUrl=:SquareLogoUrl,BlogRssUrl=:BlogRssUrl,UniversalName=:UniversalName,NumFollowers=:NumFollowers where LinkedinPageId = :linkedinpageid and UserId = :UserId")
                            .SetParameter("LinkedinPageName", linkedinPage.LinkedinPageName)
                            .SetParameter("EmailDomains", linkedinPage.EmailDomains)
                            .SetParameter("OAuthToken", linkedinPage.OAuthToken)
                            .SetParameter("OAuthSecret", linkedinPage.OAuthSecret)
                            .SetParameter("OAuthVerifier", linkedinPage.OAuthVerifier)
                            .SetParameter("Description", linkedinPage.Description)
                            .SetParameter("FoundedYear", linkedinPage.FoundedYear)
                            .SetParameter("EndYear", linkedinPage.EndYear)
                            .SetParameter("Locations", linkedinPage.Locations)
                            .SetParameter("Specialties", linkedinPage.Specialties)
                            .SetParameter("WebsiteUrl", linkedinPage.WebsiteUrl)
                            .SetParameter("Status", linkedinPage.Status)
                            .SetParameter("EmployeeCountRange", linkedinPage.EmployeeCountRange)
                            .SetParameter("Industries", linkedinPage.Industries)
                            .SetParameter("CompanyType", linkedinPage.CompanyType)
                            .SetParameter("LogoUrl", linkedinPage.LogoUrl)
                            .SetParameter("SquareLogoUrl", linkedinPage.SquareLogoUrl)
                            .SetParameter("BlogRssUrl", linkedinPage.BlogRssUrl)
                            .SetParameter("UniversalName", linkedinPage.UniversalName)
                            .SetParameter("NumFollowers", linkedinPage.NumFollowers)
                            .SetParameter("linkedinpageid", linkedinPage.LinkedinPageId)
                            .SetParameter("UserId", linkedinPage.UserId)
                            .ExecuteUpdate();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }
            }
        }

        public Domain.Socioboard.Domain.LinkedinCompanyPage getCompanyPageInformation(string liPageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of account.
                        List<Domain.Socioboard.Domain.LinkedinCompanyPage> objlst = session.CreateQuery("from LinkedinCompanyPage where LinkedinPageId = :linkedinpageid ")
                        .SetParameter("linkedinpageid", liPageid)
                        .List<Domain.Socioboard.Domain.LinkedinCompanyPage>().ToList<Domain.Socioboard.Domain.LinkedinCompanyPage>();
                        Domain.Socioboard.Domain.LinkedinCompanyPage result = new Domain.Socioboard.Domain.LinkedinCompanyPage();
                        if (objlst.Count > 0)
                        {
                            result = objlst[0];
                        }
                        return result;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.LinkedinCompanyPage getCompanyPageInformation(Guid userid, string lipageid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get details of account.
                        NHibernate.IQuery query = session.CreateQuery("from LinkedinCompanyPage where LinkedinPageId = :LinkedinPageId And UserId=:UserId");
                        query.SetParameter("UserId", userid);
                        query.SetParameter("LinkedinPageId", lipageid);
                        Domain.Socioboard.Domain.LinkedinCompanyPage result = query.UniqueResult<Domain.Socioboard.Domain.LinkedinCompanyPage>();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End Session
        }


        public int DeleteLinkedInPageByPageid(string Pageid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete linkedin account
                        NHibernate.IQuery query = session.CreateQuery("delete from LinkedinCompanyPage where LinkedinPageId = :pageid and UserId = :userid")
                                        .SetParameter("pageid", Pageid)
                                        .SetParameter("userid", userid);
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
            }//End Session
        }
    }
}   