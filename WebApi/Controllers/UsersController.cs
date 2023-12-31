using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Constants;

namespace WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;

    public UsersController(IConfiguration config)
    { 
        _config = config;
    }

    // GET: api/Users
    [HttpGet]
    [Authorize(Policy = Policies.Title)]
    public IEnumerable<string> Get()
    {
        List<string> output = new();
        for(int i = 1; i < Random.Shared.Next(2,20); i++)
        {
            output.Add($" Value #{i}");
        }
        return output;
    }

    // GET: api/Users/5
    [HttpGet("{id}", Name = "Get")]
    [Authorize(Policy = Policies.MustHaveStudentsId)] // Autorisatie: Moet een studentId hebben.
    public string Get(int id)
    {
        return _config.GetConnectionString("Default");
    }

    // POST: api/Users
    [HttpPost]
    public void Post([FromBody] string value)
    { 
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
