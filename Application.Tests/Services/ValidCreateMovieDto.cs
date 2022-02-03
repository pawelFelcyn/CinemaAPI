using Application.Dtos;
using System.Collections.Generic;
using System.IO;
using TestingHelpers;

namespace Application.Tests.Services;

internal class ValidCreateMovieDto : FromJsonFileData<CreateMovieDto>
{
    public ValidCreateMovieDto() : base(Path.Combine("DataFiles", "ValidCreateMovieDtoModels.json"))
    {
    }

    public override IEnumerator<object[]> GetEnumerator()
    {
        var models = base.GetEnumerator();

        while (models.MoveNext())
        {
            yield return new object[] { models.Current[0], true };
        }
    }
}
