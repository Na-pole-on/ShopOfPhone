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

        public UnitOfWork(AppDatabase appDatabase, UserManager<User> userManager)
        {
            this.db = appDatabase;
            this.userManager = userManager;
        }

        public UserManager<User> UserManager => userManager;

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
