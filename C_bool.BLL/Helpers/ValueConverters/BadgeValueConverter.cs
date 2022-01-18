using System;
using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C_bool.BLL.Helpers
{
    class BadgeValueConverter : ValueConverter<List<Badges>, string>
    {
        public BadgeValueConverter()
            : base((src) => To(src), dest => From(dest)) { }

        private static string To(List<Badges> src)
        {
            if (src is null || src.Count == 0)
            {
                return null;
            }
            return string.Join(";", src);
        }
        private static List<Badges> From(string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return null;
            }

            var badgesStr =  src.Split(";");
            List<Badges> badgesList = new();

            foreach (var badge in badgesStr)
            {
                Badges.TryParse(badge, out Badges badgeEnum);
                badgesList.Add(badgeEnum);
            }

            return badgesList;
        }
    }
}