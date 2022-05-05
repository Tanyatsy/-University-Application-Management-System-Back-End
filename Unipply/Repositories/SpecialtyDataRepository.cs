using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models;
using Unipply.Models.Faculty;
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

        public IQueryable<SpecialtyData> GetAsync()
        {
            return _context.SpecialtyData;
        }
    }

    public interface ISpecialtyDataRepository
    {
        IQueryable<SpecialtyData> GetAsync();
    }
}
