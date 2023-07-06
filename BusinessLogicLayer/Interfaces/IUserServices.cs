using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserServices
    {
        Task CreateUser(string username, string password);
        Task<bool> Authentication(string username, string password);
    }
}
