using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Helper;

namespace SocioBoard.Model
{
    public class NhibernateQueries
    {

        public List<Object> SelectQuery(string TableName, string[] parameters,string[] values)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    string hqlquery = string.Empty;

                    hqlquery = "from " + TableName + " where ";

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (i < parameters.Length-1 )
                        {
                            hqlquery += "" + parameters[i] + " = :" + parameters[i] + " and ";
                        }
                        else
                        {
                            hqlquery += "" + parameters[i] + " = :" + parameters[i];
                        }
                    }
                   
                    
                    NHibernate.IQuery query = session.CreateQuery(hqlquery);


                    for (int i = 0; i < parameters.Length; i++)
                    {
                        query.SetParameter("" + parameters[i] + "", values[i]);    
                    }
                    
                    List<Object> lstobj = new List<Object>();
                    foreach (Object item in query.Enumerable())
                    {
                        lstobj.Add(item);
                    }

                    return lstobj;
                }
            }
        
        }
    }
}