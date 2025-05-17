using Domain.Models.Common;

namespace Domain.Models.API.Requests;

public record FilterDialectalWordRequest(string? Title, long? LiteraryWordId, long? DialectId) : PagedRequest;