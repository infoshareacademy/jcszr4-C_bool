using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.Places;

namespace C_bool.BLL.Logic
{
    public static class SearchNearbyPlaces
    {
        const double pi = Math.PI;
        const double earthRadius = 6371000;

        public static double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = earthRadius * d;

            return dist;
        }

        public static double Radians(double x)
        {
            return x * pi / 180;
        }

        public static List<Place> GetPlaces(double lat1, double lon1, double radius, List<Place> places)
        {
            List<Place> outputList = new List<Place>();

            foreach (var place in places)
            {
                var distance = DistanceBetweenPlaces(lat1, lon1, place.Geometry.Location.Latitude,
                    place.Geometry.Location.Longitude);
                if (distance <= radius)
                {
                    outputList.Add(place);
                }
            }
            return outputList;
        }
    }
}
