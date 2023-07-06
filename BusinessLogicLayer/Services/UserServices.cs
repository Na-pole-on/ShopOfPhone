using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    internal class UserServices: IUserServices
    {
        private IUnitOfWork unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateUser(string username, string password)
        {
            var model = await unitOfWork.UserManager.FindByNameAsync(username);

            if(model is null)
            {
                User user = new User { UserName = username };
                await unitOfWork.UserManager.CreateAsync(user, password);
            }
        }

        public async Task<bool> Authentication(string username, string password)
        {
            var result = await unitOfWork.SignInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task SignOut() => await unitOfWork.SignInManager.SignOutAsync();

    }
}
