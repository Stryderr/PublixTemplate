using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Interfaces;
using S0WISRXX.SharedExternal.Logger;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        protected readonly GenericContext _context;
        protected readonly IUtilityLogger _logger;
        protected readonly IMapper _mapper;

        private bool _disposed = false;

        private string env;
        private string user;
        private string password;
        private string server;
        private string systemID;

        public BaseRepository(GenericContext context, IMapper mapper, IUtilityLogger logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Setvariables();
        }


        public void Setvariables()
        {
            env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            user = Environment.GetEnvironmentVariable("SQLUser");
            password = Environment.GetEnvironmentVariable("SQLPassword");
            server = configuration["AppSettings:SQLServer"];
            systemID = configuration["AppSettings:SystemID"];
        }

        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T?> GetByIdAsync(long id) => await _context.Set<T>().FindAsync(id).AsTask();
        public async Task AddAsync(T entity) => await Task.FromResult(_context.Set<T>().Add(entity));
        public async Task DeleteAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null) _context.Set<T>().Remove(entity);
        }
        public Task SaveChangesAsync() => _context.SaveChangesAsync();


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
