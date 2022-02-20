using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using C_bool.BLL.Config;
using C_bool.BLL.Models.GooglePlaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//TODO: przenieść do BLL, jak się da
namespace C_bool.BLL.Helpers
{
    public class GoogleApiAsync
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GoogleAPISettings _googleApiSettings;
        private string ApiKey { get; set; }
        public List<GooglePlace> Places { get; set; }
        public string Message { get; set; }
        public Status QueryStatus { get; set; }

        public GoogleApiAsync(IHttpClientFactory httpClientFactory, GoogleAPISettings googleApiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _googleApiSettings = googleApiSettings;
            ApiKey = _googleApiSettings.GoogleAPIKey;
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

        public async Task<List<GooglePlace>> GetBySearchQuery(string query = "", string language = "pl")
        {
            var listPlaces = new List<GooglePlace>();
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

        public async Task<List<GooglePlace>> GetNearby(string latitude,
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
            var listPlaces = new List<GooglePlace>();
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

        private List<GooglePlace> GetPlacesFromResponseContent(string responseContent, out string nextPageToken)
        {

            var listPlaces = new List<GooglePlace>();
            try
            {
                Enum.TryParse(TrimJson(responseContent, "status"), out Status queryStatus);
                var trimmedJson = TrimJson(responseContent, "results");
                listPlaces.AddRange(JsonConvert.DeserializeObject<List<GooglePlace>>(trimmedJson));
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

        public async Task<string> DownloadImageAsync(GooglePlace place, string width)
        {
            using var httpClient = new HttpClient();
            var requestUri =
                @$"https://maps.googleapis.com/maps/api/place/photo?maxwidth={width}&photo_reference={place.GooglePhotos[0].PhotoReference}&key={ApiKey}";
            Uri uri = new Uri(requestUri);
            var imageBytes = await httpClient.GetByteArrayAsync(uri);
            return Convert.ToBase64String(imageBytes);
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

