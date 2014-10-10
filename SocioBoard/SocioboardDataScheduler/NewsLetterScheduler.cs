using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioboardDataScheduler
{
    class NewsLetterScheduler
    {
        public static string PostNewsLetter()
        {
            string str = string.Empty;
            try
            {
                Api.NewsLetter.NewsLetter ApiObjNewsLetter = new Api.NewsLetter.NewsLetter();
                str = ApiObjNewsLetter.GetAllNewsLetters();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return str;
        }
    }
}
