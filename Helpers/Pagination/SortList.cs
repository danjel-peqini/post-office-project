using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Pagination
{
    public class SortList
    {
        public static IList<T> OrderByProperty<T>(IEnumerable<T> list, string propertyName)
        {
            return list.OrderBy(x => GetPropertyValue(x, propertyName)).ToList();
        }
        public static IList<T> OrderByDescendingProperty<T>(IEnumerable<T> list, string propertyName)
        {
            return list.OrderByDescending(x => GetPropertyValue(x, propertyName)).ToList();
        }
        public static IQueryable<T> OrderByPropertyAsQueryable<T>(IQueryable<T> list, string propertyName)
        {
            return list.AsEnumerable().OrderBy(x => GetPropertyValue(x, propertyName)).AsQueryable();
        }
        public static IQueryable<T> OrderByDescendingPropertyAsQueryable<T>(IQueryable<T> list, string propertyName)
        {
            return list.AsEnumerable().OrderByDescending(x => GetPropertyValue(x, propertyName)).AsQueryable();
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s,
                                              BindingFlags.Public
                                            | BindingFlags.Instance
                                            | BindingFlags.IgnoreCase)))
                {
                    obj = prop.GetValue(obj, null);
                }
                return obj;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
