using GlobalThings.Domain.Entities;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GlobalThings.API.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class MeasurementController : Controller
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        #region ## Searches
        [SwaggerOperation("Listar todos as medições")]
        [ProducesResponseType(typeof(List<MeasurementModel>), (int)HttpStatusCode.OK)]
        [HttpGet("get-all-measurements")]
        public async Task<IActionResult> GetAllMeasurements()
        {
            try
            {
                return Ok(await _measurementService.GetAllMeasurements());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region ## Methods
        [SwaggerOperation("Criar medição")]
        [ProducesResponseType(typeof(MeasurementModel), (int)HttpStatusCode.OK)]
        [HttpPost("create-measurement")]
        public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementDto dto)
        {
            try
            {
                var model = new MeasurementModel
                {
                    Value = dto.Value,
                    DateTime = DateTime.Now,
                };
                return Ok(await _measurementService.CreateMeasurement(model, dto.SensorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
