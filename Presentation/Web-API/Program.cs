using Application.Validators.Products;
using Domain.Entities.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Filters;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices();
builder.Services.AddCors(options =>
       options.AddPolicy("default", builder =>
           builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration =>
{
    configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
})
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //    //options.RoutePrefix = string.Empty;
    //    options.RoutePrefix = "Swagger";
    //});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("default");

app.MapControllers();

app.Run();