using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TaskList.Core.DataAccess;
using TaskList.Core.Models;

namespace TaskList.Infrastructure.DataAccess
{
    public class TaskItemRepository : ITaskItemRepository
    {
        static TaskItemRepository()
        {
            BsonClassMap.RegisterClassMap<TaskItem>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(x => x.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer().WithRepresentation(BsonType.ObjectId)));
                cm.GetMemberMap(x => x.Title)
                    .SetElementName("title")
                    .SetIsRequired(true);
                cm.GetMemberMap(x => x.Description)
                    .SetElementName("description")
                    .SetIsRequired(false)
                    .SetIgnoreIfNull(true);
                cm.GetMemberMap(x => x.Completed)
                    .SetElementName("completed")
                    .SetIsRequired(false)
                    .SetIgnoreIfNull(true);
            });
        }

        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<TaskItem> _taskItems;

        public TaskItemRepository()
        {
            // TODO - Refactor configuration and creation of MongoClient
            //
            _client = new MongoClient();
            _db = _client.GetDatabase("tasks");
            _taskItems = _db.GetCollection<TaskItem>("taskItems");
        }

        public async Task Create(TaskItem taskItem)
        {
            await _taskItems.InsertOneAsync(taskItem);
        }

        public async Task<IEnumerable<TaskItem>> Get()
        {
            return await _taskItems
                .Find(new BsonDocument())
                .ToListAsync();               
        }

        public async Task<TaskItem> Get(string id)
        {
            return await _taskItems
                .Find(ti => ti.Id == id)
                .FirstOrDefaultAsync<TaskItem>();
        }

        public async Task Remove(TaskItem taskItem)
        {
            await Remove(taskItem.Id);
        }

        public async Task Remove(string id)
        {
            await _taskItems.DeleteOneAsync(ti => ti.Id == id);
        }

        public async Task Update(TaskItem taskItem)
        {
            var update = Builders<TaskItem>.Update
                .Set(ti => ti.Title, taskItem.Title)
                .Set(ti => ti.Description, taskItem.Description)
                .Set(ti => ti.Completed, taskItem.Completed);
            await _taskItems.UpdateOneAsync(ti => ti.Id == taskItem.Id, update);
        }
    }
}
