using BidvestMobilitySurveyBackendServer.Models;

namespace BidvestMobilitySurveyBackendServer.Helpers
{
    public static class ModelHelpers
    {
        public static GetSurvey From(Survey survey, IEnumerable<ProgrammingLanguages>  languages)
        {
            return new()
            {
                Id = survey.Id,
                Name = survey.Name,
                Surname = survey.Surname,
                ProgrammingLanguage = languages.FirstOrDefault()!
            };
        }
    }
}
