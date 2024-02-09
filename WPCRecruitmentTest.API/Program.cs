using WPCRecruitmentTest.Services.Interfaces;
using WPCRecruitmentTest.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICrimeService, CrimeService>();
builder.Services.AddScoped<DateHelper>();
builder.Services.AddScoped<LocationHelper>();
builder.Services.AddScoped<IApiCaller, ApiCaller>();

builder.Services.AddControllers();
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
