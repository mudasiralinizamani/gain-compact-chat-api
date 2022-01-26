namespace gain_impact_chat_api.Models;

// Extending the Identity User, So I can add some extra fields
public class UserModel : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime UpdateAt { get; set; }
  public string ProfilePic { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
}