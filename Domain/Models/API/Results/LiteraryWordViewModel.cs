using DataAccess.Enums;
using Dictionary.Domain.Entity;
using Newtonsoft.Json;

namespace Domain.Models.API.Results;

public class LiteraryWordViewModel
{
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("partOfSpeechId")]
    public long PartOfSpeechId { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("status")]
    public EntityStatus Status { get; set; }

    public LiteraryWordViewModel( LiteraryWord word)
    {
        Id = word.Id;
        Title = word.Title;
        PartOfSpeechId = word.PartOfSpeechId;
        Status = word.Status;
        Description = word.Description;
    }
}