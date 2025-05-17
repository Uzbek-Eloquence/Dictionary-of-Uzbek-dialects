using Application.Interfaces;
using DataAccess;
using DataAccess.Enums;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class DialectsService(EntityContext entityContext) : IDialect
{
    public async Task<Result<IReadOnlyCollection<DialectResult>>> GetDialects(CancellationToken cancellationToken = default)
    {
        var dialects = await entityContext.Dialects
            .Where(x=>x.Status != EntityStatus.Deleted)
            .AsNoTracking()
            .Select(x=>x.Adapt<DialectResult>())
            .ToListAsync(cancellationToken: cancellationToken);
        return dialects;
    }
}