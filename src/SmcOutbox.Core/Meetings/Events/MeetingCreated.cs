using SmcOutbox.Core.Common;

namespace SmcOutbox.Core.Meetings.Events;

public record MeetingCreated(Guid MeetingId) : IDomainEvent;