using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace SocioBoard.Model
{
    public class DataTableGenerator
    {
        public static DataSet CreateDataSetForTable(Object o)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                Type myType = o.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                foreach (PropertyInfo prop in props)
                {
                    dt.Columns.Add(prop.Name);
                }
                ds.Tables.Add(dt);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ds;

        }
    }
}