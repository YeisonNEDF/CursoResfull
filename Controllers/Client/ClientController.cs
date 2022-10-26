using Core.Features.Commands.CreateClientCommand;
using Core.Features.Commands.DeleteClientCommand;
using Core.Features.Commands.UpdateClientCommand;
using Core.Features.Queries.GetAllCients;
using Core.Features.Queries.GetClientById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Client
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllClientParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllClientQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre= filter. Nombre,
                Apellido= filter.Apellido,
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if(id != command.Id)
                return BadRequest("El id no es valido para ser actualizado");
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles= "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id}));
        }

    }
}
