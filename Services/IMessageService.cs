namespace gain_impact_chat_api.Services;

public class IMessageService : IMessage
{
  private readonly ApiContext _context;

  public IMessageService(ApiContext context)
  {
    _context = context;
  }

  public async Task<MessageModel> CreateAsync(string message, string senderId, string receiverId)
  {
    ArgumentNullException.ThrowIfNull(message);
    ArgumentNullException.ThrowIfNull(senderId);
    ArgumentNullException.ThrowIfNull(receiverId);

    MessageModel model = new()
    {
      CreatedAt = DateTime.Now,
      Id = Guid.NewGuid().ToString(),
      Message = message,
      ReceiverId = receiverId,
      SenderId = senderId,
      UpdatedAt = DateTime.Now,
    };

    await _context.Messages.AddAsync(model);
    await _context.SaveChangesAsync();
    return model;
  }

  public async Task<IEnumerable<MessageModel>> FindAllMessages()
  {
    return await _context.Messages.ToListAsync<MessageModel>();
  }

  public async Task<MessageModel?> FindByIdAsync(string id)
  {
    ArgumentNullException.ThrowIfNull(id);

    return await _context.Messages.Where(m => m.Id == id).FirstOrDefaultAsync<MessageModel>();
  }

  public async Task<IEnumerable<MessageModel>> FindByReceiverIdAsync(string receiverId)
  {
    ArgumentNullException.ThrowIfNull(receiverId);

    return await _context.Messages.Where(m => m.ReceiverId == receiverId).ToListAsync<MessageModel>();
  }

  public async Task<IEnumerable<MessageModel>> FindBySenderIdAsync(string senderId)
  {
    ArgumentNullException.ThrowIfNull(senderId);

    return await _context.Messages.Where(m => m.SenderId == senderId).ToListAsync<MessageModel>();
  }

  public async Task<IEnumerable<MessageModel>> FindByUserId(string userId)
  {
    ArgumentNullException.ThrowIfNull(userId);
    return await _context.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId).ToListAsync<MessageModel>();
  }

  public async Task<IEnumerable<MessageModel>> FindMessages(string receiverId, string senderId)
  {
    ArgumentNullException.ThrowIfNull(senderId);
    ArgumentNullException.ThrowIfNull(receiverId);

    return await _context.Messages.Where(m => m.ReceiverId == receiverId && m.SenderId == senderId || m.ReceiverId == senderId && m.SenderId == receiverId).ToListAsync<MessageModel>();
  }

  public MessageModel UpdateReply(MessageModel message, string reply)
  {
    ArgumentNullException.ThrowIfNull(reply);
    ArgumentNullException.ThrowIfNull(message);

    message.Reply = reply;
    _context.SaveChanges();
    return message;
  }
}