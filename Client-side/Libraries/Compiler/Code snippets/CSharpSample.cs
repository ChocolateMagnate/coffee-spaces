//This file is used to test C# code analysis and does not convey any meaning to the app itself.
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using coffee_spaces;
public static class ProgramClass{
    static async Task Main(string[] args){
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("app");
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        await builder.Build().RunAsync();
    }
}