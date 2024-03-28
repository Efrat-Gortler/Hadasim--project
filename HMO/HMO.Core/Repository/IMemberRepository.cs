using HMO.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.Repository
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<Member>> GetAsync();
        public Task<Member> GetAsync(int id);
        public Task<Member> PostAsync(Member value);
        public Task PutAsync(int id, Member value);
        public Task DeleteAsync(int id);
    }
}
