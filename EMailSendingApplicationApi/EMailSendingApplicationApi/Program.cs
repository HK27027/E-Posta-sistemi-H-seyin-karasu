using EMailSending.Data;
using EMailSending.Interfaces;
using EMailSending.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IAccount, RAccount>();
builder.Services.AddSingleton<IUSers, RUser>();
builder.Services.AddSingleton<IEmails, REmails>();
builder.Services.AddSingleton<IEmailSent, REmailSent>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
ConfigurationManager configuration = builder.Configuration;
string mongoConStr = configuration.GetSection("Mongo:ConnectionString").Value;
string mongoConStrDatabase = configuration.GetSection("Mongo:Database").Value;

builder.Services.Configure<AppSettings>(o =>
{
    o.Mongo.ConnectionString = mongoConStr;
    o.Mongo.Database = mongoConStrDatabase;
});

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin() 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(); // Enable CORS

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
