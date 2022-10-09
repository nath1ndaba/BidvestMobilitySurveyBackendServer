using BidvestMobilitySurveyBackendServer.Database;
using BidvestMobilitySurveyBackendServer.Helpers;
using BidvestMobilitySurveyBackendServer.Models;
using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Driver;
using System.Net;

namespace BidvestMobilitySurveyBackendServer.Infrustructure;

internal class SurveyRepo : BaseRepository<Survey>, ISurvey
{
    private readonly IRepository<Survey> survey;
    private readonly IRepository<ProgrammingLanguages> programmingLanguages;
    private readonly IIDGenerator idGenerator;
    private readonly IQueryBuilderProvider queryBuilderProvider;

    public SurveyRepo(
                    RepositoryManager repositoryManager,
                    IRepository<Survey> survey,
                    IRepository<ProgrammingLanguages> programmingLanguages,
                    IIDGenerator idGenerator,
                    IQueryBuilderProvider queryBuilderProvider) : base(repositoryManager)
    {
        this.survey = survey;
        this.idGenerator = idGenerator;
        this.queryBuilderProvider = queryBuilderProvider;
        this.programmingLanguages = programmingLanguages;
    }
    public async Task<Response> GetSurveys()
    {
        var surveys = await survey.Find(_ => true);

        var querableLanguages =  programmingLanguages.AsQueryable();

        var filtered = from survey in surveys
                       join language in querableLanguages on survey.LanguageId equals language.Id into joinedLanguages
                       select new { survey, joinedLanguages };
        var result = filtered.AsEnumerable().Select(x=> ModelHelpers.From(x.survey,x.joinedLanguages));

        return new Response<IEnumerable<GetSurvey>>(result);
    }
    public async Task<Response> AddSurvey(Survey _survey)
    {
        _survey.Id = idGenerator.GenerateNewId();
        await  survey.Insert(_survey);
        return new Response<HttpStatusCode>(HttpStatusCode.Created);

    }
    public async Task<Response> RemoveSurvey(string suveyId)
    {
        await survey.DeleteOne(s => s.Id == suveyId);
        return new Response<HttpStatusCode>(HttpStatusCode.Created);
    }
}

