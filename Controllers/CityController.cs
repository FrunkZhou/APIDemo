using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo.Models;
using APIDemo.Models.Database;
using APIDemo.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepo;
        private readonly IMapper mapper;

        public CityController(ICityRepository cityRepo, IMapper mapper)
        {
            this.cityRepo = cityRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<City>> GetAll()
        {
            var cities = cityRepo.GetAllAsync().Result;
            return Ok(cities);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<City>> GetById(string id)
        {
            var city = await cityRepo.GetByIdAsync(id);
           
            if (city != null)
            {
                return Ok(city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CityAPICreate>> Create(CityAPICreate city)
        {
            var mappedCity = mapper.Map<City>(city);
            var success = await cityRepo.CreateAsync(mappedCity); 
            if (success)
            {
                return CreatedAtRoute(nameof(GetById), new { ID = mappedCity.ID }, city);
            }
            return BadRequest();
        }

        [HttpPut("{id}", Name = "Update")]
        public async Task<ActionResult<CityAPIUpdate>> Update(CityAPIUpdate city, string id)
        {
            var originalCity = await cityRepo.GetByIdAsync(id);
            var mappedCity = mapper.Map<City>(city);
            mappedCity.ID = id;
            mappedCity.Name = originalCity.Name;
            var success = await cityRepo.UpdateAsync(mappedCity);
            if (success)
            {
                return CreatedAtRoute(nameof(GetById), new { ID = mappedCity.ID }, city);
            }
            return NotFound();
        }

        [HttpDelete("{id}", Name = "Delete")]
        public async Task<ActionResult<City>> Delete(string id)
        {
            var originalCity = await cityRepo.GetByIdAsync(id);
            var success = await cityRepo.DeleteAsync(originalCity);
            if (success)
            {
                return Ok(originalCity);
            }
            return NotFound();
        }
    }
}
