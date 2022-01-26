using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace gain_impact_chat_api.Data;

public class AuthContext : IdentityDbContext
{
  public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

  public DbSet<UserModel> Users { get; set; }
}