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
    }
}