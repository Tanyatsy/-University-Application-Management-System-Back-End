using Microsoft.EntityFrameworkCore;
using System.Linq;
using Unipply.Context;
using Unipply.Models.Specialty;

namespace Unipply.Repositories
{
    public class SpecialtyDataRepository : ISpecialtyDataRepository
    {
        private readonly DBContext _context;

        public SpecialtyDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<SpecialtyData> Get()
        {
            return _context.SpecialtyData.Include(x => x.Faculty);
        }
    }

    public interface ISpecialtyDataRepository
    {
        IQueryable<SpecialtyData> Get();
    }
}
