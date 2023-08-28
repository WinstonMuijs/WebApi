using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
/* Authenticatie Cotroller:
 * Wie ben jij? 
 * En ben jij wie je zegt dat je bent.
 */
public class AuthenticationController : ControllerBase

{
    /* Een record om informatie vast te leggen over een gebruiker.
     * inlog gegevens zoals naam en paswoord.
    */
    public record AuthenticationData(string? UserName, string? Password);
    /* Als Authenticatie data klopt krijg je UserData terug */
    public record UserData(int UserId, string UserName);

    // api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        
    }
    /* Validatie process: 
     * door derde partij Azure Active Directory of Auth0
     * ingevoerde gegevens worden vergeleken met de verwachte waarden.
     * Als ze overeenkomen geef gebruiker Id en Naam, ander Null.
     */
    private UserData? ValidateCredentials(AuthenticationData data)
    {
        /*valideren van onze data door derde partij authenticatie provider in productie
         * In deze Demo setting zo hardcoded.
        */
        if(CompareValues(data.UserName, "whmuijs") &&
            CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, data.UserName!);
        }

        return null;
    }
    /*Methode of functie over de invoer waarden met de verwachte waarden tevergelijken.
     * Als ze ovvereenkomen geef true anders false.
     */
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
