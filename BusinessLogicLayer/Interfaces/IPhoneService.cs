using BusinessLogicLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPhoneService
    {
        IEnumerable<PhoneDTO> GetPhones();
        Task<bool> CreateAsync(PhoneDTO model);
    }
}
