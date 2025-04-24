using Newtonsoft.Json;

namespace Domain.Models.API.Requests;

public class UpsertLiteraryWordRequest
{
    [JsonProperty("id")]
    public long? Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("partOfSpeechId")]
    public long PartOfSpeechId { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
}