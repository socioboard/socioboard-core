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

        /// <GetAllBlogPosts>
        /// Get all BlogPosts
        /// </summary>
        /// <param name="blog_Posts"></param>
        /// <returns>Icollection of Blog post data objects </returns>
        public ICollection<Blog_Posts> GetAllBlogPosts(Blog_Posts blog_Posts)
        {
            ICollection<Blog_Posts> iCol = null;
            try
            {
                //Creates a database connection and opens up a session
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //Proceed action, to to get all blog posts
                    iCol = session.CreateCriteria(typeof(Blog_Posts)).List<Blog_Posts>();

                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : "+ex.StackTrace);

            }
            return iCol;
        } // End Method


        /// <Insert>
        /// Insert new Data of blog posts
        /// </summary>
        /// <param name="blog_Posts">Set Values in a blog posts Class Property and Pass the same Object of blog posts Class.(Domain.Blog_Posts)</param>
        /// <returns>When it is successfully inserte, it is return 1 or failed its is return 0</returns>
        public int Insert(Blog_Posts blog_Posts)
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
                        session.Save(blog_Posts);
                        transaction.Commit();
                        insert = 1;

                    }//End Transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return insert;
        }


        /// <Update>
        /// Update/Change value of blog post
        /// </summary>
        /// <param name="blog_Posts">Set Values in a blog posts Class Property and Pass the same Object of blog posts Class in paremeter.(Domain.Blog_Posts)</param>
        /// <returns></returns>
        public int Update(Blog_Posts blog_Posts)
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
                        //Proceed action, to update Data
                        session.Update(blog_Posts.Id, blog_Posts);
                        transaction.Commit();

                        update = 1;
                    }//End Trsaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return update;
        }


        /// <Delete>
        /// Delete Blog post data by blog post id.
        /// </summary>
        /// <param name="blog_Posts">Set the blog post id in a blog posts Class Property and Pass the same Object of blog posts Class.(Domain.Blog_Posts)</param>
        /// <returns>Its is return integer value. when action is successfully completed it is returns 1 , otherwise its return 0.</returns>
        public int Delete(Blog_Posts blog_Posts)
        {
            int delete = 0;
            try
            {
                //Creates a database connection and opens up a session.
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {
                    //After Session creation, start Transaction. 
                    using (NHibernate.ITransaction transaction = session.BeginTransaction())
                    {
                        //Proceed action, to delete specific data.
                        session.Delete(blog_Posts.Id);
                        transaction.Commit();

                        delete = 1;
                    }//End Transaction
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }

            return delete;
        }


        /// <GetBlogId>
        /// Get all blog Data from blog post id.
        /// </summary>
        /// <param name="objBlog_Posts">Set the blog post id in a blog posts Class Property and Pass the same Object of blog posts Class.(Domain.Blog_Posts)</param>
        /// <returns>Its is returns Icollection of blog post property class objects.(ICollection<Blog_Posts>)</returns>
        public ICollection<Blog_Posts> GetBlogId(Blog_Posts objBlog_Posts)
        {
            ICollection<Blog_Posts> iCol = null;
            // ICollection<Blog_Comments> iColById = null;
            try
            {
                //Creates a database connection and opens up a session.
                using (NHibernate.ISession session = SessionFactory.GetNewSession())
                {

                    //Proceed action, to get all blog data by blog id.
                    iCol = session.CreateCriteria(typeof(Blog_Posts)).List<Blog_Posts>()
                        .Where<Blog_Posts>(x => x.Id == objBlog_Posts.Id).ToList<Blog_Posts>();
                    //  iColById = iCol.Where<Blog_Comments>(x => x.CommentPostId == objBlog_Posts.Id).ToList<Blog_Comments>();
                }//End session
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return iCol;
        }

    }
}