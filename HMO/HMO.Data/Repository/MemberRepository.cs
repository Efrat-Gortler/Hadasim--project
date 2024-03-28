using HMO.Core.Entity;
using HMO.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMO.Data.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DataContext _dataContext;

        public MemberRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Member>> GetAsync()
        {
            return await _dataContext.Members
                .Include(m => m.Vaccinations)
                .Include(m => m.City)
                .ToListAsync();
        }

        public async Task<Member> GetAsync(int id)
        {
            return await _dataContext.Members.FindAsync(id);
        }

        public async Task<Member> PostAsync(Member value)
        {
            _dataContext.Members.Add(value);
            await _dataContext.SaveChangesAsync();
            return value;
        }

        public async Task PutAsync(int id, Member value)
        {
            var existingMember = await _dataContext.Members.FindAsync(id);
            if (existingMember != null)
            {
                existingMember.Identity = value.Identity;
                existingMember.Name = value.Name;
                existingMember.CityId = value.CityId;
                existingMember.Street = value.Street;
                existingMember.HouseNumber = value.HouseNumber;
                existingMember.DateOfBirth = value.DateOfBirth;
                existingMember.Phone = value.Phone;
                existingMember.MobilePhone = value.MobilePhone;
                existingMember.Vaccinations = value.Vaccinations;
                existingMember.NumOfVaccinations = value.NumOfVaccinations;
                existingMember.StartOfIll = value.StartOfIll;
                existingMember.EndOfIll = value.EndOfIll;
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _dataContext.Members.FindAsync(id);
            if (member != null)
            {
                _dataContext.Members.Remove(member);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
