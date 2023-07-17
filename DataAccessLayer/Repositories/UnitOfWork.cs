using DataAccessLayer.Database;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class UnitOfWork: IUnitOfWork
    {
        private AppDatabase db;
        private bool disposed = false;

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private PhoneRepository? phoneRepository;
        private OrderRepository? orderRepository;

        public UnitOfWork(AppDatabase appDatabase, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            this.db = appDatabase;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public UserManager<User> UserManager => userManager;
        public SignInManager<User> SignInManager => signInManager;
        public IRepository<Phone> Phones
        {
            get
            {
                if (phoneRepository == null)
                    phoneRepository = new PhoneRepository(db);

                return phoneRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if(orderRepository == null)
                    orderRepository = new OrderRepository(db);

                return orderRepository;
            }
        }

        public async Task SaveAsync() => await db.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
    }
}
