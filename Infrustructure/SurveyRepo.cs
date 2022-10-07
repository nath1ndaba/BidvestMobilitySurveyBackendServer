using BidvestMobilitySurveyBackendServer.Database;
using BidvestMobilitySurveyBackendServer.Models;
using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Driver;
using System.Net;

namespace BidvestMobilitySurveyBackendServer.Infrustructure;

public class SurveyRepo : ISurvey
{
    private readonly IRepository<Survey> survey;
    private readonly IIDGenerator idGenerator;

    public SurveyRepo(IRepository<Survey> survey, IIDGenerator idGenerator)
    {
        this.survey = survey;
        this.idGenerator = idGenerator;
    }
    public async Task<Response> GetSurveys()
    {
        var surveys = await survey.Find(_ => true);
        return new Response<IEnumerable<Survey>>(surveys);
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

