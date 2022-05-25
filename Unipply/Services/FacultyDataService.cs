using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Models.Faculty;
using Unipply.Repositories;

namespace Unipply.Services
{
    public class FacultyDataService : IFacultyDataService
    {
        private readonly IFacultyDataRepository facultyDataRepository;

        public FacultyDataService(IFacultyDataRepository facultyDataRepository)
        {
            this.facultyDataRepository = facultyDataRepository;
        }

        public async Task<List<FacultyData>> GetAsync()
        {
            return await facultyDataRepository.Get().Include(x => x.Specialties).ToListAsync();
        }  
        
        public FacultyData GetFacultyByTitle(string title)
        {
            return facultyDataRepository.Get().Include(x => x.Specialties).FirstOrDefault(x => x.Title.Trim().ToLower() == title.Trim().ToLower());
        } 
    }

    public interface IFacultyDataService
    {
        Task<List<FacultyData>> GetAsync();
        FacultyData GetFacultyByTitle(string title);
    }
}
