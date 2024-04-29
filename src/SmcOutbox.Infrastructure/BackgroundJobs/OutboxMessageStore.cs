using Microsoft.EntityFrameworkCore;
using SmcOutbox.Data;
using SmcOutbox.Data.Outbox;

namespace SmcOutbox.Infrastructure.BackgroundJobs;

public sealed class OutboxMessageStore(AppDbContext dbContext)
{
    public const int MaxMessages = 20;

    public async Task<List<OutboxMessage>> GetNotProcessedMessagesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<OutboxMessage>()
            .Where(_ => !_.ProcessedOn.HasValue)
            .Take(MaxMessages)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}