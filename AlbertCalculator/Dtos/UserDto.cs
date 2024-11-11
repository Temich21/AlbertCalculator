﻿namespace AlbertCalculator.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}