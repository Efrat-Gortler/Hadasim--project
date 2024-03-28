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
    public class CityService:ICityService
    {
        private readonly ICityRepository _CityRepository;
        public CityService(ICityRepository context)
        {
            _CityRepository = context;
        }
        public async Task<IEnumerable<City>> GetAsync()
        {
            return await _CityRepository.GetAsync();
        }

        public async Task<City> GetAsync(int id)
        {
            return await _CityRepository.GetAsync(id);
        }

        public async Task<City> PostAsync(City value)
        {
            return await _CityRepository.PostAsync(value);
        }

        public async Task PutAsync(int id, City value)
        {
            await _CityRepository.PutAsync(id, value);
        }

        public async Task DeleteAsync(int id)
        {
            await _CityRepository.DeleteAsync(id);
        }
    }
}
