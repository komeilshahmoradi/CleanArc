using Application.Command.Product;
using Application.CustomAttribute;
using Application.Query.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
