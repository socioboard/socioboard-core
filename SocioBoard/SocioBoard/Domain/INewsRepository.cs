using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    interface INewsRepository
    {
        void AddNews(News news);
        int DeleteNews(Guid newsid);
        void UpdateNews(News news);
        List<News> getAllNews();
        bool checkNewsExists(string newsname);
        News getNewsDetails(string newsname);
    }
}
