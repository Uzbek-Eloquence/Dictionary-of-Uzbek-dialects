using Domain.Enums;

namespace Domain.Models.API.Requests;

public record TranslateWordRequest(
    WordTypeEnum From,
    WordTypeEnum To,
    string Word);