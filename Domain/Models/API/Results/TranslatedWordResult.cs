namespace Domain.Models.API.Results;

public record TranslatedWordResult(
    string TranslatedWord,
    string PartOfSpeech,
    string Description);