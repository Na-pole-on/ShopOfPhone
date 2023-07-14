using DataAccessLayer.Database;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class PhoneRepository: IRepository<Phone>
    {
        AppDatabase db;

        public PhoneRepository(AppDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<Phone> GetAll() => db.Phones;

        public async Task<Phone> GetById(string id) => await db.Phones
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<bool> CreateAsync(Phone model)
        {
            if(model is not null)
            {
                var list = db.Phones.Where(p => p.UserName == model.UserName).ToList();
                string path = model.User.UserName + $"-{list.Count}.png";
                model.PhotoLink = "/photo/" + path;

                using(var fileStream = new FileStream("D:\\Data\\ShopOfPhone\\ShopOfPhone\\wwwroot\\photo\\" + path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                await db.Phones.AddAsync(model);
                await db.SaveChangesAsync();
            }

            return false;
        }

        public bool Update(Phone model)
        {
            if(model is not null)
            {
                db.Entry(model).State = EntityState.Modified;

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Phone model = await GetById(id);

            if(model is not null)
            {
                db.Phones.Remove(model);

                return true;
            }

            return false;
        }
    }
}
