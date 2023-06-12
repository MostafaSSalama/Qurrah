using Microsoft.EntityFrameworkCore;
using Qurrah.Data;
using Qurrah.Data.Repository;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Web.APIs.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Database
builder.Services.AddDbContext<QurrahDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QurrahConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Automapper
builder.Services.AddAutoMapper(typeof(MappingConfiguration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
