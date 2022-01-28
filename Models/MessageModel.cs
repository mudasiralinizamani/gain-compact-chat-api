namespace gain_impact_chat_api.Models;

public class MessageModel
{
  public string Id { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public string Reply { get; set; } = string.Empty;
  public string SenderId { get; set; } = string.Empty;
  public string ReceiverId { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}