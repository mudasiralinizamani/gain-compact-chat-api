namespace gain_impact_chat_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly UserManager<UserModel> _userManager;

  public UserController(UserManager<UserModel> userManager)
  {
    _userManager = userManager;
  }

  // This method will return all Users from database
  [HttpGet]
  [Route("GetUsers")]
  public async Task<ActionResult> GetUsers()
  {
    return Ok(await _userManager.Users.ToListAsync());
  }

  
  // This mehtod will return one User by Id
  [HttpGet]
  [Route("GetUser/{id}")]
  public async Task<ActionResult> GetUser(string id)
  {
    var user = await _userManager.FindByIdAsync(id);

    if (user is null)
      return BadRequest(new { code = "UserNotFound", error = "User is not found" });

    return Ok(user);
  }
}