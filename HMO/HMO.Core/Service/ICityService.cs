using HMO.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.Service
{
    public interface ICityService
    {
        public Task<IEnumerable<City>> GetAsync();
        public Task<City> GetAsync(int id);
        public Task<City> PostAsync(City value);
        public Task PutAsync(int id, City value);
        public Task DeleteAsync(int id);
    }
}
