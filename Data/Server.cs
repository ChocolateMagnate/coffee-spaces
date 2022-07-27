using Microsoft.AspNetCore.Mvc;
namespace spider_web.Data;
[ApiController]
//Base class for all back-end interactions. Contains the basic methods for CRUD operations.
public class Server: ControllerBase {
    //Logs user in with token-based authentication.
    [HttpPost("/login")]
    public void Login(string username, string position, string password) {
        // ...
    }
    //Test method for verifying port connection. List of chars is the arbitrary value.
    [HttpGet("/fetch")]
    public IActionResult Fetch() {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        return Ok(new List<char> { 'a', 'b', 'c' });
    }
}
