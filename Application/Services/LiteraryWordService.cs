using System.Net;
using Application.Interfaces;
using DataAccess;
using DataAccess.Enums;
using Dictionary.Domain.Entity;
using Domain.Enums;
using Domain.Extensions;
using Domain.Models.API.Requests;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class LiteraryWordService : ILiteraryWord
{
    private readonly EntityContext _context;

    public LiteraryWordService(EntityContext context)
    {
        _context = context;
    }

    public async Task<Result<LiteraryWordViewModel>> UpsertLiteraryWord(
        UpsertLiteraryWordRequest wordRequest, 
        CancellationToken cancellationToken)
    {
        try
        {
            wordRequest.Id ??= 0;
            wordRequest.Title = wordRequest.Title.Trim();
            
            LiteraryWordViewModel result;

            if (wordRequest.Id == 0)
            {
                var wordIsExist = await _context.LiteraryWords.FirstOrDefaultAsync(x =>
                    x.Title == wordRequest.Title &&
                    x.PartOfSpeechId == wordRequest.PartOfSpeechId);

                if (wordIsExist != null) return new ErrorModel(ErrorEnum.LiteraryWordAlreadyExist);

                result = await InsertLiteraryWord(wordRequest,cancellationToken);
            }
            else
            {
                var word = await _context.LiteraryWords
                    .FirstOrDefaultAsync(c => c.Id == wordRequest.Id && 
                                              c.Status != EntityStatus.Deleted);

                if (word == null) return new ErrorModel(ErrorEnum.LiteraryWordNotFound);

                result = await UpdatewordOfAttorney(wordRequest,cancellationToken);
            }

           
            return result;
        }
        catch (Exception ex)
        {
            return new ErrorModel(ErrorEnum.InternalServerError);
        }
    }

    private async Task<LiteraryWordViewModel> UpdatewordOfAttorney(
        UpsertLiteraryWordRequest wordRequest, 
        CancellationToken cancellationToken)
    {
        var word = await _context.LiteraryWords
            .FirstOrDefaultAsync(c => c.Id == wordRequest.Id);

        word!.Title = wordRequest.Title.Trim();
        word.PartOfSpeechId = wordRequest.PartOfSpeechId;
        word.Description = wordRequest.Description;
        word.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return new LiteraryWordViewModel(word);
    }

    private async Task<LiteraryWordViewModel> InsertLiteraryWord(
        UpsertLiteraryWordRequest wordRequest, 
        CancellationToken cancellationToken)
    {
        var word = new LiteraryWord()
        {
            Title = wordRequest.Title,
            Description = wordRequest.Description,
            PartOfSpeechId = wordRequest.PartOfSpeechId,
        };
        await _context.LiteraryWords.AddAsync(word, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

     
        return new LiteraryWordViewModel(word);
    }

    public async Task<Result<LiteraryWordSingleViewModel>> GetLiteraryWordById(long wordId, CancellationToken cancellationToken)
    {
        try
        {
            
            var word = await _context.LiteraryWords.AsQueryable()
                .FirstOrDefaultAsync(d => d.Id == wordId && d.Status != EntityStatus.Deleted);

            if (word is null)
                return new ErrorModel(ErrorEnum.WordNotFound);
            
            return new LiteraryWordSingleViewModel(word);
        }
        catch (Exception ex)
        {
            return new ErrorModel(ErrorEnum.InternalServerError);
        }
    }

    public async Task<Result<PagedResult<LiteraryWordViewModel>>> GetLiteraryWords(FilterLiteraryWordRequest request,CancellationToken cancellationToken)
    {
        try
        {
            
            request.Title = request.Title.Trim();
            
            var words = _context.LiteraryWords.AsQueryable();
            
            words = words.Where(x => x.Status != EntityStatus.Deleted);

            if (request.Title.Length > 0)
                words = words.Where(x =>
                    x.Title.Contains(request.Title));

            if (request.Description.Length > 0)
                words = words.Where(x =>
                    x.Description.Contains(request.Description));
            
            if ( request.PartOfSpeechId != 0) 
                words = words.Where(x => x.PartOfSpeechId == request.PartOfSpeechId);

            
            var result = await words.OrderBy(x => x.Title)
                .Select(x => new LiteraryWordViewModel(x))
                .ToListAsync();
          
            return request.All ? 
                result.ToListResponse() : 
                result.ToListResponse(request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            return new ErrorModel(ErrorEnum.InternalServerError);
        }
    }

    public async Task<Result<LiteraryWordViewModel>> LiteraryWordToggleActivation(long wordId, CancellationToken cancellationToken)
    {
         try {
            
            var word = await _context.LiteraryWords
                .FirstOrDefaultAsync(c => c.Id == wordId);

            if (word is null)
                return new ErrorModel(ErrorEnum.LiteraryWordNotFound);

            word.Status = EntityStatus.Deleted;
            word.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return new LiteraryWordViewModel(word);
         }
         catch (Exception ex)
         {
            return new ErrorModel(ErrorEnum.InternalServerError);
         }
    }
}