using System.Collections.Generic;
using Tasks.Core.Models;

namespace Tasks.Core.DataAccess
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasks();

        void AddRecord(Task task);
        void UpdateTasks(Task task);
    }
}
