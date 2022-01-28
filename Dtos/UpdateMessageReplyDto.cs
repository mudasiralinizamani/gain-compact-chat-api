namespace gain_impact_chat_api.Dtos;

public class UpdateMessageReplyDto
{
  public int MessageId { get; set; }
  public string Reply { get; set; } = string.Empty;
}