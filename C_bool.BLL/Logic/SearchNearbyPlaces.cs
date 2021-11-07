using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models.Places;

namespace C_bool.BLL.Logic
{
    public static class SearchNearbyPlaces
    {
        const double Pi = Math.PI;
        const double EarthRadius = 6371000;
        private static double Radians(double x) => x * Pi / 180;

        /// <summary>
        /// Returns approximate distance between two places on Earth, calculated from formula Distance, d = 3963.0 * arccos[(sin(lat1) * sin(lat2)) + cos(lat1) * cos(lat2) * cos(long2 – long1)]
        /// </summary>
        /// <param name="place1Latitude">First place latitude</param>
        /// <param name="place1Longitude">First place longitude</param>
        /// <param name="place2Latitude">Second place latitude</param>
        /// <param name="place2Longitude">First place longitude</param>
        /// <returns></returns>
        private static double DistanceBetweenPlaces(double place1Latitude, double place1Longitude,
            double place2Latitude, double place2Longitude)
        {
            var sinPlace1Latitude = Math.Sin(Radians(place1Latitude));
            var sinPlace2Latitude = Math.Sin(Radians(place2Latitude));
            var cosPlace1Latitude = Math.Cos(Radians(place1Latitude));
            var cosPlace2Latitude = Math.Cos(Radians(place2Latitude));

            var distance = EarthRadius * Math.Acos(sinPlace1Latitude * sinPlace2Latitude + cosPlace1Latitude *
                cosPlace2Latitude * Math.Cos(Radians(place1Longitude) - Radians(place2Longitude)));

            return distance;
        }

        /// <summary>
        /// Returns approximate distance between two places on Earth, calculated from formula Distance, d = 3963.0 * arccos[(sin(lat1) * sin(lat2)) + cos(lat1) * cos(lat2) * cos(long2 – long1)]
        /// </summary>
        /// <param name="firstPlace">First Place object</param>
        /// <param name="secondPlace">Second Place object</param>
        /// <returns></returns>
        private static double DistanceBetweenPlaces(Place firstPlace, Place secondPlace) =>
            DistanceBetweenPlaces(
                firstPlace.Geometry.Location.Latitude,
                firstPlace.Geometry.Location.Longitude,
                secondPlace.Geometry.Location.Latitude,
                secondPlace.Geometry.Location.Longitude
            );

        /// <summary>
        /// gets places nearby according to entered coordinates and radius
        /// </summary>
        /// <param name="latitude">Latitude of the place from which to search for nearby locations</param>
        /// <param name="longitude">Longitude of the place from which to search for nearby locations</param>
        /// <param name="radius">Radius in which to search, entered as meters</param>
        /// <param name="places">Input - list of Place object</param>
        /// <returns>List of places nearby</returns>
        public static List<Place> GetPlaces(List<Place> places, double latitude, double longitude, double radius)
        {
            return places.Where(p =>
                    DistanceBetweenPlaces(
                        latitude,
                        longitude,
                        p.Geometry.Location.Latitude,
                        p.Geometry.Location.Latitude
                    ) <= radius)
                .ToList();
        }

        /// <summary>
        /// gets places nearby according to entered coordinates and radius
        /// </summary>
        /// <param name="fromPlace">Place object from which to search for nearby locations</param>
        /// <param name="radius">Radius in which to search, entered as meters</param>
        /// <param name="places">Input - list of Place object</param>
        /// <returns>List of places nearby</returns>
        public static List<Place> GetPlaces(List<Place> places, Place fromPlace, double radius)
        {
            return places.Where(p =>
                    DistanceBetweenPlaces(
                        fromPlace.Geometry.Location.Latitude,
                        fromPlace.Geometry.Location.Longitude,
                        p.Geometry.Location.Latitude,
                        p.Geometry.Location.Longitude
                    ) <= radius)
                .ToList();
        }
    }
}