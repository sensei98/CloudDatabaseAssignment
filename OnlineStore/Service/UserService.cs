using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL;
using OnlineStore.DTO;
using OnlineStore.Interface;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service
{
    public class UserService : IUserService
    {
        private readonly OnlineStoreContext _context;
        public UserService(OnlineStoreContext context)
        {
            _context = context;
        }
        public async Task AddUser(CreateUserDTO user)
        {
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSingleUserById(Guid id)
        {
            _context.Users.Remove(new User { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetSingleUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUserById(Guid id, User user)
        {
            user.Id = id;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
