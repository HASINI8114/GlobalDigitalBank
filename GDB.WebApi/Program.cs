using GDB.Core.Application.Services.Implementations;
using GDB.Core.Infrastructure.Repositories;
using GDB.Core.Infrastructure.Repositories.Contracts;
using GDB.Core.Infrastructure.Repositories.Implementations;
using GDB.Core.Application.Services;
using GDB.Core.Application.Services.Contracts;
using GDB.Core.Application.Services.Implementations;
using GDB.Core.Infrastructure.Repositories;
using GDB.Core.Infrastructure.Repositories.Contracts;
using GDB.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString =
    builder.Configuration.GetConnectionString("GDBConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'GDBConnection' not found.");

builder.Services.AddScoped<IDataBaseConnectionManager>(_ =>
    new DataBaseConnectionManager(connectionString));
builder.Services.AddScoped<IAccountRepository, AccountRepositoryDB>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepositoryDB>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionQueryService, TransactionQueryService>();
builder.Services.AddScoped<TransactionCommandFactory>();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

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
