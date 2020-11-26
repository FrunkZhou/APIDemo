using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models.Database
{
    public interface ICityRepository
    {
        Task<IQueryable<City>> GetAllAsync();

        Task<City> GetByIdAsync(string id);

        Task<bool> CreateAsync(City city);

        Task<bool> UpdateAsync(City city);

        Task<bool> DeleteAsync(City city);
    }
}
