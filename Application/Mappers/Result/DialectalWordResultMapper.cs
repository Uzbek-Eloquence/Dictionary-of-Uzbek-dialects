using Dictionary.Domain.Entity;
using Domain.Models.API.Results;
using Mapster;

namespace Application.Mappers.Result;

public class DialectalWordResultMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<DialectalWord, DialectalWordResult>()
            .ConstructUsing(src => Map(src));

    }

    private static DialectalWordResult Map(DialectalWord src)
    {
        return new DialectalWordResult(src.Id, src.Title, src.LiteraryWords.Title, src.Dialects.Title);
    }
}