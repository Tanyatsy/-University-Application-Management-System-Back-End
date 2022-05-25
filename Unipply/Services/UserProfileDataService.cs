using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Unipply.Models.User;
using Unipply.Repositories;

namespace Unipply.Services
{
    public class UserProfileDataService : IUserProfileDataService
    { 
        private readonly IUserProfileDataRepository userProfileDataRepository;

        public UserProfileDataService(IUserProfileDataRepository userProfileDataRepository)
        {
            this.userProfileDataRepository = userProfileDataRepository;
        }

        public async Task CreateAsync(UserProfileData data)
        {
             await userProfileDataRepository.CreateAsync(data);
        }

        public async Task UpdateAsync(UserProfileData data)
        {
            await userProfileDataRepository.UpdateAsync(data);
        } 
        
        public async Task UpdateHobbiesAsync(UserProfileData data)
        {
            await userProfileDataRepository.UpdateHobbiesAsync(data);
        }

        public async Task<UserProfileData> FindUserProfileDataByUserIdAsync(Guid userId)
        {
            return await userProfileDataRepository.FindUserProfileDataByUserId(userId).FirstOrDefaultAsync();
        }
    }

    public interface IUserProfileDataService
{
        Task CreateAsync(UserProfileData data);
        Task UpdateAsync(UserProfileData data);
        Task UpdateHobbiesAsync(UserProfileData data);
        Task<UserProfileData> FindUserProfileDataByUserIdAsync(Guid userId);
    }
}
