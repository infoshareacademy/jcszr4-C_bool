using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.DTOs;
using C_bool.BLL.Enums;
using Newtonsoft.Json;

namespace C_bool.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        
        public ReportService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ReportClient");
            _mapper = mapper;
        }

        private const string UserReportUri = "api/UserReports";
        private const string PlaceReportUri = "api/PlaceReports";
        private const string GameTaskReportUri = "api/GameTaskReports";
        private const string UserGameTaskReportUri = "api/UserGameTaskReports";

        public async Task CreateUserReportEntry(User user)
        {
            var userReportCreateDto = _mapper.Map<UserReportCreateDto>(user);
            await CreateReportEntry(userReportCreateDto, UserReportUri);
        }

        public async Task UpdateUserReportEntry(User user)
        {
            var userReportUpdateDto = _mapper.Map<UserReportUpdateDto>(user);
            await UpdateReportEntry(userReportUpdateDto, UserReportUri, user.Id);
        }

        public async Task CreatePlaceReportEntry(Place place)
        {
            var placeReportCreateDto = _mapper.Map<PlaceReportCreateDto>(place);
            await CreateReportEntry(placeReportCreateDto, PlaceReportUri);
        }

        public async Task UpdatePlaceReportEntry(Place place)
        {
            var placeReportUpdateDto = _mapper.Map<PlaceReportUpdateDto>(place);
            await UpdateReportEntry(placeReportUpdateDto, PlaceReportUri, place.Id);
        }

        public async Task CreateGameTaskReportEntry(GameTask gameTask)
        {
            var gameTaskReportCreateDto = _mapper.Map<GameTaskReportCreateDto>(gameTask);
            await CreateReportEntry(gameTaskReportCreateDto, GameTaskReportUri);
        }

        public async Task UpdateGameTaskReportEntry(GameTask gameTask)
        {
            var gameTaskReportUpdateDto = _mapper.Map<GameTaskReportUpdateDto>(gameTask);
            await UpdateReportEntry(gameTaskReportUpdateDto, GameTaskReportUri, gameTask.Id);
        }

        public async Task CreateUserGameTaskReportEntry(UserGameTask userGameTask)
        {
            var userGameTaskReportCreateDto = _mapper.Map<UserGameTaskReportCreateDto>(userGameTask);
            await CreateReportEntry(userGameTaskReportCreateDto, UserGameTaskReportUri);
        }

        public async Task UpdateUserGameTaskReportEntry(UserGameTask userGameTask)
        {
            var userGameTaskReportUpdateDto = _mapper.Map<UserGameTaskReportUpdateDto>(userGameTask);
            await UpdateReportEntry(userGameTaskReportUpdateDto, UserGameTaskReportUri, userGameTask.Id);
        }

        public async Task TheMostPopularGameTaskReport(DateTime ValidFrom)
        {
            var gameTaskReportPopularTaskDto = _mapper.Map<GameTaskReportPopularTaskDto>(ValidFrom);
            await TheMostPopularTypeOfTask(gameTaskReportPopularTaskDto, GameTaskReportUri);
        }


        private async Task TheMostPopularTypeOfTask(IEntityReportDto entityReportDto, string relativeUri)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(entityReportDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
                );

            using var theMostPopularTypeOfTask =
                await _httpClient.GetAsync(relativeUri);
            theMostPopularTypeOfTask.EnsureSuccessStatusCode();
        }

        private async Task CreateReportEntry(IEntityReportDto entityReportDto, string relativeUri)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(entityReportDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            );

            using var createReportResponse =
                await _httpClient.PostAsync(relativeUri, content);

            createReportResponse.EnsureSuccessStatusCode();
        }
        
        private async Task UpdateReportEntry(IEntityReportDto entityReportDto, string relativeUri, int entityId)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(entityReportDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            );

            using var updateReportResponse =
                await _httpClient.PutAsync($"{relativeUri}/{entityId}", content);

            updateReportResponse.EnsureSuccessStatusCode();
        }
    }
}