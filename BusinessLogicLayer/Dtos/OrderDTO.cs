using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public string? PhoneId { get; set; }
    }
}
