using API.Helpers;
using Application.Interfaces;
using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]

public  class LiteraryWordController : MyController<LiteraryWordController>
{
    private ILiteraryWord _service;

    public LiteraryWordController(ILiteraryWord service)
    {
        _service = service;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<PagedResult<LiteraryWordViewModel>>> GetAllLiteraryWords(
        FilterLiteraryWordRequest request)
    {
        var response = await _service.GetLiteraryWords(request, HttpContext.RequestAborted);

        return response;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    public async Task<Result<LiteraryWordSingleViewModel>> GetLiteraryWordById(long id)
    {
        var response = await _service.GetLiteraryWordById(id, HttpContext.RequestAborted);

        return response;
    }


    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("upsert")]
    public async Task<Result<LiteraryWordViewModel>> UpsertLiteraryWord(UpsertLiteraryWordRequest request)
    {
        var response = await _service.UpsertLiteraryWord(request, HttpContext.RequestAborted);

        return response;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}/toggleActivation")]
    public async Task<Result<LiteraryWordViewModel>> LiteraryWordToggleActivation(long id)
    {
        var response = await _service.LiteraryWordToggleActivation(id, HttpContext.RequestAborted);

        return response;
    }
}