using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
