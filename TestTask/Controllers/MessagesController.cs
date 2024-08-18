using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask.API.DTO;
using TestTask.BLL.Interfaces;
using TestTask.BLL.Models;

namespace TestTask.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMessageService _messageService;

    public MessagesController(IMapper mapper, IMessageService messageService)
    {
        _mapper = mapper;
        _messageService = messageService;
    }

    // GET: api/<MessagesController>
    [HttpGet]
    public async Task<IEnumerable<MessageDto>> Get(DateTime start, DateTime end, CancellationToken ct)
    {
        var messages = await _messageService.GetMessages(start, end, ct);
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    // POST api/<MessagesController>
    [HttpPost]
    public Task Post([FromBody] CreateMessageDto message, CancellationToken ct)
    {
        var messageModel = _mapper.Map<MessageModel>(message);
        return _messageService.CreateMessage(messageModel, ct);
    }
}
