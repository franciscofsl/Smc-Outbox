using MediatR;

namespace SmcOutbox.Application.Meetings.Commands.Create;

public class CreateMeetingCommand : IRequest
{
    public string Code { get; init; }
    
    public DateTime Date { get; init; }
}