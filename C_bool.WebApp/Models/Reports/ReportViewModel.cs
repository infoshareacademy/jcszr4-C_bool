using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Reports;

namespace C_bool.WebApp.Models.Reports
{
    public class ReportViewModel
    {
        public ActiveUsersCount ActiveUsersCount { get; set; }
        public GameTaskPointsAverage GameTaskPointsAverage { get; set; }
        public List<GameTaskTypeClassification> GameTaskTypeClassification { get; set; }
        public List<PlaceByGameTasksClassification> PlaceByGameTasksClassification { get; set; }
        public PlaceWithoutGameTasksCount PlaceWithoutGameTasksCount { get; set; }
        public List<UserGameTaskByUsersClassification> UserGameTaskByUsersClassification { get; set; }
        public List<UserGameTaskMostActiveUsersClassification> UserGameTaskMostActiveUsersClassification { get; set; }
        public UserGameTaskDoneCount UserGameTaskDoneCount { get; set; }
        public UserGameTaskDoneTimeAverage UserGameTaskDoneTimeAverage { get; set; }
    }
}
