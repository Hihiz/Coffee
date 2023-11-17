using Coffee.Application.Products.Commands.CreateProduct;
using Coffee.Application.Products.Commands.DeleteProduct;
using Coffee.Application.Products.Commands.UpdateProduct;
using Coffee.Application.Products.Queries.GetProductDetail;
using Coffee.Application.Products.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => (_mediator) = (mediator);

        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            GetProductListQuery query = new GetProductListQuery();
            ProductListViewModel vm = await _mediator.Send(query);

            return Ok(vm.ProductList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductDetail(int id)
        {
            GetProductDetailQuery query = new GetProductDetailQuery(id);
            ProductDetailDto dto = await _mediator.Send(query);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            ProductDetailDto dto = await _mediator.Send(command);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            DeleteProductCommand command = new DeleteProductCommand(id);

            return Ok(await _mediator.Send(command));
        }
    }
}
