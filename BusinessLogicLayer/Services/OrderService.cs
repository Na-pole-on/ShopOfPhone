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
    internal class OrderService: IOrderService
    {
        private IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OrderDTO> GetByIdAsync(string orderId)
        {
            Order order = await unitOfWork.Orders.GetByIdAsync(orderId);

            if(order is not null)
            {
                OrderDTO dto = new OrderDTO
                {
                    Id = order.Id,
                    Quantity = order.Quantity,
                    PhoneId = order.PhoneId,
                    UserId = order.UserId
                };

                return dto;
            }

            return new OrderDTO();
        }

        public IEnumerable<OrderDTO> GetAllFromUser(string userId)
        {
            IEnumerable<Order> orders = unitOfWork.Orders
                .GetAll().Where(o => o.UserId == userId);

            if (orders.Count() > 0)
            {
                IEnumerable<OrderDTO> dtos = orders.Select(d => new OrderDTO
                {
                    Id = d.Id,
                    Quantity = d.Quantity,
                    UserId = userId,
                    PhoneId = userId
                });

                return dtos;

            }

            return new List<OrderDTO>();
        }

        public async Task<bool> AddFromUserAsync(OrderDTO model)
        {
            if(model is not null)
            {
                Order order = new Order
                {
                    Quantity = model.Quantity,
                    User = await unitOfWork.UserManager.FindByIdAsync(model.UserId),
                    Phone = await unitOfWork.Phones.GetByIdAsync(model.PhoneId)
                };

                await unitOfWork.Orders.CreateAsync(order);
                await unitOfWork.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteFromUserAsync(string orderId, string userId)
        {
            User user = await unitOfWork.UserManager
                .FindByIdAsync(userId);

            if(user is not null)
            {
                Order? order = user.Orders.FirstOrDefault(o => o.Id == orderId);

                if(order is not null)
                {
                    user.Orders.Remove(order);

                    return true;
                }

            }

            return false;
        }
    }
}
