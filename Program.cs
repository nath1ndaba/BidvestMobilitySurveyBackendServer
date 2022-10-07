using BidvestMobilitySurveyBackendServer;
using BidvestMobilitySurveyBackendServer.Database;
using BidvestMobilitySurveyBackendServer.Infrustructure;
using BidvestMobilitySurveyBackendServer.Services;
using DotNetEnv;
using Microsoft.Extensions.Options;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp => new RepositoryManager(sp));
builder.Services.AddSingleton<IDatabaseContext>(sp => new MongoDbContext(
    MobilityConfig.DATABASE_NAME
    , MobilityConfig.DATABASE_HOST
    , MobilityConfig.DATABASE_AUTH_DB
    , MobilityConfig.DATABASE_USERNAME
    , MobilityConfig.DATABASE_USER_PASSWORD
    , sp.GetService<ILogger<MongoDbContext>>()));
builder.Services.AddSingleton(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IQueryBuilderProvider, QueryBuilderProvider>();
builder.Services.AddTransient(typeof(IQueryBuilder<>), typeof(BaseQueryBuilder<>));
builder.Services.AddTransient(typeof(IUpdateBuilderProvider), typeof(UpdateBuilderProvider));
builder.Services.AddTransient(typeof(IUpdateBuilder<>), typeof(BaseUpdateBuilder<>));


builder.Services.AddSingleton<IIDGenerator, BsonIdGenerator>();
builder.Services.AddSingleton<IProgrammingLanguages, ProgramingLanguagesRepo>();
builder.Services.AddTransient<ISurvey, SurveyRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<HttpResponseErrorHandlerMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

