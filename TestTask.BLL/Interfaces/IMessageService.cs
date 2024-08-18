﻿using TestTask.BLL.Models;

namespace TestTask.BLL.Interfaces;

public interface IMessageService
{
    Task CreateMessage(MessageModel messageModel, CancellationToken ct);

    Task<IReadOnlyCollection<MessageModel>> GetMessages(DateTime start, DateTime end, CancellationToken ct);
}
