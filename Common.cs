using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebShop
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = dr[textField].ToString(),
                    Value = dr[textField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }

        public class ListToDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dt = new DataTable(typeof(T).Name);
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach(PropertyInfo prop in properties) 
                {
                    dt.Columns.Add(prop.Name);
                }
                foreach(T item in items)
                {
                    var values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(item, null);
                    }
                    dt.Rows.Add(values);
                }
                return dt;

            }
        }
        public class ProductType
        {
            public int id { get; set; }
            public string Name { get; set; }
        }
    }
}