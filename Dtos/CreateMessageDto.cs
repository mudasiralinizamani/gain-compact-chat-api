namespace gain_impact_chat_api.Dtos;

public class CreateMessageDto
{
  [Required]
  public string Message { get; set; } = string.Empty;
  [Required]
  public string SenderId { get; set; } = string.Empty;
  [Required]
  public string ReceiverId { get; set; } = string.Empty;
}