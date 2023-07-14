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
    public class PhoneService: IPhoneService
    {
        private IUnitOfWork unitOfWork;

        public PhoneService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PhoneDTO> GetPhones()
        {
            List<Phone> modelList = unitOfWork.Phones.GetAll().ToList();

            if(modelList is not null)
            {
                return ToPhoneDTO(modelList);
            }

            return new List<PhoneDTO>();
        }

        public async Task<bool> CreateAsync(PhoneDTO model)
        {
            if(model is not null)
            {
                var user = await unitOfWork.UserManager.FindByNameAsync(model.UserName);

                if(user is not null)
                {
                    Phone phone = new Phone
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Information = model.Information,
                        Photo = model.Photo,
                        UserName = model.UserName,
                        User = user
                    };

                    await unitOfWork.Phones.CreateAsync(phone);
                    await unitOfWork.SaveAsync();
                }
            }

            return false;
        }


        private IEnumerable<PhoneDTO> ToPhoneDTO(List<Phone> phones)
        {
            List<PhoneDTO> result = new List<PhoneDTO>();

            foreach(Phone phone in phones)
            {
                PhoneDTO phoneDTO = new PhoneDTO
                {
                    Id = phone.Id,
                    Name = phone.Name,
                    Price = phone.Price,
                    Quantity = phone.Quantity,
                    Information = phone.Information,
                    PhotoLink = phone.PhotoLink,
                    UserName = phone.UserName
                };

                result.Add(phoneDTO);
            }

            return result;
        } 
    }
}
