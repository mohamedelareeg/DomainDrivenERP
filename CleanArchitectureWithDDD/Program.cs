#region DI
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

#endregion
#region MiddleWare
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
