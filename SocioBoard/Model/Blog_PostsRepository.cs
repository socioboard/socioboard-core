using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class Blog_PostsRepository : IBlog_PostsRepository
    {
        public ICollection<Blog_Posts> GetAllBlogPosts(Blog_Posts blog_Posts)
        {
            ICollection<Blog_Posts> iCol = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    iCol = session.CreateCriteria(typeof(Blog_Posts)).List<Blog_Posts>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : "+ex.StackTrace);

            }
            return iCol;
        }

        public int Insert(Blog_Posts blog_Posts)
        {
            int insert = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(blog_Posts);

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

        public int Update(Blog_Posts blog_Posts)
        {
            int update = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(blog_Posts.Id, blog_Posts);
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

        public int Delete(Blog_Posts blog_Posts)
        {
            int delete = 0;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(blog_Posts.Id);
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


        public ICollection<Blog_Posts> GetBlogId(Blog_Posts objBlog_Posts)
        {
            ICollection<Blog_Posts> iCol = null;
            // ICollection<Blog_Comments> iColById = null;
            try
            {
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    iCol = session.CreateCriteria(typeof(Blog_Posts)).List<Blog_Posts>()
                        .Where<Blog_Posts>(x => x.Id == objBlog_Posts.Id).ToList<Blog_Posts>();
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