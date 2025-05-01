using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;

namespace Application.Interfaces;

public interface IDialectalWord
{
    Task<Result<UpsertDialectalWordResult>> Upsert(UpsertDialectalWordRequest request);
    Task<Result<PagedResult<DialectalWordResult>>> Filter(FilterDialectalWordRequest request);
}