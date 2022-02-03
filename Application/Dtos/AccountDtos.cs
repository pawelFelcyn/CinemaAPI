namespace Application.Dtos;

public record RegisterDto(string FirstName, string LastName, string Email, string RoleName, DateTime? Birthdate, string Password, string ConfirmPassword);
public record LoginDto(string Email, string Password);
