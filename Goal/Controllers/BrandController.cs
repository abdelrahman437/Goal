using Goal.Core.DTO;
using Goal.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandServices _brandServices;

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] string? name)
        {
            return Ok(await _brandServices.GetAll(name));
        }
        [HttpGet]
        [Route("Single/{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                return Ok(await _brandServices.GetById(id,false));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] BrandDTO brandDTO)
        {
            try
            {
                await _brandServices.Update(id, brandDTO);
                return Ok("Discount Has Been Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] BrandDTO brandDTO)
        {

            try
            {
                await _brandServices.Add(brandDTO);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {
            try
            {
                await _brandServices.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _brandServices.Restore(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
