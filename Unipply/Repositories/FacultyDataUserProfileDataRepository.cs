using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models.User;

namespace Unipply.Repositories
{
    public class FacultyDataUserProfileDataRepository : IFacultyDataUserProfileDataRepository
    {
        private readonly DBContext _context;

        public FacultyDataUserProfileDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task CreateAsync(FacultyDataUserProfileData data)
        {
            var dictionaryData = new Dictionary<string, object>
            {
                ["FavouritesFacultiesId"] = data.FavouritesFacultiesId,
                ["UserProfileDatasId"] = data.UserProfileDatasId,
            };
            var facultyDataUserProfileDataContext = _context.Set<Dictionary<string, object>>("FacultyDataUserProfileData").Find(data.FavouritesFacultiesId, data.UserProfileDatasId);
            if(facultyDataUserProfileDataContext == null) 
            {
                _context.Set<Dictionary<string, object>>("FacultyDataUserProfileData").Add(dictionaryData);
                await _context.SaveChangesAsync();
            }
            
        }
    }

    public interface IFacultyDataUserProfileDataRepository
    {
        Task CreateAsync(FacultyDataUserProfileData data);
    }
}
