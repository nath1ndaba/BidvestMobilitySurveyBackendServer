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
public class ProgrammingLanguagesController
{
    private readonly IProgrammingLanguages programmingLanguages;
    public ProgrammingLanguagesController(IProgrammingLanguages programming)
    { 
        programmingLanguages=programming;   
    }
    [HttpGet("Languages")]
    [ProducesResponseType(typeof(Response<ProgrammingLanguages>), 200)]
    public async Task<Response>  GetProgrammingLanguages()
    {
        var response = await programmingLanguages.GetLanguages();
        return response;
    }


    [HttpPost]
    [ProducesResponseType(typeof(Response<ProgrammingLanguages>), 201)]
    public async Task<Response> AddProgram([FromBody] ProgrammingLanguages _programming )
    {
         var response =  await programmingLanguages.AddLanguage(_programming);
        return response;
    }

 
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response), 200)]
    public async Task<Response> RemoveProgram(string id)
    {
      var response =  await programmingLanguages.RemoveLanguage(id);
        return response;
    }
}
