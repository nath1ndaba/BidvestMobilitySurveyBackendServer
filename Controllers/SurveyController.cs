using BidvestMobilitySurveyBackendServer.Models;
using BidvestMobilitySurveyBackendServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Windows.Input;

namespace BidvestMobilitySurveyBackendServer.Controllers;

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/v1/[controller]")]
[ProducesResponseType(typeof(Response), 400)]
[ProducesResponseType(typeof(Response), 500)]
[ApiController]
public class SurveyController
{
    private readonly ISurvey survey;
    public SurveyController(ISurvey  survey)
    {
        this.survey = survey;
    }
    [HttpGet("Surveys")]
    [ProducesResponseType(typeof(Response<Survey>), 200)]
    public async Task<Response> GetSurveys()
    {
        var response = await survey.GetSurveys();
        return response;
    }


    [HttpPost]
    [ProducesResponseType(typeof(Response<Survey>), 201)]
    public async Task<Response> AddSurvey([FromBody] Survey _survey)
    {
        var response = await survey.AddSurvey(_survey);
        return response;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response), 200)]
    public async Task<Response> RemoveProgram(string id)
    {
        var response = await survey.RemoveSurvey(id);
        return response;
    }
}
