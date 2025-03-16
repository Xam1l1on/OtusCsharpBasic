using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBot.Core.Helper
{
    class DialogStateStorage
    {
        public Dictionary<long, DialogState> dialogState = new Dictionary<long, DialogState>();
    }
    enum DialogState
    {
        Description,
        Responsible,
        TaskType,
    }
}
