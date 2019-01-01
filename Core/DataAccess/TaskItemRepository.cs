using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList.Core.Models;

namespace TaskList.Core.DataAccess
{
    public class TaskItemRepository : ITaskItemRepository
    {
        public Task Create(TaskItem taskItem)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TaskItem>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<TaskItem> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(TaskItem taskItem)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(TaskItem taskItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
