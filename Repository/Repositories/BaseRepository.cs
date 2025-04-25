using AutoMapper;
using Repository.Context;
using S0WISRXX.SharedExternal.Logger;

namespace Repository.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        protected readonly GenericContext _context;
        public readonly IUtilityLogger _logger;
        public readonly IMapper _mapper;

        private bool _disposed = false;

        public BaseRepository(GenericContext context, IMapper mapper, IUtilityLogger logger) : base()
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<T> ExecuteWithLoggingAsync<T>(Func<Task<T>> func, string errorMessage)
        {
            try
            {
                // Execute the function and return the result
                return await func();
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow it
                _logger.LogError(ex, errorMessage);
                throw new Exception(ex.Message);
            }
        }

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
