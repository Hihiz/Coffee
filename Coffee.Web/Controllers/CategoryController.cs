using Coffee.Application.Categories.Commands;
using Coffee.Application.Categories.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CategoryController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetCategory()
        {
            GetCategoryListQuery query = new GetCategoryListQuery();

            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryCommand command) => Ok(await _mediator.Send(command));
    }
}
