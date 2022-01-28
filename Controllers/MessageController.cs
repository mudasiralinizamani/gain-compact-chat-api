namespace gain_impact_chat_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
  private readonly IMessage _messageService;
  private readonly UserManager<UserModel> _userManager;

  public MessageController(IMessage messageService, UserManager<UserModel> userManager)
  {
    _messageService = messageService;
    _userManager = userManager;
  }

  [HttpPost]
  [Route("Create")]
  public async Task<ActionResult<object>> CreateMessage(CreateMessageDto dto)
  {
    UserModel sender = await _userManager.FindByIdAsync(dto.SenderId);

    if (sender is null)
      return BadRequest(new { code = "SenderNotFound", error = "Message sender is not found" });

    UserModel receiver = await _userManager.FindByIdAsync(dto.ReceiverId);

    if (receiver is null)
      return BadRequest(new { code = "ReceiverNotFound", error = "Message receiver is not found" });

    try
    {
      MessageModel model = await _messageService.CreateAsync(dto.Message, sender.Id, receiver.Id);
      return Ok(new { succeeded = true, message = model });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while creating message" });
    }
  }

  [HttpGet]
  [Route("GetBySenderId/{sender_id}")]
  public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessagesBySenderId(string sender_id)
  {
    try
    {
      UserModel sender = await _userManager.FindByIdAsync(sender_id);

      if (sender is null)
        return BadRequest(new { code = "SenderNotFound", error = "Sender is not found" });

      return Ok(await _messageService.FindBySenderIdAsync(sender.Id));
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding messages" });

    }
  }

  [HttpGet]
  [Route("GetAll")]
  public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllMessages()
  {
    return Ok(await _messageService.FindAllMessages());
  }

  [HttpGet]
  [Route("GetByReceiverId/{receiver_id}")]
  public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessagesByReceiverId(string receiver_id)
  {
    try
    {
      UserModel receiver = await _userManager.FindByIdAsync(receiver_id);

      if (receiver is null)
        return BadRequest(new { code = "ReceiverNotFound", error = "Receiver is not found" });

      return Ok(await _messageService.FindByReceiverIdAsync(receiver.Id));
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while finding messages" });
    }
  }

  [HttpPost]
  [Route("GetMessages")]
  public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessages(FindMessagesDto dto)
  {
    UserModel receiver = await _userManager.FindByIdAsync(dto.ReceiverId);

    if (receiver is null)
      return BadRequest(new { code = "ReceiverNotFound", error = "Receiver is not found" });

    UserModel sender = await _userManager.FindByIdAsync(dto.SenderId);

    if (receiver is null)
      return BadRequest(new { code = "SenderNotFound", error = "Sender is not found" });

    return Ok(await _messageService.FindMessages(receiver.Id, sender.Id));
  }

  [HttpGet]
  [Route("GetByUserId/{user_id}")]
  public async Task<ActionResult<object>> GetMessagesByUserId(string user_id)
  {
    UserModel user = await _userManager.FindByIdAsync(user_id);

    if (user is null)
      return BadRequest(new { code = "UserNotFound", error = "User is not found" });

    return Ok(await _messageService.FindByUserId(user_id));
  }
}
