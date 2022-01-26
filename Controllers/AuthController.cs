namespace gain_impact_chat_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  private readonly UserManager<UserModel> _userManager;

  public AuthController(UserManager<UserModel> userManager)
  {
    _userManager = userManager;
  }

  // Route - /Auth/Signup/
  // This is endpoint will create a new user in database
  [HttpPost]
  [Route("Signup")]
  public async Task<ActionResult<object>> Signup(SignupDto dto)
  {
    if (dto.Role != "Admin" && dto.Role != "User")
    {
      return BadRequest(new { code = "InvalidRole", error = "Role does not exists" });
    }

    UserModel user = new UserModel()
    {
      Email = dto.Email,
      FullName = dto.FullName,
      ProfilePic = dto.ProfilePic,
      Role = dto.Role,
      UserName = dto.Email,
    };
    try
    {
      var result = await _userManager.CreateAsync(user, dto.Password);

      if (!result.Succeeded)
        return BadRequest(new { code = "ValidationError", error = result.Errors });
      return Ok(new { succeeded = true, user = user });
    }
    catch (Exception)
    {
      return BadRequest(new { code = "ServerError", error = "Error occurred while creating the user" });
    }
  }

  // Route = /Auth/Signin
  // This method will Signin the user by Email and Password
  [HttpPost]
  [Route("Signin")]
  public async Task<ActionResult> Signin(SigninDto dto)
  {
    var user = await _userManager.FindByEmailAsync(dto.Email);

    if (user is null)
    {
      return BadRequest(new { code = "EmailNotFound", error = "Email address is not found" });
    }

    var password = await _userManager.CheckPasswordAsync(user, dto.Password);

    if (!password)
    {
      return BadRequest(new { code = "IncorrectPassword", error = "Password is incorrect" });
    }

    return Ok(user);
  }
}