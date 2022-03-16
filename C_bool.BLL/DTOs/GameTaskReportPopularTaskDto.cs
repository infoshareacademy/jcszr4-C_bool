using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.Enums;

namespace C_bool.BLL.DTOs
{
    public class GameTaskReportPopularTaskDto: IEntityReportDto
    {
        public TaskType GameTaskType { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
