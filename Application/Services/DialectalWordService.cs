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

    public async Task<Result<TranslatedWordResult>> Translate(TranslateWordRequest request)
    {
        request = request with{Word = request.Word.Trim().ToLower()};

        // in this case one of the words must be literary
        if (request.From == WordTypeEnum.Literary)
        {
            return await HandleFromLiteraryWordToDialect(request);
        }

        if (request.To == WordTypeEnum.Literary)
        {
            return await HandleFromDialectToLiteraryWord(request);
        }

        return await HandleFromDialectToDialect(request);
    }

    async private Task<Result<TranslatedWordResult>> HandleFromDialectToLiteraryWord(TranslateWordRequest request)
    {
        var fromId = await GetDialectId(request.From);
        var dialect = await entityContext.DialectalWords.FirstOrDefaultAsync(x => x.DialectsId == fromId);
        if (dialect == null)
            return new ErrorModel(ErrorEnum.DialectNotFound);
        
        var literaryWord = await entityContext.LiteraryWords
            .Include(x=>x.PartOfSpeech)
            .FirstAsync(x => x.Id == dialect.LiteraryWordsId);
        
        return new TranslatedWordResult(literaryWord.Title, literaryWord.PartOfSpeech.Title);
    }

    async private Task<Result<TranslatedWordResult>> HandleFromDialectToDialect(TranslateWordRequest request)
    {
        var fromId = await GetDialectId(request.From);
        var toId = await GetDialectId(request.To);

        var dialectalFromId = await entityContext.DialectalWords.FirstOrDefaultAsync(x=>x.Title.ToLower().Contains(request.Word.ToLower()) && x.DialectsId == fromId);
        if (dialectalFromId == null)
            return new ErrorModel(ErrorEnum.DialectNotFound);
    
        var result = await entityContext
            .DialectalWords
            .Include(x => x.LiteraryWords)
            .ThenInclude(literaryWord =>
                literaryWord.PartOfSpeech)
            .FirstOrDefaultAsync(x=>x.DialectsId == toId && x.LiteraryWordsId == dialectalFromId.LiteraryWordsId);
        if (result == null)
            return new ErrorModel(ErrorEnum.DialectNotFound);
    
        return new TranslatedWordResult(result.Title, result.LiteraryWords.PartOfSpeech.Title);
    }
    async private Task<Result<TranslatedWordResult>> HandleFromLiteraryWordToDialect(TranslateWordRequest request)
    {
        var toId = await GetDialectId(request.From);

        var literaryWord =
            await entityContext.LiteraryWords
                .FirstOrDefaultAsync(x =>
                    x.Title.ToLower()
                        .Contains(request.Word));
        if (literaryWord == null)
            return new ErrorModel(ErrorEnum.LiteraryWordNotFound);
        
        var result = await entityContext
            .DialectalWords
            .Include(x => x.LiteraryWords)
            .ThenInclude(literaryWord =>
                literaryWord.PartOfSpeech)
            .FirstOrDefaultAsync(x=>x.DialectsId == toId && x.LiteraryWordsId == literaryWord.Id);
        if (result == null)
            return new ErrorModel(ErrorEnum.DialectNotFound);

        return new TranslatedWordResult(result.Title, result.LiteraryWords.PartOfSpeech.Title);
    }

    async private ValueTask<long> GetDialectId(
        WordTypeEnum typeEnum)
    {
        Dialect? dialect;
        switch (typeEnum)
        {
            case WordTypeEnum.Parkent:
                dialect = await entityContext.Dialects.FirstOrDefaultAsync(x=> x.Title.ToLower().Contains("parkent"));
                break;
            case WordTypeEnum.Piskent:
                dialect = await entityContext.Dialects.FirstOrDefaultAsync(x=> x.Title.ToLower().Contains("piskent"));
                break;
            case WordTypeEnum.Toshkent:
                dialect = await entityContext.Dialects.FirstOrDefaultAsync(x=> x.Title.ToLower().Contains("toshkent"));
                break;
            default:
                return 0;
        }

        return dialect!.Id;
    } 
}