using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Unipply.Models;
using Unipply.Models.User;
using Unipply.Repositories;

namespace Unipply.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataRepository userDataRepository;

        public UserDataService(IUserDataRepository userDataRepository)
        {
            this.userDataRepository = userDataRepository;
        }

        public async Task CreateAsync(UserData data)
        {
            await userDataRepository.CreateAsync(data);
        }

        public async Task<UserData> FindUserByIdAsync(Guid id)
        {
            return await userDataRepository.FindUserById(id).FirstOrDefaultAsync();
        }

        public async Task<UserData> FindUserByNameAsync(string userName)
        {
            return await userDataRepository.FindUserByName(userName).FirstOrDefaultAsync();
        }
        
        public async Task<UserData> FindUserByEmailAsync(string userEmail)
        {
            return await userDataRepository.FindUserByEmail(userEmail).FirstOrDefaultAsync();
        }
    }

    public interface IUserDataService
    {
        Task CreateAsync(UserData data);
        Task<UserData> FindUserByIdAsync(Guid id);
        Task<UserData> FindUserByNameAsync(string userName);
        Task<UserData> FindUserByEmailAsync(string userEmail);
    }
}
