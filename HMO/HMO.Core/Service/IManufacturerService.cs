using HMO.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.Service
{
    public interface IManufacturerService
    {
        public Task<IEnumerable<Manufacturer>> GetAsync();
        public Task<Manufacturer> GetAsync(int id);
        public Task<Manufacturer> PostAsync(Manufacturer value);
        public Task PutAsync(int id, Manufacturer value);
        public Task DeleteAsync(int id);
    }
}
