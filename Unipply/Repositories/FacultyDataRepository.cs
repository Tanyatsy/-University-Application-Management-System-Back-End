using System.Linq;
using Unipply.Context;
using Unipply.Models.Faculty;

namespace Unipply.Repositories
{
    public class FacultyDataRepository : IFacultyDataRepository
    {
        private readonly DBContext _context;

        public FacultyDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<FacultyData> GetAsync()
        {
            return _context.FacultyData;
        }
    }

    public interface IFacultyDataRepository
    {
        IQueryable<FacultyData> GetAsync();
    }
}
