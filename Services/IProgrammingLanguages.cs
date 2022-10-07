using BidvestMobilitySurveyBackendServer.Models;
using MongoDB.Driver;

namespace BidvestMobilitySurveyBackendServer.Services
{
    public interface IProgrammingLanguages
    {
        Task<Response> GetLanguages();
        Task<Response> AddLanguage(ProgrammingLanguages language);
        Task<Response> RemoveLanguage(string languageId);

    }

}
