using Microsoft.AspNetCore.Mvc;

namespace spider_web.Data;
[ApiController]
public class Server: ControllerBase {
    [HttpPost("/login")]
    public void Login(string username, string position, string password) {
        // ...
    }
    [HttpGet("/fetch")]
    public IActionResult Fetch() {
        return Ok(new List<char> { 'a', 'b', 'c' });
    }
}
