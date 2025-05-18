using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.Models.API.Requests;

public record TranslateAudioRequest(
    WordTypeEnum From,
    WordTypeEnum To,
    IFormFile Audio);