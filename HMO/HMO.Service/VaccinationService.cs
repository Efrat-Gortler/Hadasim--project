using HMO.Core.Entity;
using HMO.Core.Repository;
using HMO.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Service
{
    public class VaccinationService : IVaccinationService
    {
        private readonly IVaccinationRepository _vaccinationRepository;
        public VaccinationService(IVaccinationRepository context)
        {
            _vaccinationRepository = context;
        }
        public async Task<IEnumerable<Vaccination>> GetAsync()
        {
            return await _vaccinationRepository.GetAsync();
        }

        public async Task<Vaccination> GetAsync(int id)
        {
            return await _vaccinationRepository.GetAsync(id);
        }

        public async Task<Vaccination> PostAsync(Vaccination value)
        {
            return await _vaccinationRepository.PostAsync(value);
        }

        public async Task PutAsync(int id, Vaccination value)
        {
            await _vaccinationRepository.PutAsync(id, value);
        }

        public async Task DeleteAsync(int id)
        {
            await _vaccinationRepository.DeleteAsync(id);
        }
    }
}
