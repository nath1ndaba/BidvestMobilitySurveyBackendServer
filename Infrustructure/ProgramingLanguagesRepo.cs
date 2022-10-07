using BidvestMobilitySurveyBackendServer.Database;
using BidvestMobilitySurveyBackendServer.Models;
using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;

namespace BidvestMobilitySurveyBackendServer.Infrustructure;

public class ProgramingLanguagesRepo:IProgrammingLanguages
{
    private readonly IRepository<ProgrammingLanguages> programmingLanguages;
    private readonly IIDGenerator idGenerator;

    public ProgramingLanguagesRepo(IRepository<ProgrammingLanguages> programingLanuages, IIDGenerator idGenerator)
    {
        this.programmingLanguages = programingLanuages;
        this.idGenerator = idGenerator;
    }
    public async Task<Response> GetLanguages()
    {
        var languages = await programmingLanguages.Find(_=>true);
        return new Response<IEnumerable<ProgrammingLanguages>>(languages);
    }
    public async Task<Response> AddLanguage(ProgrammingLanguages _language)
    {
        _language.Id = idGenerator.GenerateNewId();
        await programmingLanguages.Insert(_language); 
        return new Response<HttpStatusCode>(HttpStatusCode.Created);

    }
    public async Task<Response> RemoveLanguage(string languageId)
    {
        await programmingLanguages.DeleteOne(s=>s.Id== languageId);
        return new Response<HttpStatusCode>(HttpStatusCode.Created);
    }








    //private  MongoDbContext mongoDbContext;





    //public ProgramingLanguagesRepo(MongoDbContext dbContext)
    //{ 
    //    mongoDbContext = dbContext;
    //}
    //public List<ProgrammingLanguages> GetProgrammingLanguages()
    //{
    //    try
    //    {
    //        return mongoDbContext.ProgrammingLanguages.Find(_ => true).ToList();
    //    }
    //    catch
    //    { 
    //        throw;
    //    }
    //}
    //public void AddProgram(ProgrammingLanguages language)
    //{
    //    try
    //    {
    //        mongoDbContext.ProgrammingLanguages.InsertOne(language);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    //public ProgrammingLanguages GetProgram(string programId)
    //{
    //    try
    //    {
    //        FilterDefinition<ProgrammingLanguages> filterLanguageData = Builders<ProgrammingLanguages>.Filter.Eq("Id", programId);

    //        return mongoDbContext.ProgrammingLanguages.Find(filterLanguageData).FirstOrDefault();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    //public void RemoveProgram(string programId)
    //{
    //    try
    //    {
    //        FilterDefinition<ProgrammingLanguages> languageData = Builders<ProgrammingLanguages>.Filter.Eq("Id", programId);
    //        mongoDbContext.ProgrammingLanguages.DeleteOne(languageData);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


}
