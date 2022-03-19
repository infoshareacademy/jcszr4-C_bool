using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Reports;

namespace C_bool.WebApp.Models.Reports
{
    public class ReportViewModel
    {
        public ActiveUsersCount ActiveUsersCount { get; set; } //OK
        public GameTaskPointsAverage GameTaskPointsAverage { get; set; } //OK
        public List<GameTaskTypeClassification> GameTaskTypeClassification { get; set; } //OK
        public List<PlaceByGameTasksClassification> PlaceByGameTasksClassification { get; set; } //OK
        public PlaceWithoutGameTasksCount PlaceWithoutGameTasksCount { get; set; } //OK
        public List<UserGameTaskByUsersClassification> UserGameTaskByUsersClassification { get; set; } //OK
        public List<UserGameTaskMostActiveUsersClassification> UserGameTaskMostActiveUsersClassification { get; set; } //OK
        public UserGameTaskDoneCount UserGameTaskDoneCount { get; set; } //OK
        public UserGameTaskDoneTimeAverage UserGameTaskDoneTimeAverage { get; set; } //OK
    }
}
