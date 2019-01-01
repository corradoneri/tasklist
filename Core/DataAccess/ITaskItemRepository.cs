using System.Collections.Generic;
using Tasks.Core.Models;

namespace Tasks.Core.DataAccess
{
    public interface ITaskItemRepository
    {
        IEnumerable<TaskItem> GetTaskItems();

        void AddTaskItem(TaskItem taskItem);
        void UpdateTaskItem(TaskItem taskItem);
    }
}
