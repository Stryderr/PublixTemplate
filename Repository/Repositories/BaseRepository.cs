using Repository.Context;

namespace Repository.Repositories
{
    public interface IBaseRepository : IDisposable
    {
        // Define common repository methods here if needed
    }

    public class BaseRepository : IBaseRepository, IDisposable
    {
        protected readonly GenericContext _context;
        private bool _disposed = false;

        public BaseRepository(GenericContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
