namespace gain_impact_chat_api.Interfaces;

public interface IMessage
{
  Task<MessageModel> CreateAsync(string message, string senderId, string receiverId);

  Task<MessageModel?> FindByIdAsync(string id);

  Task<IEnumerable<MessageModel>> FindBySenderIdAsync(string senderId);

  Task<IEnumerable<MessageModel>> FindByReceiverIdAsync(string receiverId);

  Task<IEnumerable<MessageModel>> FindMessages(string receiverId, string senderId);

  Task<IEnumerable<MessageModel>> FindAllMessages();

  MessageModel UpdateReply(MessageModel message, string reply);

  Task<IEnumerable<MessageModel>> FindByUserId(string userId);
}