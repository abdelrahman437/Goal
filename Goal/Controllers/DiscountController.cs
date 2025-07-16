
using AutoMapper;
using Goal.Core.Models;
using Goal.Core.UnitOfWork;
using Goal.Core.DTO;
using Goal.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Goal.Core.Interfaces.Services;

namespace Goal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountServices _discountServices;

        public DiscountController(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery]DiscountQueryParameters queryParameters)
        {
            return Ok(await _discountServices.GetAll(queryParameters));
        }
        [HttpGet]
        [Route("Single/{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                return Ok(await _discountServices.GetById(id,false));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] DiscountDTO discount)
        {
            try
            {
                await _discountServices.Update(id, discount);
                return Ok("Discount Has Been Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] DiscountDTO discount)
        {
            
            try
            {
                await _discountServices.Add(discount);
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
                await _discountServices.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore(int id, int Quntity)
        {
            try
            {
                await _discountServices.Restore(id);
                return Ok();
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }

        }
    }
}
