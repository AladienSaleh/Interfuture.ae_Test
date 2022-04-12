using interfuture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace interfuture.Providers
{
    public interface ITaskProvider
    {
        Task<int> Create(Data.Task user);

        Task<Data.Task> Get(int userId);

        Task<(List<Data.Task> taskList, int totalSize)> GetAll(int offset, int pageSize);

        Task<Data.Task> Delete(int userId);

        Task<Data.Task> Update(Data.Task user);


    }

    public class TaskProvider : ITaskProvider
    {
        private readonly InterfutureDbContext dbContext;

        public TaskProvider(
            InterfutureDbContext dbContext
            )
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Create(Data.Task task)
        {
            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task.Id;
        }

        public async Task<Data.Task> Delete(int taskId)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);

            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<Data.Task> Get(int taskId)
        {
            var task = await dbContext.Tasks
                .Where(task => task.Id == taskId)
                .Include(t => t.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return task;
        }

        public async Task<(List<Data.Task> taskList, int totalSize)> GetAll(int offset, int pageSize)
        {
            var taskQuery = dbContext.Tasks
                .Include(t => t.User)
                .AsNoTracking();
            var count = await taskQuery.CountAsync();

            List<Data.Task>? taskList;
            if (pageSize > 0)
            {
                taskList = await taskQuery.Skip(offset).Take(pageSize).ToListAsync();
            }
            else
            {
                taskList = await taskQuery.Skip(offset).ToListAsync();
            }
            return (taskList, count);
        }

        public async Task<Data.Task> Update(Data.Task task)
        {
            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task;
        }
    }
}
