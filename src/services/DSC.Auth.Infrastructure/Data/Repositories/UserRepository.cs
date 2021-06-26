using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using DSC.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSC.Auth.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        public UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserModel> GetByUserNameAsync(string userName)
        {
            return await _userManager.Users
                .Where(a => a.Status == EntityStatusEnum.Ativa)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userManager.Users
                .AsNoTracking().ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return await _userManager.Users
                .Where(a => a.Status == EntityStatusEnum.Ativa)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IdentityResult> CreateAsync(UserModel user, string password)
        {
            return await _userManager.CreateAsync(user, password);

        }

        public async Task<SignInResult> SignInAsync(string userName, string password)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: true);
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return await _signInManager.UserManager.CheckPasswordAsync(user, password);
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Add(UserModel obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
