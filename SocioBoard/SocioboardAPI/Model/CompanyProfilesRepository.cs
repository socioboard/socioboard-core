using Api.Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Model
{
    public class CompanyProfilesRepository
    {
        public bool addAccount(Domain.Socioboard.Domain.CompanyProfiles compnayProfiles)
        {
            bool IsSuccess = false;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //proceed action, to save data.
                    session.Save(compnayProfiles);
                    transaction.Commit();
                    IsSuccess = true;
                }//End Transaction
            }//End session
            return IsSuccess;
        }


        public bool updateAccount(Domain.Socioboard.Domain.CompanyProfiles compnayProfiles)
        {
            bool IsUpdated = false;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        // Proceed action to Update Data.
                        // And Set the reuired paremeters to find the specific values.
                        session.CreateQuery("Update companyprofiles set CompanyName =:CompanyName,FbProfileId =:FbProfileId,TwitterProfileId =:TwitterProfileId,LinkedinProfileId=:LinkedinProfileId,InstagramProfileId=:InstagramProfileId,YoutubeProfileId=:YoutubeProfileId,GPlusProfileId=:GPlusProfileId,TumblrProfileId=:TumblrProfileId,UserId=:UserId where Id = :Id ")
                            .SetParameter("CompanyName", compnayProfiles.Companyname)
                            .SetParameter("FbProfileId", compnayProfiles.Fbprofileid)
                            .SetParameter("TwitterProfileId", compnayProfiles.Twitterprofileid)
                            .SetParameter("LinkedinProfileId", compnayProfiles.Linkedinprofileid)
                            .SetParameter("InstagramProfileId", compnayProfiles.Instagramprofileid)
                            .SetParameter("YoutubeProfileId", compnayProfiles.Youtubeprofileid)
                            .SetParameter("GPlusProfileId", compnayProfiles.Gplusprofileid)
                            .SetParameter("TumblrProfileId", compnayProfiles.Tumblrprofileid)
                            .SetParameter("UserId", compnayProfiles.Userid)
                            .SetParameter("Id", compnayProfiles.Id)
                            .ExecuteUpdate();
                        transaction.Commit();
                        IsUpdated = true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                    return IsUpdated;
                }//End Transaction
            }//End session
        }


        public Domain.Socioboard.Domain.CompanyProfiles getProfiles(string Name)
        {
            Domain.Socioboard.Domain.CompanyProfiles companyProfile = null;
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    NHibernate.IQuery query = session.CreateQuery("from companyprofiles where CompanyName = :CompanyName");
                    query.SetParameter("CompanyName", Name);
                    List<Domain.Socioboard.Domain.CompanyProfiles> result = (List<Domain.Socioboard.Domain.CompanyProfiles>)query.Enumerable();
                    companyProfile = result[0];
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
                    NHibernate.IQuery query = session.CreateQuery("from companyprofiles");

                    foreach (Domain.Socioboard.Domain.CompanyProfiles Profile in query.Enumerable())
                    {
                        companyNames.Add(Profile.Companyname);
                    }
                }//End Transaction
            }//End session

            return companyNames;
        }
    }
}