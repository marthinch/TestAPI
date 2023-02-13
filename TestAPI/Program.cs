using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Repositories;
using TestAPI.Services;
using TestAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<TestAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestAPIContext") ?? throw new InvalidOperationException("Connection string 'TestAPIContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<ICarLocationService, CarLocationService>();
builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();

// Uncomment this code to perform migration in runtime
//DatabaseManagementService.MigrationInitialisation(app);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();