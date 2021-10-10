using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models.Users;

namespace C_bool.BLL.Logic
{
    public static class SearchUsers
    {
        /// <summary>
        /// Returns a sorted List of users with a specific range of scored points
        /// </summary>
        /// <param name="minPoints">Minimum points range</param>
        /// <param name="maxPoints">Maximum points range</param>
        /// <param name="orderByDescending">Sorting of list - if set to True, list will be sorted in descending order</param>
        /// <param name="users">Input - list of User object</param>
        /// <returns></returns>
        public static List<User> ByPointsCount(int minPoints, int maxPoints, bool orderByDescending, List<User> users)
        {
            var userList = (from user in users where (user.Points >= minPoints && user.Points <= maxPoints) select user).ToList();
            var sortedList = orderByDescending ? userList.OrderByDescending(user => user.Points).ToList() : userList.OrderBy(user => user.Points).ToList();
            return sortedList;
        }
    }
}
