using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Phone
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Information { get; set; }
        public string? PhotoLink { get; set; }

        //Ignore
        public IFormFile? Photo { get; set; }

        public User? User { get; set; } 
        public string? UserName { get; set; }

        public Phone()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
