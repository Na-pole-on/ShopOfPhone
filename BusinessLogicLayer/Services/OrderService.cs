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
            Order order = await unitOfWork.Orders.GetById(orderId);

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
            IEnumerable<Order> orders = unitOfWork.UserManager
                .FindByIdAsync(userId).Result.Orders ?? new List<Order>();

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

        public 
    }
}
