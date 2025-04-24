using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;

namespace Application.Interfaces;

public interface ILiteraryWord
{
    Task<Result<LiteraryWordViewModel>> UpsertLiteraryWord(UpsertLiteraryWordRequest wordRequest, CancellationToken cancellationToken);
    Task<Result<LiteraryWordSingleViewModel>> GetLiteraryWordById(long wordId, CancellationToken cancellationToken);

    Task<Result<PagedResult<LiteraryWordViewModel>>> GetLiteraryWords(FilterLiteraryWordRequest request,
        CancellationToken cancellationToken);

    Task<Result<LiteraryWordViewModel>> LiteraryWordToggleActivation(long wordId,
        CancellationToken cancellationToken);
}