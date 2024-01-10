using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Pagination
{
    public class SearchHelper<T> where T : class
    {
        public static IEnumerable<T> SearchForStringInProperties(IEnumerable<T> list, string searchString)
        {
            return from obj in list where FindStringInObjProperties(obj, searchString) select obj;
        }

        private static bool FindStringInObjProperties(object obj, string searchString)
        {
            return obj.GetType().GetProperties().Any(property => obj.GetType().GetProperty(property.Name).GetValue(obj) != null ?

            obj.GetType().GetProperty(property.Name).GetValue(obj, null).ToString().ToLower().Contains(searchString.ToLower()) : false);
        }

        public static IQueryable<T> SearchForStringInPropertiesAsQueryable(IQueryable<T> list, string searchString)
        {
            var dataFiltered = from obj in list.AsEnumerable() where FindStringInObjPropertiesAsQueryable(obj, searchString) select obj;
            return dataFiltered.AsQueryable();
        }

        private static bool FindStringInObjPropertiesAsQueryable(object obj, string searchString)
        {
            return obj.GetType().GetProperties().Any(property => obj.GetType().GetProperty(property.Name).GetValue(obj) != null ?

            obj.GetType().GetProperty(property.Name).GetValue(obj, null).ToString().ToLower().Contains(searchString.ToLower()) : false);
        }
    }

}
