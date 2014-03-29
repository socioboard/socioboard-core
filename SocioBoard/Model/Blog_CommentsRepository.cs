using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class Blog_CommentsRepository : IBlog_CommentsRepository
    {
        /// <GetAllBlog_Comments>
        /// Get AllBlog Comment.
        /// </summary>
        /// <param name="blog_Comments">The object of the Blog_Comments class(Domain.Blog_Comments)</param>
        /// <returns>Return Blog Comment from Blog_Comments in the form of ICollection Type.</returns>
        public ICollection<Blog_Comments> GetAllBlog_Comments(Blog_Comments blog_Comments)
        {
            ICollection<Blog_Comments> iCol = null;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //Proceed action, to get data.
                    iCol = session.CreateCriteria(typeof(Blog_Comments)).List<Blog_Comments>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return iCol;
        }


        /// <Insert>
        /// Add a Blog_Comment in a Database
        /// </summary>
        /// <param name="blog_Comments">The object of the Blog_Comments class(Domain.Blog_Comments)</param>
        /// <returns>Return Integer</returns>
        public int Insert(Blog_Comments blog_Comments)
        {
            int insert = 0;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to save data 
                        session.Save(blog_Comments);
                        transaction.Commit();
                        insert = 1;
                    }//End Using trasaction
                }//End using session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return insert;
        }

        
        /// <Update>
        /// Update All Fields of Blog_Comments by Id.
        /// </summary>
        /// <param name="blog_Comments">The object of the Blog_Comments class(Domain.Blog_Comments)</param>
        /// <returns>Integer where 0 is failed and 1 is success </returns>
        public int Update(Blog_Comments blog_Comments)
        {
            int update = 0;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action to update data.
                        //Return int (0 or 1)
                        session.Update(blog_Comments.Id, blog_Comments);
                        transaction.Commit();

                        update = 1;
                    }//End using Transaction
                }//End using session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return update;
        }

        
        /// <Delete>
        /// Delete a Blog_Comments from Database by Id.
        /// </summary>
        /// <param name="blog_Comments">The object of the Blog_Comments class(Domain.Blog_Comments)</param>
        /// <returns>Integer</returns>
        public int Delete(Blog_Comments blog_Comments)
        {
            int delete = 0;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to Delete
                        session.Delete(blog_Comments.Id);
                        transaction.Commit();
                        delete = 1;
                    }//End Trsaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return delete;
        }
        

        /// <summary>
        /// Not used Now.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Return all Comment By BlogId in the form of List type.</returns>
        public List<Blog_Comments> GetAllCommentByBlogId(string str)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction. 
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to get all data.
                        NHibernate.IQuery query = session.CreateQuery("from User");
                        List<Blog_Comments> alstUser = new List<Blog_Comments>();
                        foreach (Blog_Comments item in query.Enumerable())
                        {
                            alstUser.Add(item);
                        }

                        return alstUser;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return null;
                    }
                }//End Transaction
            }//End session
        }


        ///NOT USED NOW.
        /// <summary>
        /// Get all Blog_Comments from Database by Id.
        /// </summary>
        /// <param name="objBlog_Posts">The object of the Blog_Comments class(Domain.Blog_Comments)</param>
        /// <returns>Return all Comment By BlogId in the form of List type.</returns>
        public ICollection<Blog_Comments> GetAllCommentByBlogIda(Blog_Posts objBlog_Posts)
        {
            ICollection<Blog_Comments> iCol = null;
           // ICollection<Blog_Comments> iColById = null;
            try
            {
                 //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //Proceed action, to get all data                 
                    iCol = session.CreateCriteria(typeof(Blog_Comments)).List<Blog_Comments>()
                        .Where< Blog_Comments > (x => x.CommentPostId == objBlog_Posts.Id).ToList<Blog_Comments>();
                  //  iColById = iCol.Where<Blog_Comments>(x => x.CommentPostId == objBlog_Posts.Id).ToList<Blog_Comments>();
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return iCol;
        }//End Method 

    }
}