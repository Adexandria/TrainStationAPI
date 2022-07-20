using TrainStationAPI.Model;
using TrainStationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<FluentNhibernateHelper>();
builder.Services.AddScoped<ITrainStation<Train>,TrainStationRepository<Train>>();
builder.Services.AddScoped<ITrainInfo, TrainInfoRepository>();
builder.Services.AddScoped<ITrainStation<Station>, TrainStationRepository<Station>>();
builder.Services.AddScoped<ITrainStation<Connection>, TrainStationRepository<Connection>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
