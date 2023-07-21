using BusinessLogicLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> GetByIdAsync(string orderId);
        IEnumerable<OrderDTO> GetAllFromUser(string userId);
        Task<bool> AddFromUserAsync(OrderDTO orderDTO);
        Task<bool> DeleteFromUserAsync(string orderId, string userId);
    }
}
