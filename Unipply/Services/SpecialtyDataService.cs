using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await specialtyDataRepository.GetAsync().ToListAsync();
        }
    }

    public interface ISpecialtyDataService
    {
       Task<List<SpecialtyData>> GetAsync();
    }
}
