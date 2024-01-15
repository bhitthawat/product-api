using Microsoft.AspNetCore.Mvc;
using ThanachartAPI.Db;
using ThanachartAPI.Dto;

namespace ThanachartAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ModelContext modelContext;

        public AppController(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }

        [HttpGet("products")]
        public IActionResult GetAllProduct()
        {
            return Ok(modelContext.Products.ToList());
        }

        [HttpPost("verify")]
        public IActionResult VerifyProductStock([FromBody] List<Input> inputList)
        {
            try
            {
                var isValid = inputList.All(input =>
                 {
                     var entity = modelContext.Products.FirstOrDefault(x => x.Id == input.Id);
                     if (entity == null)
                     {
                         throw new Exception();
                     }

                     return entity.Quantity >= input.Quantity;
                 });

                if (!isValid)
                {
                    throw new Exception();
                }
                return Ok();
            }

            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("products")]
        public IActionResult InsertProduct([FromBody] List<Input> inputList)
        {
            try
            {
                inputList.ForEach(input =>
            {
                var entity = modelContext.Products.FirstOrDefault(x => x.Id == input.Id);
                if (entity == null || input.Quantity < 0 || entity.Quantity < input.Quantity)
                {
                    throw new Exception();
                }

                entity.Quantity = entity.Quantity - input.Quantity;
            });


                modelContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
