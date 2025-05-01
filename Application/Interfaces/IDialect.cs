using Domain.Models.API.Results;
using Domain.Models.Common;

namespace Application.Interfaces;

public interface IDialect
{
    Task<Result<IReadOnlyCollection<DialectResult>>> GetDialects(CancellationToken cancellationToken = default);
}