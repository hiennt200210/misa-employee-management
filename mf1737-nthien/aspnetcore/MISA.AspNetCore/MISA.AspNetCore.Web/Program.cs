using MISA.AspNetCore.Application;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Infrastructure;
using MISA.AspNetCore.Web;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corspolicy", build =>
    {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

// Get connection string
builder.Services.AddTransient<IDbConnectionService>(provider => new DbConnectionService(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
