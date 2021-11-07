using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.Places;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C_bool.BLL.Repositories
{
    public sealed class PlacesRepository : BaseRepository<Place>, IRepository<Place>
    {
        public override List<Place> Repository { get; protected set; }
        public override string FileName { get; } = "places.json";

        public PlacesRepository()
        {
            Repository = new List<Place>();
        }

        public List<Place> SearchByName(string searchName)
        {
            return (from place in Repository
                where place.Name.ToLower().Contains(searchName.ToLower())
                select place).ToList();
        }

        public List<Place> GetNearbyPlacesFromRadius(double radius) =>
            SearchNearbyPlaces.GetPlaces(Repository, Repository.FirstOrDefault(), radius);

        private string TrimJson(string convertedJson, string sectionToGet)
        {
            try
            {
                return JObject.Parse(convertedJson)[sectionToGet].ToString();
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }

        protected override string ConvertFileJsonToString() => TrimJson(base.ConvertFileJsonToString(), "results");

        public void AddApiDataToRepository(double latitude, double longitude, double radius, string apiKey)
        {
            try
            {
                var webRequest =
                    WebRequest.Create(
                        @$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude},{longitude}&radius={radius}&key={apiKey}");
                var trimmedJson = TrimJson(ConvertApiJsonToString(webRequest), "results");

                Repository = JsonConvert.DeserializeObject<List<Place>>(trimmedJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}