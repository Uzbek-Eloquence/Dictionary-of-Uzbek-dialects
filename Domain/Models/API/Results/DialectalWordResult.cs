namespace Domain.Models.API.Results;

public record DialectalWordResult(
    long Id,
    string Title,
    string LiteraryWordTitle,
    string Dialect);