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
using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class DialectalWordService(
    EntityContext entityContext) : IDialectalWord
{
    public async
        Task<Result<UpsertDialectalWordResult>>
        Upsert(UpsertDialectalWordRequest request)
    {
        var literaryWord =
            await entityContext.LiteraryWords
                .FirstOrDefaultAsync(x =>
                    x.Id ==
                    request.LiteraryWordId);
        if (literaryWord == null)
            return new ErrorModel(ErrorEnum
                .LiteraryWordNotFound);

        var dialect =
            await entityContext.Dialects
                .FirstOrDefaultAsync(x =>
                    x.Id == request.DialectId);
        if (dialect == null)
            return new ErrorModel(ErrorEnum
                .DialectNotFound);

        if (request.Id.HasValue)
        {
            var word =
                await entityContext.DialectalWords
                    .FirstOrDefaultAsync(x =>
                        x.Id == request.Id.Value);
            if (word == null)
                return new ErrorModel(ErrorEnum
                    .DialectalWordNotFound);

            word.LiteraryWordsId =
                request.LiteraryWordId;
            word.Title = request.Title;
            word.UpdatedDate = DateTime.Now;
            await entityContext
                .SaveChangesAsync();
            return word
                .Adapt<
                    UpsertDialectalWordResult>();
        }

        var newWord = new DialectalWord
        {
            LiteraryWordsId =
                request.LiteraryWordId,
            Title = request.Title,
            DialectsId = request.DialectId,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        await entityContext.DialectalWords
            .AddAsync(newWord);
        await entityContext.SaveChangesAsync();
        return newWord
            .Adapt<UpsertDialectalWordResult>();
    }

    public async
        Task<Result<
            PagedResult<DialectalWordResult>>>
        Filter(FilterDialectalWordRequest request)
    {
        var result = entityContext.DialectalWords
            .Include(x => x.Dialects)
            .Include(x => x.LiteraryWords)
            .Where(x =>
                x.Status != EntityStatus.Deleted)
            .AsQueryable();

        if (request.LiteraryWordId.HasValue)
            result = result.Where(x =>
                x.LiteraryWordsId ==
                request.LiteraryWordId.Value);
        if (request.DialectId.HasValue)
            result = result.Where(x =>
                x.DialectsId ==
                request.DialectId.Value);
        if (!string.IsNullOrEmpty(request.Title))
            result = result.Where(x =>
                x.Title.Contains(request.Title));

        var allCount = result.Count();
        if (!request.All)
            result = result.Paginate(request);

        var words = await result
            .Select(x =>
                x.Adapt<DialectalWordResult>())
            .ToListAsync();
        return new
            PagedResult<DialectalWordResult>(
                words, allCount, request);
    }

// Modified handler method for translation
    public async
        Task<Result<TranslatedWordResult>>
        Translate(TranslateWordRequest request)
    {
        // Validate request
        if (string.IsNullOrWhiteSpace(
                request.Word))
            return new ErrorModel(ErrorEnum
                .InvalidRequest);

        // Case 1: Dialect to Literary (Adabiy)
        if (request.From !=
            WordTypeEnum.Literary &&
            request.To == WordTypeEnum.Literary)
        {
            return await
                TranslateFromDialectToLiterary(
                    request);
        }
        // Case 2: Literary (Adabiy) to Dialect
        else if (request.From ==
                 WordTypeEnum.Literary &&
                 request.To !=
                 WordTypeEnum.Literary)
        {
            return await
                TranslateFromLiteraryToDialect(
                    request);
        }
        else
        {
            return new ErrorModel(ErrorEnum
                .UnsupportedTranslation);
        }
    }

    public async
        Task<Result<TranslatedWordResult>> GetFromAudio(IFormFile audioFile)
    {
        try
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return new ErrorModel(ErrorEnum.UnsupportedTranslation);
            }

            // Validate file extension
            var allowedExtensions = new[]
            {
                ".wav", ".mp3", ".ogg", ".m4a",
                ".wma"
            };
            var fileExtension = Path
                .GetExtension(audioFile.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(
                    fileExtension))
            {
                return new ErrorModel(ErrorEnum.UnsupportedTranslation);
            }

            // Get Azure Speech Service configuration from appsettings.json
            var speechKey = "2uhxeSZmsAuCN0WYKe3lVYuTYT2hWq0cysM8bfZ7gVpGmPr990IFJQQJ99BEACYeBjFXJ3w3AAAYACOGYt2o";
            var speechRegion = "eastus";

            // Create a temporary file to save the uploaded audio
            var tempFilePath = Path.GetTempFileName();
            await using (var stream =
                         File.Create(
                             tempFilePath))
            {
                await audioFile.CopyToAsync(
                    stream);
            }

            try
            {
                // Configure Azure Speech Service
                var config =
                    SpeechConfig.FromSubscription(
                        speechKey, speechRegion);

                config.SpeechRecognitionLanguage = "uz-UZ";

                using var audioInput = AudioConfig.FromWavFileInput(tempFilePath);
                using var recognizer = new SpeechRecognizer(config, audioInput);

                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason != ResultReason.RecognizedSpeech)
                    return new ErrorModel(ErrorEnum.WordNotFound);

                var word = await entityContext
                    .LiteraryWords.Include(literaryWord =>
                        literaryWord
                            .PartOfSpeech)
                    .FirstOrDefaultAsync(x => x.Title.Contains(result.Text.ToLower()));
                if (word == null)
                    return new ErrorModel(ErrorEnum.WordNotFound);
                return new
                    TranslatedWordResult
                    (word.PartOfSpeech.Title,
                        word.Title);
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
        catch (Exception ex)
        {
            return new ErrorModel(ErrorEnum.WordNotFound);
        }
    }


// Dialect to Literary Uzbek
    async private
        Task<Result<TranslatedWordResult>>
        TranslateFromDialectToLiterary(
            TranslateWordRequest request)
    {
        var dialectId =
            await GetDialectId(request.From);
        if (dialectId == 0)
            return new ErrorModel(ErrorEnum
                .DialectNotFound);

        // Find the dialectal word
        var dialectalWord = await entityContext
            .DialectalWords
            .Include(x => x.LiteraryWords)
            .ThenInclude(literaryWord => literaryWord.PartOfSpeech)
            .FirstOrDefaultAsync(x => x.Title.ToLower().Contains(request.Word.ToLower()));

        if (dialectalWord == null)
            return new ErrorModel(ErrorEnum.WordNotFound);

        // Return the literary word that corresponds to this dialectal word
        return new TranslatedWordResult(
            dialectalWord.LiteraryWords.Title,
            dialectalWord.LiteraryWords.PartOfSpeech.Title);
    }

// Literary Uzbek to Dialect
    async private
        Task<Result<TranslatedWordResult>>
        TranslateFromLiteraryToDialect(
            TranslateWordRequest request)
    {
        var dialectId =
            await GetDialectId(request.To);
        if (dialectId == 0)
            return new ErrorModel(ErrorEnum
                .DialectNotFound);

        // Find the literary word
        var literaryWord = await entityContext
            .LiteraryWords
            .Include(x => x.PartOfSpeech)
            .FirstOrDefaultAsync(x =>
                x.Title.ToLower()
                    .Contains(
                        request.Word.ToLower()));

        if (literaryWord == null)
            return new ErrorModel(ErrorEnum
                .LiteraryWordNotFound);

        // Find the dialectal word that corresponds to this literary word
        var dialectalWord = await entityContext
            .DialectalWords
            .FirstOrDefaultAsync(x =>
                x.DialectsId == dialectId &&
                x.LiteraryWordsId ==
                literaryWord.Id);

        if (dialectalWord == null)
            return new ErrorModel(ErrorEnum
                .DialectNotFound);

        return new TranslatedWordResult(
            dialectalWord.Title,
            literaryWord.PartOfSpeech.Title);
    }

    async private ValueTask<long> GetDialectId(
        WordTypeEnum typeEnum)
    {
        Dialect? dialect;
        switch (typeEnum)
        {
            case WordTypeEnum.Parkent:
                dialect = await entityContext
                    .Dialects
                    .FirstOrDefaultAsync(x =>
                        x.Title.ToLower()
                            .Contains("parkent"));
                break;
            case WordTypeEnum.Piskent:
                dialect = await entityContext
                    .Dialects
                    .FirstOrDefaultAsync(x =>
                        x.Title.ToLower()
                            .Contains("piskent"));
                break;
            case WordTypeEnum.Toshkent:
                dialect = await entityContext
                    .Dialects
                    .FirstOrDefaultAsync(x =>
                        x.Title.ToLower()
                            .Contains(
                                "toshkent"));
                break;
            case WordTypeEnum.Literary:
                // For Literary, return 0 to indicate Literary Uzbek
                return 0;
            default:
                return 0;
        }

        return dialect?.Id ?? 0;
    }
}