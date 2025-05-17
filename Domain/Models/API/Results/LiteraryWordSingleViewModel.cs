using DataAccess.Enums;
using Dictionary.Domain.Entity;
using Newtonsoft.Json;

namespace Domain.Models.API.Results;

public class LiteraryWordSingleViewModel
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
    
   
    public LiteraryWordSingleViewModel( LiteraryWord literaryWords)
    {
        Id = literaryWords.Id;
        Title = literaryWords.Title;
        PartOfSpeechId = literaryWords.PartOfSpeechId;
        Status = literaryWords.Status;
        Description = literaryWords.Description;
    }
}