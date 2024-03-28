using AutoMapper;
using HMO.API.Models;
using HMO.Core.DTOs;
using HMO.Core.Entity;
using HMO.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace HMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MembersController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<MembersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAsync()
        {
            var members = await _memberService.GetAsync();
            var memberDtos = _mapper.Map<IEnumerable<MemberDto>>(members);
            return Ok(memberDtos);
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> Get(int id)
        {
            var member = await _memberService.GetAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            var memberDto = _mapper.Map<MemberDto>(member);
            return Ok(memberDto);
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<ActionResult<MemberDto>> Post([FromBody] MemberPostModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine(value);
            var memberToAdd = _mapper.Map<Member>(value);
            
            await _memberService.PostAsync(memberToAdd);
            var memberDto = _mapper.Map<MemberDto>(memberToAdd);
            return CreatedAtAction(nameof(Get), new { id = memberDto.Id }, memberDto);
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MemberDto>> Put(int id, [FromBody] MemberPostModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingMember = await _memberService.GetAsync(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            _mapper.Map(value, existingMember);
            await _memberService.PutAsync(id, existingMember);
            var updatedMemberDto = _mapper.Map<MemberDto>(existingMember);
            return Ok(updatedMemberDto);
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingMember = await _memberService.GetAsync(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            await _memberService.DeleteAsync(id);
            return NoContent();
        }
    }
}