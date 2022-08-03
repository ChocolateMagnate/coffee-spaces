using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Cors;

namespace spider_web.Data;
[ApiController]
[EnableCors()]
//Base class for all back-end interactions. Contains the basic methods for CRUD operations.
public class Server: ControllerBase {
    //Logs user in with token-based authentication.
    public Server() {
        StringValues token = new StringValues("*");
        Response.Headers.Add("Access-Control-Allow-Origin", token);
    }
    [HttpPost("/login")]
    public void Login(string username, string position, string password) {
        // ...
    }
    //Test method for verifying port connection. List of chars is the arbitrary value.
    [HttpGet("/fetch")]
    public IActionResult Fetch() {
        //Response.Headers.Add("Access-Control-Allow-Origin", "*");
        //HttpContext.Request.Headers["Access-Control-Allow-Origin"] = "http://localhost:4200";
        return Ok(new List<char> { 'a', 'b', 'c' });
    }
}
