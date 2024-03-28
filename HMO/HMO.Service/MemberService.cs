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
    public class MemberService:IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberService(IMemberRepository context)
        {
            _memberRepository = context;
        }
        public async Task<IEnumerable<Member>> GetAsync()
        {
            return await _memberRepository.GetAsync();
        }

        public async Task<Member> GetAsync(int id)
        {
            return await _memberRepository.GetAsync(id);
        }

        public async Task<Member> PostAsync(Member value)
        {
            return await _memberRepository.PostAsync(value);
        }

        public async Task PutAsync(int id, Member value)
        {
            await _memberRepository.PutAsync(id, value);
        }

        public async Task DeleteAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
        }
    }
}
