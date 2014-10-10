using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

namespace Socioboard.Helper
{
    public class UrlExtractor
    {

        public static string[] splitUrlFromString(string text)
        {
            try
            {

                //Regex.Replace(text, @"((www\.|(http|https|ftp)\://)[.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])",
                //            "<a href=\"$1\" target=\"&#95;blank\">$1</a>", RegexOptions.IgnoreCase);
              //  Regex.Replace(text, @"^((www\.|(http|https|ftp)\://)[.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])$",
                //            "<a href=\"$1\" target=\"&#95;blank\">$1</a>", RegexOptions.IgnoreCase)






                Regex regx = new Regex(@"^((www\.|(http|https|ftp)\://)[.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])$",
                                 RegexOptions.IgnoreCase);


                Regex reges = new Regex(@"((www\.|(http|https|ftp)\://)[.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])", RegexOptions.IgnoreCase);

                MatchCollection mss = reges.Matches(text.TrimStart('"').TrimEnd('"'));


                MatchCollection ms = regx.Matches(text.TrimStart('"').TrimEnd('"'));
                 string sd = string.Empty;
                 string[] st = new string[mss.Count + 2];
                 ArrayList aslt = new ArrayList();
                 int i = 0;
                 string fortesting = string.Empty;
                 if (mss.Count != 0)
                 {
                     foreach (Match item in mss)
                     {
                         aslt.Add(item.Value.ToString());
                         string[] stringseprators = new string[] { item.Value };

                         if (i == 0)
                         {
                             string[] sstr = text.Split(stringseprators, StringSplitOptions.None);
                             st[0] = sstr[0];
                             st[i + 1] = item.Value;
                             fortesting = sstr[1];
                         }
                         else
                         {
                             if (string.IsNullOrEmpty(st[i]))
                             {
                                 string[] ssstr = fortesting.Split(stringseprators, StringSplitOptions.None);
                                 st[i] = ssstr[0];
                                 st[i + 1] = item.Value;
                                 fortesting = ssstr[1];
                             }
                         }

                         i = i + 2;
                     }
                     st[i] = fortesting;
                 }
                 else
                 {
                     st[0] = text;
                 }
                return st;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}