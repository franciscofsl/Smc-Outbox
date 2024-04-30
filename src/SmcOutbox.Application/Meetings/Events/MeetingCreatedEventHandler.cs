using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings;
using SmcOutbox.Core.Meetings.Events;

namespace SmcOutbox.Application.Meetings.Events;

public class MeetingCreatedEventHandler : IEventHandler<MeetingCreated>
{
    private readonly IRepository<Meeting> _repository;

    public MeetingCreatedEventHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    public async Task Handle(MeetingCreated request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.GetAsync(request.MeetingId);

        meeting.Processed = true;

        await _repository.UpdateAsync(meeting);
    }
}