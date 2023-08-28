using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName);

    // api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        //valideren van onze data door derde partij authenticatie provider in productie
        if(CompareValues(data.UserName, "whmuijs") &&
            CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, data.UserName!);
        }

        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        if(actual is not null)
        {
            if (actual.Equals(expected, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
