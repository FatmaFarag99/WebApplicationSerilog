using Serilog;
using WebApplicationSerilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



Serilog.ILogger logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.Map("Name", "log", (name, wt) =>
    {
        var date = DateTime.Now;
        wt.File($"logs/{date.Year}/{date.Month}/{date.Day}/{date.Minute}/{name}.txt");
    })
    .CreateLogger();

logger.Information($"Date : {DateTime.Now} ");
builder.Host.UseSerilog(logger);



//builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
//                                     .WriteTo.Map("Name", "log", (name, wt) =>
//                                     {
//                                         var Date = DateTime.Now;
//                                         wt.File($"logs/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Minute}/logs-.txt",
//                                                                        rollingInterval: RollingInterval.Minute,
//                                                                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
//                                     }));





//builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
//                                       .WriteTo.File($"logs/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Minute}/logs-.txt",
//                                       rollingInterval: RollingInterval.Day,
//                                       outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"));



var app = builder.Build();

Serilog.ILogger log = app.Services.GetRequiredService<Serilog.ILogger>();
app.ConfigureExceptionHandler(log);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello 1 " + "\n");

//    await next.Invoke();
//    await context.Response.WriteAsync("Hello 3 " + "\n");

//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello 2");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Use2 " + "\n");

//    await next.Invoke();

//    await context.Response.WriteAsync("Use2-2" + "\n");

//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("xxxxx " + "\n");
//    next();
//});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
