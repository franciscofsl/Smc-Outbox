using Microsoft.AspNetCore.Mvc;
using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings;

namespace SmcOutbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingsController(IRepository<Meeting> repository) : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync([FromForm] CreateMeetingDto request)
    {
        var meeting = new Meeting(Guid.NewGuid(), request.Code, request.Date);
        await repository.InsertAsync(meeting);
    }
}