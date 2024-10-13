using CongestionTax.API;
using CongestionTaxCalculator.Service;
using CongestionTaxCalculator.Service.Contracts;
using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.Contracts.Dtos.TaxRule;
using CongestionTaxCalculator.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityTaxController : ControllerBase
    {
        private readonly ICityTaxRuleService _service;
        private readonly ILogger<CityTaxController> _logger;

        public CityTaxController(ILogger<CityTaxController> logger, ICityTaxRuleService service)
        {
            _service = service;
            _logger = logger;

        }
        [HttpPost("Add")]
        public async Task<ActionResult> AddCity(string name)
        {
            await _service.AddCity(name);
            return Ok();
        }

        [HttpPost("AddTaxRule")]
        public async Task<ActionResult> AddTaxRule(CityTaxRuleCreationDto dto)
        {
            await _service.AddTaxRules(dto);

            return Ok();
        }

        [HttpPost("GetTax")]
        public async Task<ActionResult> GetTax(string cityName, string[] dates, IVehicle vehicle)
        {
            List<DateTime> dateTimedates = [];
            dates.ToList().ForEach(c => dateTimedates.Add(DateTime.Parse(c)));
         

            return Ok(await _service.GetTax(cityName, vehicle, [.. dateTimedates]));
        }
    }
}
