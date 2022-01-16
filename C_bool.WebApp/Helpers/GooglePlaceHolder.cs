using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.GooglePlaces;

namespace C_bool.WebApp.Helpers
{
    public class GooglePlaceHolder
    {
        public static Dictionary<int, List<GooglePlace>> _tempPlaces = new();

        public static void CreateNewOrUpdateExisting<TKey, TValue>(IDictionary<TKey, TValue> map, TKey key, TValue value)
        {
            map[key] = value;
        }
    }
}
