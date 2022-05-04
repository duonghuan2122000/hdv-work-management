using HDV.Nhom2.EmailService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day));

// Add services to the container.
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("MailSettings"));

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
