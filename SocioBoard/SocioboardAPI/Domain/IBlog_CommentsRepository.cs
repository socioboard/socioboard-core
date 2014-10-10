using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Socioboard.Domain
{
    interface IBlog_CommentsRepository
    {
        ICollection<Blog_Comments> GetAllBlog_Comments(Blog_Comments blog_Comments);
        int Insert(Blog_Comments blog_Comments);
        int Update(Blog_Comments blog_Comments);
        int Delete(Blog_Comments blog_Comments);
    }
}
