using Goal.Core.DTO;
using Goal.Core.Interfaces.Services;
using Goal.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] string? name)
        {
            return Ok(await _categoryServices.GetAll(name));
        }
        [HttpGet]
        [Route("Single/{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                return Ok(await _categoryServices.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CategoryDTO categoryDTO)
        {
            try
            {
                await _categoryServices.Update(id, categoryDTO);
                return Ok("Discount Has Been Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] CategoryDTO categoryDTO)
        {

            try
            {
                await _categoryServices.Add(categoryDTO);
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
                await _categoryServices.Delete(id);
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
                await _categoryServices.Restore(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
