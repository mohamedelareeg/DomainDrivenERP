using System.Text.Json.Serialization;
using CleanArchitectureWithDDD.Application;
using CleanArchitectureWithDDD.Infrastructure;
using CleanArchitectureWithDDD.MiddleWares;
using CleanArchitectureWithDDD.Persistence;
using CleanArchitectureWithDDD.Presentation.Configuration.Extensions.Swagger;
#region DI
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<GlobalExceptionHandlerMiddleWare>();
builder
    .Services
    .AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddApplicationPart(CleanArchitectureWithDDD.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationDependencies()
                .AddInfrustructureDependencies()
                .AddPersistenceDependencies(builder.Configuration);
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
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
