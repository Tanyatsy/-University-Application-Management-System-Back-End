using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models.User;

namespace Unipply.Repositories
{
    public class SpecialtyDataUserProfileDataRepository : ISpecialtyDataUserProfileDataRepository
    {
        private readonly DBContext _context;

        public SpecialtyDataUserProfileDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task CreateAsync(SpecialtyDataUserProfileData data)
        {
            var dictionaryData = new Dictionary<string, object>
            {
                ["FavouritesSpecialtiesId"] = data.FavouritesSpecialtiesId,
                ["UserProfileDatasId"] = data.UserProfileDatasId,
            };
            var specialtyDataUserProfileDataContext = _context
                            .Set<Dictionary<string, object>>("SpecialtyDataUserProfileData")
                            .Find(data.FavouritesSpecialtiesId, data.UserProfileDatasId);

            if(specialtyDataUserProfileDataContext == null) 
            {
                _context.Set<Dictionary<string, object>>("SpecialtyDataUserProfileData").Add(dictionaryData);
                await _context.SaveChangesAsync();
            }
            
        }
    }

    public interface ISpecialtyDataUserProfileDataRepository
    {
        Task CreateAsync(SpecialtyDataUserProfileData data);
    }
}
