using AutoMapper;
using TestTask.BLL.Interfaces;
using TestTask.BLL.Models;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;
using TestTask.Domain.Interfaces;

namespace TestTask.BLL.Services;

public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMapper mapper, IDateTimeProvider dateTimeProvider, IMessageRepository messageRepository)
    {
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
        _messageRepository = messageRepository;
    }

    public Task CreateMessage(MessageModel messageModel, CancellationToken ct)
    {
        var messageEntity = _mapper.Map<MessageEntity>(messageModel);
        messageEntity.CreatedAt = _dateTimeProvider.GetDateTime();
        return _messageRepository.CreateMessage(messageEntity, ct);
    }

    public async Task<IReadOnlyCollection<MessageModel>> GetMessages(DateTime start, DateTime end, CancellationToken ct)
    {
        var messageEntities = await _messageRepository.GetMessages(start, end, ct);
        return _mapper.Map<List<MessageModel>>(messageEntities);
    }
}
