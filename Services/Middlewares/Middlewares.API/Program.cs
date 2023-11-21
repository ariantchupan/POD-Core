using Microsoft.Extensions.Configuration;
using Middlewares.API.Extensions;
using Middlewares.Infrastructure;


// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle




try
{
  
    var builder = WebApplication.CreateBuilder(args);
      var app = builder
        .ConfigureServices(builder.Configuration)
        .ConfigurePipeline();
	app.UseCors(builder =>
		builder
			.AllowCredentials()
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
	app.Run();
}
// https://github.com/dotnet/runtime/issues/60600
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
	Console.WriteLine(ex.ToString(), "Unhandled exception");
}
finally
{
    Console.WriteLine("Shut down complete");
    
}

