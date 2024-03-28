using AutoMapper;
using HMO.API.Models;
using HMO.Core.DTOs;
using HMO.Core.Entity;
using HMO.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAsync()
        {
            var cities = await _cityService.GetAsync();
            var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(cityDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var city = await _cityService.GetAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            var cityDto = _mapper.Map<CityDto>(city);
            return Ok(cityDto);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Post([FromBody] CityPostModel model)
        {
            var cityToAdd = _mapper.Map<City>(model);
            await _cityService.PostAsync(cityToAdd);
            var cityDto = _mapper.Map<CityDto>(cityToAdd);
            return CreatedAtAction(nameof(Get), new { id = cityDto.Id }, cityDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CityPostModel model)
        {
            var existingCity = await _cityService.GetAsync(id);
            if (existingCity == null)
            {
                return NotFound();
            }

            // Update existingCity with the values from the model
            _mapper.Map(model, existingCity);
            await _cityService.PutAsync(id, existingCity);

            var updatedCityDto = _mapper.Map<CityDto>(existingCity);
            return Ok(updatedCityDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _cityService.GetAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            await _cityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
