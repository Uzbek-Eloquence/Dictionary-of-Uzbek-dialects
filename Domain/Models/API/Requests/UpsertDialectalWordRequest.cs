namespace Domain.Models.API.Requests;

public record UpsertDialectalWordRequest(
    long? Id,
    string Title,
    long LiteraryWordId,
    long DialectId);