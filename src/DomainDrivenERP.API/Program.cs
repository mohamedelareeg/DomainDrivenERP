using System.Text.Json;
using System.Text.Json.Serialization;
using DomainDrivenERP.Application;
using DomainDrivenERP.Infrastructure;
using DomainDrivenERP.MiddleWares;
using DomainDrivenERP.Persistence;
using DomainDrivenERP.Presentation.Configuration.Extensions.Swagger;
using Serilog;
#region DI
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<GlobalExceptionHandlerMiddleWare>();
builder
    .Services
    .AddControllers()

    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .AddApplicationPart(DomainDrivenERP.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationDependencies()
                .AddInfrustructureDependencies()
                .AddPersistenceDependencies(builder.Configuration);

builder.Host.UseSerilog((context, loggerConfig) =>
loggerConfig.ReadFrom.Configuration(context.Configuration));

#endregion
#region MiddleWare
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerDocumentation();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
