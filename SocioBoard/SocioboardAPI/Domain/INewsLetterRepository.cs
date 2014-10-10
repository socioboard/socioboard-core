using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Socioboard.Domain
{
    public interface INewsLetterRepository
    {
        void AddNewsLetter(NewsLetter nl);
        int DeleteNewsLetter(Guid nlid);
        void UpdateNewsLetter(NewsLetter nl);
        List<NewsLetter> getAllNewsLetter();
        bool checkNewsLetterExists(string nlDetail);
        NewsLetter getNewsLetterDetails(string nlDetail);
    }
}
