using DataAccessLayer.Database;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class OrderRepository: IRepository<Order>
    {
        private AppDatabase db;

        public OrderRepository(AppDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Order> GetAll() => db.Orders;

        public async Task<Order> GetByIdAsync(string id) => await db.Orders
            .FirstOrDefaultAsync(o => o.Id == id);

        public Order GetById(string id) => db.Orders
            .FirstOrDefault(o => o.Id == id);

        public async Task<bool> CreateAsync(Order model)
        {
            if(model is not null)
                await db.Orders.AddAsync(model);

            return false;
        }

        //it is not work
        public bool Update(Order model) => false;

        public async Task<bool> DeleteAsync(string id)
        {
            Order order = await GetByIdAsync(id);

            if(order is not null)
            {
                db.Orders.Remove(order);

                return true;
            }

            return false;
        }
    }
}
