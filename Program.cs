using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<kpi_feedback_dbcontext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("kpi_feedback_connection_string")));

builder.Services.AddScoped<IKPI_repository, KPI_repository>();
builder.Services.AddScoped<IUser_repository, User_repository>();
builder.Services.AddScoped<IKPI_Assignment_repository, KPI_Assignment_repository>();
builder.Services.AddScoped<IKPI_Assessor_repository, KPI_Assessor_repository>();
builder.Services.AddScoped<IKPI_Assessor_Feedback_repository, KPI_Assessor_Feedback_Repository>();


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
