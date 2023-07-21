using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public int Quantity { get; set; }


        public string? UserId { get; set; }
        public User? User { get; set; }

        public string? PhoneId { get; set; }
        public Phone? Phone { get; set; }

        public Order()
        {
            this.Id = Guid.NewGuid().ToString();   
        }
    }
}
