
using Microsoft.OpenApi.Models;
using CleanArchitectureWithDDD.Application;
using CleanArchitectureWithDDD.Infrastructure;
using CleanArchitectureWithDDD.Persistence;
using CleanArchitectureWithDDD.Presentation;
using CleanArchitectureWithDDD.Presentation.Configuration.Extensions.Swagger;
#region DI
var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .AddApplicationPart(CleanArchitectureWithDDD.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationDependencies()
                .AddInfrustructureDependencies()
                .AddPersistenceDependencies(builder.Configuration);


#endregion
#region MiddleWare
var app = builder.Build();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
