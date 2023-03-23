using DevStudyNotes.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DevStudyNotes");
builder.Services.AddDbContext<StudyNoteDbContext>(
    e => e.UseSqlServer(connectionString)
);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich
    .FromLogContext()
    .WriteTo
    .MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions()
    {
        AutoCreateSqlTable = true,
        TableName = "Logs"
    })
    .WriteTo.Console().CreateLogger();
}).UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(n =>
{
    n.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevStudyNotes.Api",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Silva",
            Email = "gabrielgbr.contato@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
        }
    });

    var xmlFile = "DevStudyNotes.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    n.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
