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
        public ICollection<Blog_Comments> GetAllBlog_Comments(Blog_Comments blog_Comments)
        {
            ICollection<Blog_Comments> iCol = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    iCol = session.CreateCriteria(typeof(Blog_Comments)).List<Blog_Comments>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return iCol;
        }

        public int Insert(Blog_Comments blog_Comments)
        {
            int insert = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(blog_Comments);

                        transaction.Commit();
                        insert = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return insert;
        }

        public int Update(Blog_Comments blog_Comments)
        {
            int update = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(blog_Comments.Id, blog_Comments);
                        transaction.Commit();

                        update = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return update;
        }

        public int Delete(Blog_Comments blog_Comments)
        {
            int delete = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(blog_Comments.Id);
                        transaction.Commit();

                        delete = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return delete;
        }


        public List<Blog_Comments> GetAllCommentByBlogId(string str)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
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
                }
            }
        }


        public ICollection<Blog_Comments> GetAllCommentByBlogIda(Blog_Posts objBlog_Posts)
        {
            ICollection<Blog_Comments> iCol = null;
           // ICollection<Blog_Comments> iColById = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    iCol = session.CreateCriteria(typeof(Blog_Comments)).List<Blog_Comments>()
                        .Where< Blog_Comments > (x => x.CommentPostId == objBlog_Posts.Id).ToList<Blog_Comments>();
                  //  iColById = iCol.Where<Blog_Comments>(x => x.CommentPostId == objBlog_Posts.Id).ToList<Blog_Comments>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return iCol;
        }

    }
}