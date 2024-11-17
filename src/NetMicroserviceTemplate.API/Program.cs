using NetMicroserviceTemplate.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDomainServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureDataServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.MapAllEndpoints();

app.Run();
