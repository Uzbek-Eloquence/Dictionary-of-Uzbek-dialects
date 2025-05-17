using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IDialectalWord
{
    Task<Result<UpsertDialectalWordResult>> Upsert(UpsertDialectalWordRequest request);
    Task<Result<PagedResult<DialectalWordResult>>> Filter(FilterDialectalWordRequest request);

    Task<Result<TranslatedWordResult>> Translate(TranslateWordRequest request);

    Task<Result<TranslatedWordResult>> GetFromAudio(IFormFile file);
}