using System.Threading.Tasks;

namespace WebApi.Persistent.Utility
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FcDbContext _context;
        public UnitOfWork(FcDbContext context)
        {
            this._context = context;
            //this._context.Database.EnsureCreated();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
