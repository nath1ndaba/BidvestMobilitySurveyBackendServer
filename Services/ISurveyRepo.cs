using BidvestMobilitySurveyBackendServer.Models;
using MongoDB.Driver;

namespace BidvestMobilitySurveyBackendServer.Services
{
    public interface ISurvey
    {
        Task<Response> GetSurveys();
        Task<Response> AddSurvey(Survey survey);
        Task<Response> RemoveSurvey(string surveyId);


    }

}
