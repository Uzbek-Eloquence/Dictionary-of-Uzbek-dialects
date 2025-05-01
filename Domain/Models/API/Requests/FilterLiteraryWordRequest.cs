using DataAccess.Enums;
using Domain.Models.Common;
using Newtonsoft.Json;

namespace Domain.Models.API.Requests;

public record FilterLiteraryWordRequest : PagedRequest
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("partOfSpeechId")]
    public long PartOfSpeechId { get; set; }
    
    [JsonProperty("status")]
    public EntityStatus Status { get; set; }
    
    [JsonProperty("description")]
    public string? Description { get; set; }
}