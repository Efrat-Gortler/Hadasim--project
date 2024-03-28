using AutoMapper;
using HMO.API.Models;
using HMO.Core.DTOs;
using HMO.Core.Entity;
using HMO.Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationService _vaccinationService;
        private readonly IMapper _mapper;
        public VaccinationController(IVaccinationService vaccinationService, IMapper mapper)
        {
            _vaccinationService = vaccinationService;
            _mapper = mapper;
        }
        // GET: api/<VaccinationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccination>>> Get()
        {
            var list = await _vaccinationService.GetAsync();
            var listDto = _mapper.Map<IEnumerable<VaccinationDto>>(list);
            return Ok(listDto);
        }
        // GET api/<VaccinationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccination>> Get(int id)
        {
            var vaccination = await _vaccinationService.GetAsync(id);
            var vaccinationDto = _mapper.Map<VaccinationDto>(vaccination);
            return Ok(vaccinationDto);
        }
        // POST api/<VaccinationController>
        [HttpPost]
        public async Task<ActionResult<Vaccination>> Post([FromBody] VaccinationPostModel value)
        {
            var vaccinationToAdd = _mapper.Map<Vaccination>(value);
            await _vaccinationService.PostAsync(vaccinationToAdd);
            var vaccinationDto = _mapper.Map<VaccinationDto>(vaccinationToAdd);
            return Ok(vaccinationDto);
        }

        // PUT api/<VaccinationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VaccinationPostModel value)
        {
            Vaccination existVaccination = await _vaccinationService.GetAsync(id);

            if (existVaccination is null)
            {
                return NotFound();
            }
            _mapper.Map(value, existVaccination);

            await _vaccinationService.PutAsync(id, existVaccination);

            return Ok(_mapper.Map<VaccinationDto>(existVaccination));
        }

        // DELETE api/<VaccinationController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vaccinationService.DeleteAsync(id);
        }
    }
}
