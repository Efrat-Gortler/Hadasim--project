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
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _ManufacturerService;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerService ManufacturerService, IMapper mapper)
        {
            _ManufacturerService = ManufacturerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerDto>>> GetAsync()
        {
            var Manufacturers = await _ManufacturerService.GetAsync();
            var ManufacturerDtos = _mapper.Map<IEnumerable<ManufacturerDto>>(Manufacturers);
            return Ok(ManufacturerDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerDto>> Get(int id)
        {
            var Manufacturer = await _ManufacturerService.GetAsync(id);
            if (Manufacturer == null)
            {
                return NotFound();
            }
            var ManufacturerDto = _mapper.Map<ManufacturerDto>(Manufacturer);
            return Ok(ManufacturerDto);
        }

        [HttpPost]
        public async Task<ActionResult<ManufacturerDto>> Post([FromBody] ManufacturerPostModel model)
        {
            var ManufacturerToAdd = _mapper.Map<Manufacturer>(model);
            await _ManufacturerService.PostAsync(ManufacturerToAdd);
            var ManufacturerDto = _mapper.Map<ManufacturerDto>(ManufacturerToAdd);
            return CreatedAtAction(nameof(Get), new { id = ManufacturerDto.Id }, ManufacturerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ManufacturerDto>> Put(int id, [FromBody] ManufacturerPostModel model)
        {
            var existingManufacturer = await _ManufacturerService.GetAsync(id);
            if (existingManufacturer == null)
            {
                return NotFound();
            }

            // Update existingManufacturer with the values from the model
            _mapper.Map(model, existingManufacturer);
            await _ManufacturerService.PutAsync(id, existingManufacturer);

            var updatedManufacturerDto = _mapper.Map<ManufacturerDto>(existingManufacturer);
            return Ok(updatedManufacturerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Manufacturer = await _ManufacturerService.GetAsync(id);
            if (Manufacturer == null)
            {
                return NotFound();
            }
            await _ManufacturerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
