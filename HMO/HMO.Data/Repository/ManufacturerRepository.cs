using HMO.Core.Entity;
using HMO.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMO.Data.Repository
{

    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataContext _dataContext;

        public ManufacturerRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Manufacturer>> GetAsync()
        {
            return await _dataContext.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetAsync(int id)
        {
            return await _dataContext.Manufacturers.FindAsync(id);
        }

        public async Task<Manufacturer> PostAsync(Manufacturer value)
        {
            _dataContext.Manufacturers.Add(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task PutAsync(int id, Manufacturer value)
        {
            var Manufacturer = await _dataContext.Manufacturers.FindAsync(id);
            if (Manufacturer != null)
            {
                Manufacturer.Name = value.Name;
                // You can update other properties similarly
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var Manufacturer = await _dataContext.Manufacturers.FindAsync(id);
            if (Manufacturer != null)
            {
                _dataContext.Manufacturers.Remove(Manufacturer);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
