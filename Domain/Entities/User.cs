﻿namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? RoleName { get; set; }
    public DateTime Birthdate { get; set; }
    public DateTime DateOfAppending { get; set; }
    public string? PasswordHash { get; set; }

    public User()
    {
        DateOfAppending = DateTime.UtcNow;
    }
}
