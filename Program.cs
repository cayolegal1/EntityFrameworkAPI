using APITransferencias.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddNpgsql<APIContext>(builder.Configuration.GetConnectionString("API"));
builder.Services.AddNpgsql<APIContext>("User ID=postgres;Password=hola1606;Server=127.0.0.1;Port=5432;Database=entityframework;");

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddScoped<ITransferRepository, TransferRepository>();


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
