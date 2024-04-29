using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings.Events;

namespace SmcOutbox.Core.Meetings;

public class Meeting : Aggregate
{
    private Meeting()
    {
    }

    public Meeting(Guid id, string code, DateTime date)
    {
        Id = id;
        Code = code;
        Date = date;

        RaiseDomainEvent(new MeetingCreated(id));
    } 

    public string Code { get; set; }

    public DateTime Date { get; set; }

    public bool Processed { get; set; }
}