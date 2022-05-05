using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await facultyDataRepository.GetAsync().ToListAsync();
        } 
    }

    public interface IFacultyDataService
    {
        Task<List<FacultyData>> GetAsync();
    }
}
