using System.Threading.Tasks;
using MammalAPI.Context;
using Microsoft.Extensions.Logging;

namespace MammalAPI.Services
{
    public class Repository : IRepository
    {
        protected readonly DBContext _dBContext;
        protected readonly ILogger<Repository> _logger;

        public Repository(DBContext context, ILogger<Repository> logger)
        {
            _dBContext = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _dBContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type { entity.GetType()}");
            _dBContext.Remove(entity);
        }

        public Task<bool> Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _dBContext.Update(entity);
        }
    }
}
