using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C_bool.WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class UserStatisticsModel : PageModel
    {
        private readonly IUserService _userService;

        public int RankingPlace { get; set; }
        public int Points { get; set; }

        public int FavPlacesCount { get; set; }
        public int PendingTasksCount { get; set; }
        public int InProgressTasksCount { get; set; }
        public int DoneTasksCount { get; set; }

        public int CreatedPlacesCount { get; set; }
        public int CreatedGameTasksCount { get; set; }

        public UserStatisticsModel(IUserService userService)
        {
            _userService = userService;
        }

        private async Task LoadAsync()
        {
            Points = _userService.GetCurrentUser().Points;
            RankingPlace = _userService.GetRankingPlace();
            FavPlacesCount = _userService.GetFavPlaces().Count;
            PendingTasksCount = _userService.GetToDoTasks().Count;
            InProgressTasksCount = _userService.GetInProgressTasks().Count;
            DoneTasksCount = _userService.GetDoneTasks().Count;

            CreatedPlacesCount = _userService.GetPlacesCreatedByUser().Count;
            CreatedGameTasksCount = _userService.GetGameTasksCreatedByUser().Count;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }
    }
}
