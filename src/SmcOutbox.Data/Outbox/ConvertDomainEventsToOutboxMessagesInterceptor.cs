using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using SmcOutbox.Core.Common;

namespace SmcOutbox.Data.Outbox;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var outboxMessages = GetOutboxMessages(dbContext);

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static List<OutboxMessage> GetOutboxMessages(DbContext dbContext)
    {
        return dbContext.ChangeTracker
            .Entries<WithDomainEvents>()
            .Select(_ => _.Entity)
            .SelectMany(_ => _.Events)
            .Select(_ => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Type = _.GetType().Name,
                Content = JsonConvert.SerializeObject(_, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            })
            .ToList();
    }
}