namespace Application.Validation;

internal static class StringExtensions
{
    public static bool IsPhoneNumber(this string value)
    {
        if (value.Length != 9) return false;
        foreach (var c in value)
        {
            if (!char.IsDigit(c)) return false;
        }

        return true;
    }

    public static bool IsPostalCode(this string value)
        => value.Length == 6 &&
           char.IsDigit(value[0]) &&
           char.IsDigit(value[1]) &&
           char.IsDigit(value[3]) &&
           char.IsDigit(value[4]) &&
           char.IsDigit(value[5]) &&
           value[2] == '-';
}
