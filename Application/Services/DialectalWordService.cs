using Application.Extensions;
using Application.Interfaces;
using DataAccess;
using DataAccess.Enums;
using Dictionary.Domain.Entity;
using Domain.Enums;
using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class DialectalWordService(EntityContext entityContext) : IDialectalWord
{
    public async Task<Result<UpsertDialectalWordResult>> Upsert(UpsertDialectalWordRequest request)
    {
        var literaryWord = await entityContext.LiteraryWords.FirstOrDefaultAsync(x => x.Id == request.LiteraryWordId);
        if (literaryWord == null)
            return new ErrorModel(ErrorEnum.LiteraryWordNotFound);
            
        var dialect = await entityContext.Dialects.FirstOrDefaultAsync(x => x.Id == request.DialectId);
        if (dialect == null)
            return new ErrorModel(ErrorEnum.DialectNotFound);
        
        if (request.Id.HasValue)
        {
            var word = await entityContext.DialectalWords.FirstOrDefaultAsync(x=>x.Id == request.Id.Value);
            if (word == null)
                return new ErrorModel(ErrorEnum.DialectalWordNotFound);
            
            word.LiteraryWordsId = request.LiteraryWordId;
            word.Title = request.Title;
            word.UpdatedDate = DateTime.Now;
            await entityContext.SaveChangesAsync();
            return word.Adapt<UpsertDialectalWordResult>();
        }
        
        var newWord = new DialectalWord
        {
            LiteraryWordsId = request.LiteraryWordId,
            Title = request.Title,
            DialectsId = request.DialectId,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        await entityContext.DialectalWords.AddAsync(newWord);
        await entityContext.SaveChangesAsync();
        return newWord.Adapt<UpsertDialectalWordResult>();
    }

    public async Task<Result<PagedResult<DialectalWordResult>>> Filter(FilterDialectalWordRequest request)
    {
        var result = entityContext.DialectalWords
            .Include(x=>x.Dialects)
            .Include(x=>x.LiteraryWords)
            .Where(x => x.Status != EntityStatus.Deleted)
            .AsQueryable();
        
        if (request.LiteraryWordId.HasValue)
            result = result.Where(x => x.LiteraryWordsId == request.LiteraryWordId.Value);
        if (request.DialectId.HasValue)
            result = result.Where(x => x.DialectsId == request.DialectId.Value);
        if(!string.IsNullOrEmpty(request.Title))
            result = result.Where(x => x.Title.Contains(request.Title));
        
        var allCount = result.Count();
        if(!request.All)
            result = result.Paginate(request);
        
        var words = await result.Select(x=>x.Adapt<DialectalWordResult>()).ToListAsync();
        return new PagedResult<DialectalWordResult>(words, allCount, request);
    }
}