using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmcOutbox.Application.Meetings.Commands.Create;

namespace SmcOutbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task CreateAsync([FromForm] CreateMeetingDto request)
    {
        await _mediator.Send(new CreateMeetingCommand
        {
            Code = request.Code,
            Date = request.Date
        });
    }
}