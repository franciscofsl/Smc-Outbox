using MediatR;
using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings;

namespace SmcOutbox.Application.Meetings.Commands.Create;

public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand>
{
    private readonly IRepository<Meeting> _repository;

    public CreateMeetingCommandHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = new Meeting(Guid.NewGuid(), request.Code, request.Date);

        await _repository.InsertAsync(meeting);
    }
}