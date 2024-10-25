using Goodelivery.API.Endpoints;
using Goodelivery.BLL.Helpers;
using Goodelivery.DAL.Helpers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddEndpointsApiExplorer().AddSwaggerGen().AddControllers();
services.AddDataAccess(configuration).AddBusinessLogic();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.MapOrderEndpoints();
app.Run();