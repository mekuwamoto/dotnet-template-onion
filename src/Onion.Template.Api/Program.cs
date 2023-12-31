using Onion.Template.Api.Middlewares;
using Onion.Template.Api.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
        app.UseSwaggerServices();
    app.ConfigureMiddlewares();
    app.MapControllers();
    app.Run();
}
