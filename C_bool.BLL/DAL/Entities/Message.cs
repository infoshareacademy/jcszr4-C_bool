using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.BLL.DAL.Entities
{
    public class Message : Entity
    {
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public int RootId { get; set; }
        public int ParentId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsViewed { get; set; }
    }
}
