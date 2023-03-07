using Application.Accounts.UseCase.GetAccountsUseCase;
using Application.Accounts.UseCase.GetNextAccountCodeUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EntryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        [ProducesResponseType(typeof(GetAccountsUseCaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult<GetAccountsUseCaseResponse>> Get([FromQuery] string? ParentAccount = "", [FromQuery] int Sequence = 0)
        {
            var request = new GetAccountsUseCaseRequest(ParentAccount, Sequence);
            var result = await Mediator.Send(request);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [ProducesResponseType(typeof(GetNextAccountCodeUseCaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("Next")]
        public async Task<ActionResult<GetNextAccountCodeUseCaseResponse>> Next([FromQuery] string? ParentAccount)
        {
            var request = new GetNextAccountCodeUseCaseRequest(ParentAccount);
            var result = await Mediator.Send(request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(typeof(DeleteAccountUseCaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete()]
        public async Task<ActionResult<DeleteAccountUseCaseResponse>> Delete([FromBody] DeleteAccountUseCaseRequest request)
        {
            var result = await Mediator.Send(request);

            if (result.RowsAffected == 0)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(typeof(InsertOrUpdateAccountUseCaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost()]
        public async Task<ActionResult<InsertOrUpdateAccountUseCaseResponse>> Post([FromBody] InsertOrUpdateAccountUseCaseRequest request)
        {
            var result = await Mediator.Send(request);
            if (result.RowsAffected == 0)
                return NotFound();

            return Ok(result);
        }
    }
}