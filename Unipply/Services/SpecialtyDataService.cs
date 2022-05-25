using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Models.Specialty;
using Unipply.Repositories;

namespace Unipply.Services
{
    public class SpecialtyDataService : ISpecialtyDataService
    {
        private readonly ISpecialtyDataRepository specialtyDataRepository;

        public SpecialtyDataService(ISpecialtyDataRepository specialtyDataRepository)
        {
            this.specialtyDataRepository = specialtyDataRepository;
        }

        public async Task<List<SpecialtyData>> GetAsync()
        {
            return await specialtyDataRepository.Get().ToListAsync();
        }

        public SpecialtyData GetSpecialtyByTitle(string title)
        {
            return specialtyDataRepository.Get().FirstOrDefault(x => x.Title.Trim().ToLower() == title.Trim().ToLower());
        }
    }

    public interface ISpecialtyDataService
    {
       Task<List<SpecialtyData>> GetAsync();
       SpecialtyData GetSpecialtyByTitle(string title);
    }
}
