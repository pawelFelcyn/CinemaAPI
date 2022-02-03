using Application.Dtos;
using System.IO;
using TestingHelpers;

namespace Application.Tests.Services;

internal class InvalidRegister : FromJsonFileData<RegisterDto>
{
    public InvalidRegister() : base(Path.Combine("DataFiles", "InvalidRegisterModels.json"))
    {
    }
}
