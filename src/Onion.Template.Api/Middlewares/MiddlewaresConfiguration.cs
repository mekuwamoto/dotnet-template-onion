namespace Onion.Template.Api.Middlewares;

public static class MiddlewaresConfiguration
{
    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseHttpsRedirection();
        builder.UseAuthorization();
        return builder;
    }

    public static IApplicationBuilder UseSwaggerServices(this IApplicationBuilder builder)
    {
        builder.UseSwagger();

        return builder;
    }
}
