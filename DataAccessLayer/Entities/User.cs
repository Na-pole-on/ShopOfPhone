using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User: IdentityUser
    {
        public List<Phone>? Phones { get; set; }
    }
}
