using TestTask.DAL.Entities;

namespace TestTask.DAL.Interfaces;

public interface IMessageRepository
{
    Task<MessageEntity> CreateMessage(MessageEntity messageEntity, CancellationToken ct);

    Task<IReadOnlyCollection<MessageEntity>> GetMessages(DateTime start, DateTime end, CancellationToken ct);
}
