using Application.Command.People;
using Application.CustomAttribute;
using Application.Query.People;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreatePeopleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPeople()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePeopleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePeopleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
