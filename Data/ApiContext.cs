namespace gain_impact_chat_api.Data;

public class ApiContext : DbContext
{
  public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

  public DbSet<MessageModel> Messages { get; set; }
}