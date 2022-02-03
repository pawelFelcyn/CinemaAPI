using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace Application.Tests.Helpers;

internal static class OutputHelperExtensions
{
    public static void DisplayErrors(this ITestOutputHelper outputHelper, IEnumerable<ValidationFailure> errors)
    {
        if (errors.Count() != 0)
        {
            outputHelper.WriteLine("Errors:");

            foreach (var e in errors)
            {
                outputHelper.WriteLine(e.ErrorMessage);
            }
        }
    }
}
