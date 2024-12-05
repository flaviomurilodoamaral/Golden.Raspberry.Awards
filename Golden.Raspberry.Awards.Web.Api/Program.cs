using Golden.Raspberry.Awards.Business.Interface;
using Golden.Raspberry.Awards.Business;
using Golden.Raspberry.Awards.Repository.Interface;
using Golden.Raspberry.Awards.Repository;
using Golden.Raspberry.Awards.Service.Interface;
using Golden.Raspberry.Awards.Service;
using Golden.Raspberry.Awards.Web.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IMovieBusiness, MovieBusiness>();

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<SchemaFilterHelper>();
    options.OperationFilter<OperationFilterHelper>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
