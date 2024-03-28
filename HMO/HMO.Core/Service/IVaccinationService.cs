using HMO.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.Service
{
    public interface IVaccinationService
    {
        public Task<IEnumerable<Vaccination>> GetAsync();
        public Task<Vaccination> GetAsync(int id);
        public Task<Vaccination> PostAsync(Vaccination value);
        public Task PutAsync(int id, Vaccination value);
        public Task DeleteAsync(int id);
    }
}
