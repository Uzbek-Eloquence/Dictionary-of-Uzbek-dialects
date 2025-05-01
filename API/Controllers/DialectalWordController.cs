using API.Helpers;
using Application.Interfaces;
using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DialectalWordController(IDialectalWord dialectalWord) : MyController<DialectalWordController>
{
    [HttpPost("upsert")]
    public async Task<Result<UpsertDialectalWordResult>> UpsertDialectalWord(UpsertDialectalWordRequest request)
        => await dialectalWord.Upsert(request);
    
    [HttpPost("filter")]
    public async Task<Result<PagedResult<DialectalWordResult>>> FilterDialectalWord(FilterDialectalWordRequest request)
        => await dialectalWord.Filter(request);
}