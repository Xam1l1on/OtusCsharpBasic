using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBot.Core.Models
{
    public class TaskFK
    {
        public int TaskId { get; set; }
        public int Description { get; set; }
        public int UserResponsible { get; set; }
        public TaskFKType TaskType { get; set; }
    }
    enum TaskFKType
    {
        Basic,
        Project,
        Other
    }
}
