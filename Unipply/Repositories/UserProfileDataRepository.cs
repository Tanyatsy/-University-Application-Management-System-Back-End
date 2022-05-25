using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models.User;

namespace Unipply.Repositories
{
    public class UserProfileDataRepository : IUserProfileDataRepository
    {
        private readonly DBContext _context;

        public UserProfileDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task CreateAsync(UserProfileData data)
        {
            _context.UserProfileData.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfileData data)
        {
            _context.UserProfileData.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHobbiesAsync(UserProfileData data)
        {
            var userProfile = _context.UserProfileData.Find(data.Id);
            userProfile.Hobbies = data.Hobbies;
            _context.UserProfileData.Update(data);
            await _context.SaveChangesAsync();
        }

        public IQueryable<UserProfileData> FindUserProfileDataByUserId(Guid userId)
        {
            return _context.UserProfileData
                .Where(u => u.UserData.Id.Equals(userId))
                .Include(x => x.UserData)
                .Include(x => x.FavouritesFaculties)
                .ThenInclude(x => x.Specialties)
                .Include(x => x.FavouritesSpecialties);
        }
    }

    public interface IUserProfileDataRepository
    {
        Task CreateAsync(UserProfileData data);
        Task UpdateAsync(UserProfileData data);
        Task UpdateHobbiesAsync(UserProfileData data);
        IQueryable<UserProfileData> FindUserProfileDataByUserId(Guid userID);
    }
}
