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

        public async Task CreateUser(string email, string password)
        {
            var model = await unitOfWork.UserManager.FindByEmailAsync(email);

            if(model is null)
            {
                User user = new User { Email = email, UserName = email };
                await unitOfWork.UserManager.CreateAsync(user, password);
            }
        }
    }
}
