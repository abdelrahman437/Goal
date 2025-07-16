using Microsoft.AspNetCore.Mvc;
using Goal.Core.DTO;
using Goal.Core.Models;
using Goal.Core.UnitOfWork;
using AutoMapper;
using Goal.Core.Helpers;
using Goal.Core.Specifications;
using Goal.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Goal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("top-sales")]
        public async Task<IActionResult> Specials(int SizeOfPage, int NumberOfPage)
        {
            try
            {
                return Ok(await _productService.Specials(SizeOfPage, NumberOfPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Products")]
        public async Task<IActionResult> GetProduct([FromQuery] ProductQueryParameters productQueryParameters)
        {
            try
            {
                return Ok(await _productService.GetAll(productQueryParameters));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Single/{id:int}")]
        public  async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return Ok(await _productService.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore(int id, int Quntity)
        {
            try
            {
                await _productService.Restore(id,Quntity);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateProductDTO product)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid product data. ");
            try
            {
                await _productService.Update(id, product);
                return Ok("Product Has Been Updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An unexpected error occurred");
            }
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(int take, string name)
        {
            return Ok(await _productService.Search(take, name));
        }
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] AddProductDTO productDTO)
        {
            try
            {
                await _productService.Add(productDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
