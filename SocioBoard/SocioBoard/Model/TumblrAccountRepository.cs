using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class TumblrAccountRepository
    {
        public static void Add(SocioBoard.Domain.TumblrAccount tumbler)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start and open Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action to save data.
                    session.Save(tumbler);
                    transaction.Commit();

                }//End Using trasaction
            }//End Using session
        }

        public bool checkTubmlrUserExists(TumblrAccount objTumblrAccount)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to Check if FacebookUser is Exist in database or not by UserId and FbuserId.
                        // And Set the reuired paremeters to find the specific values.
                        NHibernate.IQuery query = session.CreateQuery("from TumblrAccount where UserId = :uidd and tblrUserName = :tbuname");
                        query.SetParameter("uidd",objTumblrAccount.UserId);
                        query.SetParameter("tbuname", objTumblrAccount.tblrUserName);
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
            }//End session
        }



        public TumblrAccount getTumblrAccountDetailsById(string tumblruserid, Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    NHibernate.IQuery query = session.CreateQuery("from TumblrAccount where tblrUserName = :tblruname and UserId=:userId");
                    query.SetParameter("tblruname", tumblruserid);
                    query.SetParameter("userId", userId);
                    TumblrAccount result = (TumblrAccount)query.UniqueResult();
                    return result;
                }//End Transaction
            }//End session
        }

        public TumblrAccount getTumblrAccountDetailsById(string tumblruserid)
        {
           
            TumblrAccount result = new TumblrAccount();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // proceed action, to get all Facebook Account of User by UserId(Guid) and FbUserId(string).
                    List<TumblrAccount> objlsttumb = session.CreateQuery("from TumblrAccount where tblrUserName = :tblruname")
                            .SetParameter("tblruname", tumblruserid)
                       .List<TumblrAccount>().ToList<TumblrAccount>();
                    if (objlsttumb.Count > 0)
                    {
                        result = objlsttumb[0];
                    }
                    return result;
                }//End Transaction
            }//End s


        }




        public List<TumblrAccount> getTumblrAccountDetailsById(Guid userId)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, open up a Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all twitter messages of profile by profile id
                        // And order by Entry date.
                        List<TumblrAccount> lstmsg = session.CreateQuery("from TumblrAccount where UserId=:userId")
                        .SetParameter("userId", userId)
                        .List<TumblrAccount>()
                        .ToList<TumblrAccount>();
                        return lstmsg;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Transaction
            }//End Session
        }





        public int deleteTumblrUser(string Tumblruserid, Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a TumblrAccount by FbUserId and UserId.
                        NHibernate.IQuery query = session.CreateQuery("delete from TumblrAccount where tblrUserName = :tumblruname and UserId = :userid")
                                        .SetParameter("tumblruname", Tumblruserid)
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
            }//End session
        }



        public List<TumblrAccount> getAllAccountDetail(string profileid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //Begin session trasaction and opens up.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        string str = "from TumblrAccount where  tblrUserName IN(";
                        string[] arrsrt = profileid.Split(',');
                        foreach (string sstr in arrsrt)
                        {
                            str +="'"+(sstr)+"'" + ",";
                        }
                        str = str.Substring(0, str.Length - 1);
                        str += ")group by tblrUserName";
                        List<TumblrAccount> alst = session.CreateQuery(str)
                       .List<TumblrAccount>()
                       .ToList<TumblrAccount>();
                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }

                }//End Trasaction
            }//End session
        }



    }
}