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
    public class SensorController : Controller
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        #region ## Searches
        [SwaggerOperation("Listar todos os sensores")]
        [ProducesResponseType(typeof(List<SensorModel>), (int)HttpStatusCode.OK)]
        [HttpGet("get-all-sensors")]
        public async Task<IActionResult> GetAllSensors()
        {
            try
            {
                return Ok(await _sensorService.GetAllSensors());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region ## Methods
        [SwaggerOperation("Criar sensor")]
        [ProducesResponseType(typeof(SensorModel), (int)HttpStatusCode.OK)]
        [HttpPost("create-sensor")]
        public async Task<IActionResult> CreateSensor([FromBody] SensorModel model)
        {
            try
            {
                return Ok(await _sensorService.CreateSensor(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Vincular sensor a um equipamento")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [HttpPost("bind-sensor-equipament")]
        public async Task<IActionResult> BindSensorToEquipament([FromQuery] string equipamentId, [FromQuery] string sensorId)
        {
            try
            {
                return Ok(await _sensorService.BindSensorToEquipament(sensorId, equipamentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Atualizar sensor")]
        [ProducesResponseType(typeof(SensorModel), (int)HttpStatusCode.OK)]
        [HttpPut("update-sensor")]
        public async Task<IActionResult> UpdateSensor([FromBody] SensorModel model)
        {
            try
            {
                return Ok(await _sensorService.UpdateSensor(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Excluir sensor")]
        [ProducesResponseType(typeof(SensorModel), (int)HttpStatusCode.OK)]
        [HttpDelete("delete-sensor")]
        public async Task<IActionResult> DeleteSensor([FromQuery] string sensorId)
        {
            try
            {
                return Ok(await _sensorService.DeleteSensor(sensorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
