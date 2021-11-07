using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models;


namespace C_bool.BLL.Logic
{
    public static class SearchUsers
    {
        /// <summary>
        /// Returns a sorted List of users with a specific range of scored points
        /// </summary>
        /// <param name="users">Input - list of User object</param>
        /// <param name="minPoints">Minimum points range</param>
        /// <param name="maxPoints">Maximum points range</param>
        /// <param name="orderByDescending">Sorting of list - if set to True, list will be sorted in descending order</param>
        /// <returns></returns>
        public static List<User> ByPointsRange(List<User> users, int minPoints, int maxPoints, bool orderByDescending)
        {
            var userList = users.Where(user => user.Points >= minPoints && user.Points <= maxPoints).ToList();
            var sortedList = orderByDescending
                ? userList.OrderByDescending(user => user.Points).ToList()
                : userList.OrderBy(user => user.Points).ToList();
            return sortedList;
        }
    }
}