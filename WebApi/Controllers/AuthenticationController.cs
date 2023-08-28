using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
/* Authenticatie Cotroller:
 * Wie ben jij? 
 * En ben jij wie je zegt dat je bent.
 */
public class AuthenticationController : ControllerBase

{
    // Toegang tot de instellingen
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }
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
        var user = ValidateCredentials(data);
        if(user is null)
        {
            return Unauthorized();
        }
        
    }

    // Creer token omdat user niet null is.
    private string GenerateToken(UserData user)
    {
        // Authentication in User Secrets en het ophalen door IConfiguratie

        // Byte array wordt ingebracht in de symmetricSecurityKey als argument.
        // en creert een nieuwe key.
        var secretKey = new SymmetricSecurityKey(
            // conversie van string in Byte Array.
            Encoding.ASCII.GetBytes(
                // Krijg van instellingen onder Authentication de SecretKey waarde in een string.
                _config.GetValue<string>("Authentication: SecretKey")));

    }
    /* Validatie process: 
     * door derde partij Azure Active Directory of Auth0: DataBase check!
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
