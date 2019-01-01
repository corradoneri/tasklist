using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList.Core.Models;

namespace TaskList.Core.DataAccess
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> Get();
        Task<TaskItem> Get(int id);
        Task Create(TaskItem taskItem);
        Task Update(TaskItem taskItem);
        Task Remove(TaskItem taskItem);
        Task Remove(int id);
    }
}
