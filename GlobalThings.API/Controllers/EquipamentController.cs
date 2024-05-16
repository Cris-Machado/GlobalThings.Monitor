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
    public class EquipamentController : Controller
    {
        private readonly IEquipamentService _equipamentService;

        public EquipamentController(IEquipamentService sensorService)
        {
            _equipamentService = sensorService;
        }

        #region ## Searches
        [SwaggerOperation("Listar todos os equipamentos")]
        [ProducesResponseType(typeof(List<EquipamentModel>), (int)HttpStatusCode.OK)]
        [HttpGet("get-all-equipament")]
        public async Task<IActionResult> GetAllEquipaments()
        {
            try
            {
                return Ok(await _equipamentService.GetAllEquipaments());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Listagem de sensores trazendo as ultimas dez medições")]
        [ProducesResponseType(typeof(List<EquipamentModel>), (int)HttpStatusCode.OK)]
        [HttpGet("get-report-sensor-equipament")]
        public async Task<IActionResult> GetReportSensorByEquipament([FromQuery] string equipamentId)
        {
            try
            {
                return Ok(await _equipamentService.ReportSensorByEquipament(equipamentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region ## Methods
        [SwaggerOperation("Criar equipamento")]
        [ProducesResponseType(typeof(EquipamentModel), (int)HttpStatusCode.OK)]
        [HttpPost("create-equipament")]
        public async Task<IActionResult> CreateEquipament([FromBody] EquipamentDto dto)
        {
            try
            {
                var model = new EquipamentModel
                {
                    Name = dto.Name
                };
                return Ok(await _equipamentService.CreateEquipament(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Atualizar equipamento")]
        [ProducesResponseType(typeof(EquipamentModel), (int)HttpStatusCode.OK)]
        [HttpPut("update-equipament")]
        public async Task<IActionResult> UpdateEquipament([FromBody] EquipamentDto dto, [FromQuery] string id)
        {
            try
            {
                var model = new EquipamentModel { 
                    Id = id,
                    Name = dto.Name 
                };
                return Ok(await _equipamentService.UpdateEquipament(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation("Excluir equipamento")]
        [ProducesResponseType(typeof(EquipamentModel), (int)HttpStatusCode.OK)]
        [HttpDelete("delete-equipament")]
        public async Task<IActionResult> DeleteEquipament([FromQuery] string equipamentId)
        {
            try
            {
                return Ok(await _equipamentService.DeleteEquipament(equipamentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
