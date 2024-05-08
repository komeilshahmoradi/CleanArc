using Application.Command.Order;
using Application.CustomAttribute;
using Application.Query.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllOrderQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
