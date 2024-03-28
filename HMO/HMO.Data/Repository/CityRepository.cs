using HMO.Core.Entity;
using HMO.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMO.Data.Repository
{

    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dataContext;

        public CityRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<City>> GetAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(int id)
        {
            return await _dataContext.Cities.FindAsync(id);
        }

        public async Task<City> PostAsync(City value)
        {
            _dataContext.Cities.Add(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task PutAsync(int id, City value)
        {
            var city = await _dataContext.Cities.FindAsync(id);
            if (city != null)
            {
                city.Name = value.Name;
                // You can update other properties similarly
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _dataContext.Cities.FindAsync(id);
            if (city != null)
            {
                _dataContext.Cities.Remove(city);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}



