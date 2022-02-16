using joyjet_interview_test.Factories;
using joyjet_interview_test.Factories.DiscountType;
using joyjet_interview_test.Interfaces.Factories;
using joyjet_interview_test.Interfaces.Factories.DiscountType;
using joyjet_interview_test.Interfaces.Services;
using joyjet_interview_test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ioc Registers
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddSingleton<IDiscountByAmount, DiscountByAmount>();
builder.Services.AddSingleton<IDiscountByPercentage, DiscountByPercentage>();
builder.Services.AddSingleton<IDiscountTypeFactory, DiscountTypeFactory>();

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
