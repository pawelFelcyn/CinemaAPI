using Application.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TestingHelpers;

namespace Application.Tests.Services;

internal class InvalidCreateMovieDto : FromJsonFileData<CreateMovieDto>
{
    public InvalidCreateMovieDto() : base(Path.Combine("DataFiles", "InvalidCreateMovieDtoModels.json"))
    {
    }

    public override IEnumerator<object[]> GetEnumerator()
    {
        var models = base.GetEnumerator();

        while (models.MoveNext())
        {
            yield return new object[] { models.Current[0], false };
        }
    }
}
