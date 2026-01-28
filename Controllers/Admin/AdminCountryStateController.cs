using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCountryStateController : ControllerBase
    {
        private readonly IAdminInterfaces countryState_Service;
        public AdminCountryStateController(IAdminInterfaces adminInterfaces) {
            countryState_Service = adminInterfaces;
        }

        [HttpPost("addNewCountry")]
        public async Task<IActionResult> AddNewCountry([FromBody] AdminCountryAddModel data) {
            var countryData = await countryState_Service.AdminAddCountryAsync(data);
            if (countryData != null) { 
                  return Ok(countryData);
            }
            return BadRequest(countryData);
        }

        [HttpGet("getCountry/{id}")]
        public async Task<IActionResult> GetCountryById(Guid id) {
            var getCountry = await countryState_Service.AdminGetCountryById(id);
            if (getCountry != null)
            {
                return Ok(getCountry);
            }
            return BadRequest(getCountry);
        }

        [HttpGet("getState/{id}")]
        public async Task<IActionResult> GetStateById(Guid id)
        {
             var getState = await countryState_Service.AdminGetStateById(id);
            if (getState != null) return Ok(getState);
            return BadRequest(getState);
        }

        [HttpGet("getAllCountry")]
        public async Task<IActionResult> GetAllAdminCountries()
        {
            var getAllCountries = await countryState_Service.AdminGetAllCountryAsync();
            if (getAllCountries != null)
            {
                return Ok(getAllCountries);
            }
            return BadRequest(getAllCountries);
        }

        [HttpGet("getAllState")]
        public async Task<IActionResult> GetAllStates()
        {
            var getWholeStates = await countryState_Service.AdminGetAllStateAsync();
            if(getWholeStates != null) return Ok(getWholeStates);
            return BadRequest(getWholeStates);
        }

        [HttpPut("updateCountry/{id}")]
        public async Task<IActionResult> UpdateCountry(Guid id, AdminCountryAddModel data)
        {
            var updateCountry = await countryState_Service.AdminUpdateCountryById(id, data);
            if (updateCountry != null) return Ok(updateCountry);
            return BadRequest(updateCountry);
        }

        [HttpPut("updateState/{id}")]
        public async Task<IActionResult> UpdateState(Guid id, AdminStateAddModel req) {
            var updateSingleState = await countryState_Service.AdminUpdateStateById(id, req);
            if (updateSingleState != null) return Ok(updateSingleState);
            return BadRequest(updateSingleState);
        }

        [HttpPost("addNewState")]
        public async Task<IActionResult> AddNewState([FromBody] AdminStateAddModel req)
        {
            var stateData = await countryState_Service.AdminAddStatesAsync(req);
            if (stateData != null) {
                return Ok(stateData);
            }
            return BadRequest(stateData);
        }

        [HttpDelete("deleteCountry/{id}")]
        public async Task<IActionResult> deleteCountry(Guid id) { 
          var deleteSingleCountry = await countryState_Service.AdminDeleteCountryById(id);
          if (deleteSingleCountry != null) return Ok(deleteSingleCountry);
          return BadRequest(deleteSingleCountry);
        }

        [HttpDelete("deleteState/{id}")]
        public async Task<IActionResult> deleteState(Guid id)
        {
            var deleteSingleState = await countryState_Service.AdminDeleteStateById(id);
            if (deleteSingleState != null) return Ok(deleteSingleState);
            return BadRequest(deleteSingleState);
        }









    }
}
