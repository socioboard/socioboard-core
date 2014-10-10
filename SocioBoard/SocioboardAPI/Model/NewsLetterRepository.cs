using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Socioboard.Domain;
using Api.Socioboard.Helper;
using System.IO;

namespace Api.Socioboard.Model
{
    public class NewsLetterRepository : INewsLetterRepository
    {

        /// <AddNewsLetter>
        /// Add News Letter
        /// </AddNewsLetter>
        /// <param name="nl">Set Values in a NewsLetter Class Property and Pass the Object of NewsLetter Class.(Domein.NewsLetter)</param>
        public void AddNewsLetter(Domain.Socioboard.Domain.NewsLetter nl)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to save data.
                    session.Save(nl);
                    transaction.Commit();
                }//End Transaction
            }//End Session
        }


        //public string GetAllNewLetter()
        //{
            
        //    try
        //    {
        //        lstbox.Items.Add("Getting Mails....");
        //        string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\NewsLetterScheduler";
        //        string path = dirPath + "\\hibernate.cfg.xml";
        //        string startUpFilePath = Application.StartupPath + "\\hibernate.cfg.xml";
        //        Console.Write(dirPath + " " + path + " " + startUpFilePath);
        //        if (!Directory.Exists(dirPath))
        //        {
        //            Directory.CreateDirectory(dirPath);
        //        }

        //        if (!File.Exists(path))
        //        {
        //            File.Copy(startUpFilePath, path);
        //        }
        //        SessionFactory.configfilepath = path;
        //        NHibernate.ISession session = SessionFactory.GetNewSession();

        //        UserRepository objUserRepository = new UserRepository();
        //        User objUser = objUserRepository.getUsersById(Guid.Parse("62ebeaa8-0148-4e30-84fc-bf1af515dbf8"));
        //        List<User> lstuser = objUserRepository.getAllUsers();

        //        NewsLetter objNewsLetter = new NewsLetter();
        //        NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();
        //        //List<NewsLetter> lstNewsLetter = objNewsLetterRepository.GetAllNewsLetterByDate(DateTime.Now);
        //        //lstbox.Items.Add("Found " + lstNewsLetter.Count+" Mails");
        //        //foreach (NewsLetter item in lstNewsLetter)
        //        //{
        //        //    try
        //        //    {
        //        //        SendMail(item);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {

        //        //      Console.WriteLine("Error : " + ex.StackTrace);
        //        //    }

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error : " + ex.StackTrace);
        //        timer1.Start();
        //    }
        //    return "";
        //}

        /// <DeleteNewsLetter>
        /// Delete existing record of News Letter by news Latter id.
        /// </DeleteNewsLetter>
        /// <param name="nlid">News latter id (Guid)</param>
        /// <returns>Return integer 1 for true and 0 for false.(int)</returns>
        public int DeleteNewsLetter(Guid nlid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to delete a message.
                        NHibernate.IQuery query = session.CreateQuery("delete from NewsLetter where Id = :adsid")
                                        .SetParameter("adsid", nlid);
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


        /// <UpdateNewsLetter>
        /// Update News Letter
        /// </summary>
        /// <param name="nl">Set Values in a NewsLetter Class Property and Pass the Object of NewsLetter Class.(Domein.NewsLetter)</param>
        public void UpdateNewsLetter(Domain.Socioboard.Domain.NewsLetter nl)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to update existing news.
                      //  session.CreateQuery("Update NewsLetter set SendStatus =1 where UserId = :UserId,SendDate = :SendDate and Id= :adsid")
                      //  int i = session.CreateQuery("Update NewsLetter set SendStatus ='1' where Id = :id")
                      //      .SetParameter("id",nl.Id)
                      //     // .SetParameter("uid", nl.UserId)
                      //      //.SetParameter("SendDate", nl.SendDate)
                      //      //.SetParameter("nid", nl.Id)
                      //      .ExecuteUpdate();
                      //transaction.Commit();

                      //  List<NewsLetter> alst = session.CreateQuery("from NewsLetter where Id ="+nl.Id+ " ")
                      ////.SetParameter("id", nl.Id)
                      //.List<NewsLetter>()
                      //.ToList<NewsLetter>();




                        int i = session.CreateQuery("Update NewsLetter set SendStatus ='1' where Id = :id")
                                 .SetParameter("id",nl.UserId)
                                 .ExecuteUpdate();
                      transaction.Commit();
                     


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // return 0;
                    }
                }//End Transaction
            }//End Session
        }


        /// <getAllNewsLetter>
        /// Get all news letter
        /// </getAllNewsLetter>
        /// <returns>Return object of NewsLetter Class with  value of each member in the form of list.(List<NewsLetter>)</returns>
        public List<Domain.Socioboard.Domain.NewsLetter> getAllNewsLetter()
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    //Proceed action, to get all news latters.
                    List<Domain.Socioboard.Domain.NewsLetter> alstFBAccounts = session.CreateQuery("from NewsLetter").List<Domain.Socioboard.Domain.NewsLetter>().ToList<Domain.Socioboard.Domain.NewsLetter>();

                    //List<NewsLetter> alstFBAccounts = new List<NewsLetter>();

                    //foreach (NewsLetter item in query.Enumerable())
                    //{
                    //    alstFBAccounts.Add(item);
                    //}
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }


        /// <checkNewsLetterExists>
        /// Check news letter by news latter detail.  
        /// </summary>
        /// <param name="nldetail">Details(Message body) of news latter.(string)</param>
        /// <returns>True or False (Bool)</returns>
        public bool checkNewsLetterExists(string nldetail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get/find existing news latter
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where NewsLetterBody =:adsdetail");
                        query.SetParameter("adsdetail", nldetail);
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



        /// <checkNewsLetterExists>
        /// Check news letter exists
        /// </summary>
        /// <param name="nlid">Guid of news latter (Guid)</param>
        /// <returns>True or False (bool)</returns>
        public bool checkNewsLetterExists(Guid nlid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get news by id.
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where Id =:adsid");
                        query.SetParameter("adsid", nlid);
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


        /// <GetAllNewsLetterByDate>
        /// Get all news letter by date
        /// </summary>
        /// <param name="dt">Date and time (DateTime)</param>
        /// <returns>Return object of NewsLetter Class with  value of each member in the form of list.(List<NewsLetter>)</returns>
        public List<Domain.Socioboard.Domain.NewsLetter> GetAllNewsLetterByDate(DateTime dt)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<Domain.Socioboard.Domain.NewsLetter> alstFBAccounts = new List<Domain.Socioboard.Domain.NewsLetter>();
                    try
                    {
                        //Proceed action, to find news latter by date and time.
                        alstFBAccounts = session.CreateQuery("from NewsLetter where SendDate<= :dtime and SendStatus=0").SetParameter("dtime", dt)
                                      .List<Domain.Socioboard.Domain.NewsLetter>().ToList<Domain.Socioboard.Domain.NewsLetter>();

                        //List<NewsLetter> alstFBAccounts = new List<NewsLetter>();

                        //foreach (NewsLetter item in query.Enumerable())
                        //{
                        //    alstFBAccounts.Add(item);
                        //}

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.StackTrace);
                        //return true;
                    }
                    return alstFBAccounts;

                }//End Transaction
            }//End Session
        }



        /// <getNewsLetterDetailsbyId>
        /// Get news letter details by Id
        /// </summary>
        /// <param name="nlid">Id of news latter(Guid)</param>
        /// <returns>Return object of NewsLetter Class. (Domain.NewsLetter)</returns>
        public NewsLetter getNewsLetterDetailsbyId(Guid nlid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get news details by id.
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where Id=:adsid");

                        query.SetParameter("adsid", nlid);
                        NewsLetter grou = query.UniqueResult<NewsLetter>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }//End Transaction
            }//End Session
        }

        public Domain.Socioboard.Domain.NewsLetter getAllNewsLetterDetailsbyId(Guid nlid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get news details by id.
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where Id=:adsid");

                        query.SetParameter("adsid", nlid);
                        Domain.Socioboard.Domain.NewsLetter grou = (Domain.Socioboard.Domain.NewsLetter)query.UniqueResult();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }//End Transaction
            }//End Session
        }


        /// <getNewsLetterDetails>
        /// Get news letter details by news boy
        /// </summary>
        /// <param name="nlDetail">Message(Body) of new latter</param>
        /// <returns></returns>
        public Domain.Socioboard.Domain.NewsLetter getNewsLetterDetails(string nlDetail)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get news detail by new body.
                        NHibernate.IQuery query = session.CreateQuery("from NewsLetter where NewsLetterBody=:nlDetail");
                        query.SetParameter("nlDetail", nlDetail);
                        Domain.Socioboard.Domain.NewsLetter grou = query.UniqueResult<Domain.Socioboard.Domain.NewsLetter>();
                        return grou;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }


                }//End Transaction
            }//End Session
        }



        public List<Domain.Socioboard.Domain.NewsLetter> getAllUser()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.NewsLetter> alst = session.CreateQuery("from NewsLetter where  SendDate< now() and SendStatus=0")
                        .List<Domain.Socioboard.Domain.NewsLetter>()
                        .ToList<Domain.Socioboard.Domain.NewsLetter>();

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }

        public List<Domain.Socioboard.Domain.NewsLetter> getAllNewsLetters()
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        List<Domain.Socioboard.Domain.NewsLetter> alst = session.CreateQuery("from NewsLetter where  SendDate< now() and SendStatus=0")
                        .List<Domain.Socioboard.Domain.NewsLetter>()
                        .ToList<Domain.Socioboard.Domain.NewsLetter>();

                        return alst;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }
            }
        }



    }
}




