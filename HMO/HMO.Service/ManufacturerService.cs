using HMO.Core.Entity;
using HMO.Core.Repository;
using HMO.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Service
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _ManufacturerRepository;
        public ManufacturerService(IManufacturerRepository context)
        {
            _ManufacturerRepository = context;
        }
        public async Task<IEnumerable<Manufacturer>> GetAsync()
        {
            return await _ManufacturerRepository.GetAsync();
        }

        public async Task<Manufacturer> GetAsync(int id)
        {
            return await _ManufacturerRepository.GetAsync(id);
        }

        public async Task<Manufacturer> PostAsync(Manufacturer value)
        {
            return await _ManufacturerRepository.PostAsync(value);
        }

        public async Task PutAsync(int id, Manufacturer value)
        {
            await _ManufacturerRepository.PutAsync(id, value);
        }

        public async Task DeleteAsync(int id)
        {
            await _ManufacturerRepository.DeleteAsync(id);
        }
    }
}
