using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.WebApp.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C_bool.WebApp.Services
{
    public class GoogleApiAsync
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string ApiKey { get; set; }
        public List<Place> Places { get; set; }
        public string Message { get; set; }
        public Status QueryStatus { get; set; }

        public GoogleApiAsync(IHttpClientFactory httpClientFactory, string apiKey)
        {
            _httpClientFactory = httpClientFactory;
            ApiKey = apiKey;
        }

        private string TrimJson(string convertedJson, string sectionToGet)
        {
            try
            {
                return JObject.Parse(convertedJson)[sectionToGet]?.ToString();
            }
            catch (Exception ex)
            {
                Message = $"Wystąpił błąd: {ex.Message}";
                QueryStatus = Status.INNER_METHOD_ERROR;
                return null;
            }
        }

        async Task<HttpResponseMessage> GetResponse(string requestUri)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("GoogleMapsClient");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                return response;
            }
            catch (Exception ex)
            {
                Message = $"Wystąpił błąd: {ex.Message}";
                QueryStatus = Status.INNER_METHOD_ERROR;
                return null;
            }
        }

        public async Task<List<Place>> GetBySearchQuery(string query = "", string language = "pl")
        {
            var listPlaces = new List<Place>();
            var requestUri = @$"/maps/api/place/textsearch/json?query={query}&language={language}&key={ApiKey}";
            try
            {
                var response = await GetResponse(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Enum.TryParse(TrimJson(responseContent, "status"), out Status status);
                    if (status != Status.OK)
                    {
                        Message = $"GoogleAPI - zapytanie zwróciło informację: {status}";
                        QueryStatus = status;
                        return listPlaces;
                    }
                    listPlaces.AddRange(GetPlacesFromResponseContent(responseContent, out string nextPageToken));
                }

            }
            catch (Exception ex)
            {
                Message = $"Wystąpił błąd: {ex.Message}";
                QueryStatus = Status.INNER_METHOD_ERROR;
                return listPlaces;
            }
            Message = $"Znaleziono {listPlaces.Count} miejsc w okolicy dla zapytania {query}";
            return listPlaces;
        }

        public async Task<List<Place>> GetNearby(string latitude,
        string longitude,
        double radius,
        string type = "",
        string keyword = "",
        string region = "PL",
        string language = "pl",
        bool loadAllPages = false)
        {
            var latitudeString = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeString = longitude.ToString(CultureInfo.InvariantCulture);
            var listPlaces = new List<Place>();
            var requestUri = @$"/maps/api/place/nearbysearch/json?location={latitudeString},{longitudeString}&radius={radius}&type={type}&keyword={keyword}&region={region}&language={language}&key={ApiKey}";

            try
            {
                var response = await GetResponse(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Enum.TryParse(TrimJson(responseContent, "status"), out Status status);
                    if (status != Status.OK)
                    {
                        Message = $"GoogleAPI - zapytanie zwróciło informację: {status}";
                        QueryStatus = status;
                        return listPlaces;
                    }

                    listPlaces.AddRange(GetPlacesFromResponseContent(responseContent, out string nextPageToken));

                    while (!string.IsNullOrEmpty(nextPageToken) && loadAllPages)
                    {
                        await Task.Delay(500);
                        requestUri = $"/maps/api/place/nearbysearch/json?pagetoken={nextPageToken}&key={ApiKey}";
                        response = await GetResponse(requestUri);
                        responseContent = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            Enum.TryParse(TrimJson(responseContent, "status"), out status);
                            if (status != Status.OK)
                            {
                                Message = $"Znaleziono {listPlaces.Count} miejsc w okolicy dla zapytania {keyword} {type}";
                                return listPlaces;
                            }
                            listPlaces.AddRange(GetPlacesFromResponseContent(responseContent, out nextPageToken));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"Wystąpił błąd: {ex.Message}";
                QueryStatus = Status.INNER_METHOD_ERROR;
                return listPlaces;
            }

            Message = $"Znaleziono {listPlaces.Count} miejsc w okolicy dla zapytania {keyword} {type}";
            return listPlaces;
        }

        private List<Place> GetPlacesFromResponseContent(string responseContent, out string nextPageToken)
        {

            var listPlaces = new List<Place>();
            try
            {
                Enum.TryParse(TrimJson(responseContent, "status"), out Status queryStatus);
                var trimmedJson = TrimJson(responseContent, "results");
                listPlaces.AddRange(JsonConvert.DeserializeObject<List<Place>>(trimmedJson));
                nextPageToken = TrimJson(responseContent, "next_page_token");
                QueryStatus = queryStatus;
            }
            catch (Exception e)
            {
                Message = e.Message;
                QueryStatus = Status.INNER_METHOD_ERROR;
                nextPageToken = String.Empty;
                return listPlaces;
            }
            return listPlaces;
        }


        public async Task<PhotoBase64> DownloadImageAsync(Place place, string width)
        {
            using var httpClient = new HttpClient();
            var requestUri =
                @$"https://maps.googleapis.com/maps/api/place/photo?maxwidth={width}&photo_reference={place.GooglePhotos[0].PhotoReference}&key={ApiKey}";
            Uri uri = new Uri(requestUri);
            PhotoBase64 photo = new();
            var imageBytes = await httpClient.GetByteArrayAsync(uri);
            photo.Data = Convert.ToBase64String(imageBytes);

            return photo;
        }

    }

    public enum Status
    {
        OK,
        ZERO_RESULTS,
        INVALID_REQUEST,
        OVER_QUERY_LIMIT,
        REQUEST_DENIED,
        UNKNOWN_ERROR,
        INNER_METHOD_ERROR
    }
}

