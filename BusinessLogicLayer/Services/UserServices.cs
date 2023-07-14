using BusinessLogicLayer.Dtos;
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
                await Authentication(user.UserName, password);
            }
        }

        public async Task<bool> Authentication(string username, string password)
        {
            var result = await unitOfWork.SignInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task SignOut() => await unitOfWork.SignInManager.SignOutAsync();

        public async Task<UserDTO> GetUserById(string id)
        {
            User model = await unitOfWork.UserManager.FindByIdAsync(id);

            if(model is not null)
            {
                UserDTO user = new UserDTO
                {
                    UserName = model.UserName,
                    Email = (model.Email is null || model.Email == "") ? "NULL" : model.Email,
                    PhoneNumber = (model.PhoneNumber is null || model.PhoneNumber == "") ? "NULL" : model.PhoneNumber
                };

                return user;
            }

            return null;
        }
    }
}
