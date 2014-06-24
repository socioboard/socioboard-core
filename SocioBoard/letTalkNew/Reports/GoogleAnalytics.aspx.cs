using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace letTalkNew.Reports
{
    public partial class GoogleAnalytics : System.Web.UI.Page
    {
       public string siteVisitGraphArr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedUser"] == null)
                {
                    Response.Redirect("/Default.aspx");
                    return;
                }
                siteVisitGraphArr = GetSiteVisitGraph();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public string GetSiteVisitGraph()
        {
            string replyGraphData = string.Empty;
            try
            {
                //--------Graph Data Format-----------------

               //[
               // { x: new Date(2012, 00, 1), y: 450 },
               // { x: new Date(2012, 01, 1), y: 414 },
               // { x: new Date(2012, 02, 1), y: 520 },
               // { x: new Date(2012, 03, 1), y: 460 },
               // { x: new Date(2012, 04, 1), y: 450 },
               // { x: new Date(2012, 05, 1), y: 500 },
               // { x: new Date(2012, 06, 1), y: 480 },
               // { x: new Date(2012, 07, 1), y: 480 },
               // { x: new Date(2012, 08, 1), y: 410 },
               // { x: new Date(2012, 09, 1), y: 500 },
               // { x: new Date(2012, 10, 1), y: 480 },
               // { x: new Date(2012, 11, 1), y: 510 }

               // ]

                //User user = (User)Session["LoggedUser"];

                //TaskCommentRepository objTaskCommentRepository = new TaskCommentRepository();

                List<string> lstLastYear = null;//GetLastYears();

                int year = DateTime.Now.Year;

                //List<int> lstShareCount = objTaskCommentRepository.GetTaskCommentByUserIdAndYear(user.Id);

                int no=GetUniqueRandomNumber(400,600);

                string shareGarphArray = string.Empty;

                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        string month = "0";

                        if (i < 10)
                        {

                            month = month + i.ToString();
                        }
                        else
                        {
                            month = i.ToString();
                        }

                        if (i == 0)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 1)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 2)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 3)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 4)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 5)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 6)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 7)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 8)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 9)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 10)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            continue;
                        }
                        if (i == 11)
                        {
                            shareGarphArray += "{ x: new Date(" + year + ", " + month + ", 1), y: " + lstUniqueRecord[i] + " },";
                            
                            continue;
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                shareGarphArray = "[" + shareGarphArray.Substring(0, shareGarphArray.Length - 1) + "]";

                replyGraphData = shareGarphArray;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return replyGraphData;
        }

        List<int> lstUniqueRecord = new List<int>();
        public int GetUniqueRandomNumber(int min,int max)
        {
            int uniqueRandomNumber = 0;
            try
            {
                int j=1;

                for (int i=0; i<j ; i++)
                {
                    if (lstUniqueRecord.Count < 12)
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                    int ranNo = new Random().Next(400,600);

                    if (!lstUniqueRecord.Contains(ranNo))
                    {
                        lstUniqueRecord.Add(ranNo);

                        uniqueRandomNumber = ranNo;
                    }
                    else
                    {
                        //GetUniqueRandomNumber(min, max);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return uniqueRandomNumber;
        }
    }
}