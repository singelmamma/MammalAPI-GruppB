using System.Threading.Tasks;
using MammalAPI.Context;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class Repository : IRepository
    {
        protected readonly DBContext _dBContext;
        protected readonly ILogger<Repository> _logger;

        public Repository()
        {

        }

        public Repository(DBContext context, ILogger<Repository> logger)
        {
            _dBContext = context;
            _logger = logger;
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _dBContext.Add(entity);
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type { entity.GetType()}");
            _dBContext.Remove(entity);
        }

        public virtual async Task<bool> Save()
        {
            _logger.LogInformation($"Saving Changes");
            return (await _dBContext.SaveChangesAsync()) >= 0;
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _dBContext.Update(entity);
        }
    }
}
