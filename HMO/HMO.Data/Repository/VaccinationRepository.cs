
using HMO.Core.Entity;
using HMO.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMO.Data.Repository
{
    public class VaccinationRepository : IVaccinationRepository
    {
        private readonly DataContext _dataContext;

        public VaccinationRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Vaccination>> GetAsync()
        {
            return await _dataContext.Vaccinations.ToListAsync();
        }

        public async Task<Vaccination> GetAsync(int id)
        {
            return await _dataContext.Vaccinations.FindAsync(id);
        }

        public async Task<Vaccination> PostAsync(Vaccination value)
        {
            // Check if the MemberId provided in the vaccination is valid
            var member = await _dataContext.Members.FindAsync(value.MemberId);
            if (member == null)
            {
                throw new Exception("A member with the provided MemberId does not exist.");
            }

            _dataContext.Vaccinations.Add(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task PutAsync(int id, Vaccination value)
        {
            var v = await _dataContext.Vaccinations.FindAsync(id);
            if (v != null)
            {
                v.ManufacturerId = value.ManufacturerId;
                v.Date = value.Date;
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var vaccination = await _dataContext.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                _dataContext.Vaccinations.Remove(vaccination);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}

//using HMO.Core.Entity;
//using HMO.Core.Repository;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace HMO.Data.Repository
//{
//    public class VaccinationRepository : IVaccinationRepository
//    {
//        private readonly DataContext _dataContext;
//        public VaccinationRepository(DataContext context)
//        {
//            _dataContext = context;
//        }

//        public async Task<IEnumerable<Vaccination>> GetAsync()
//        {
//            return await _dataContext.Vaccinations.ToListAsync();
//        }

//        public async Task<Vaccination> GetAsync(int id)
//        {
//            return await _dataContext.Vaccinations.FindAsync(id);
//        }

//        //public async Task<Vaccination> PostAsync(Vaccination value)
//        //{
//        //    _dataContext.Vaccinations.Add(value);
//        //    await _dataContext.SaveChangesAsync();
//        //    return value;
//        //}


//        public async Task<Vaccination> PostAsync(Vaccination value)
//        {
//            // Check if the MemberId provided in the vaccination is valid
//            var member = await _dataContext.Members.FindAsync(value.MemberId);
//            if (member == null)
//            {
//                throw new Exception("A member with the provided MemberId does not exist.");
//            }

//            _dataContext.Vaccinations.Add(value);
//            await _dataContext.SaveChangesAsync();
//            return value;
//        }

//        public async Task PutAsync(int id, Vaccination value)
//        {
//            var v = await _dataContext.Vaccinations.FindAsync(id);
//            if (v != null)
//            {
//                v.Producer = value.Producer;
//                v.Date = value.Date;
//                await _dataContext.SaveChangesAsync();
//            }
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var vaccination = await _dataContext.Vaccinations.FindAsync(id);
//            if (vaccination != null)
//            {
//                _dataContext.Vaccinations.Remove(vaccination);
//                await _dataContext.SaveChangesAsync();
//            }
//        }
//    }
//}
