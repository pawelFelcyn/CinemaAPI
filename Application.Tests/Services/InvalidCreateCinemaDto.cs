using Application.Dtos;
using System.IO;
using TestingHelpers;

namespace Application.Tests.Services;

internal class InvalidCreateCinemaDto : FromJsonFileData<CreateCinemaDto>
{
    public InvalidCreateCinemaDto() : base(Path.Combine("DataFiles", "InvalidCreateCinemaDtoModels.json"))
    {
    }
}
