using System;
using System.Linq;
using System.Threading.Tasks;
using Unipply.Context;
using Unipply.Models;
using Unipply.Models.User;

namespace Unipply.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly DBContext _context;

        public UserDataRepository(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task CreateAsync(UserData data)
        {
            _context.UserData.Add(data);
            await _context.SaveChangesAsync();
        }

        public IQueryable<UserData> FindUserById(Guid id)
        {
            return _context.UserData.Where(u => u.Id.Equals(id));
        }

         public IQueryable<UserData> FindUserByName(string userName)
        {
            return _context.UserData.Where(u => u.UserName.Equals(userName));
        }

        public IQueryable<UserData> FindUserByEmail(string userEmail)
        {
            return _context.UserData.Where(u => u.Email.Equals(userEmail));
        }
    }

    public interface IUserDataRepository
    {
        Task CreateAsync(UserData data);
        IQueryable<UserData> FindUserById(Guid id);
        IQueryable<UserData> FindUserByName(string userName);
        IQueryable<UserData> FindUserByEmail(string userEmail);
    }
}
