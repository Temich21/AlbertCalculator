﻿namespace AlbertCalculator.Dtos
{
    public class CategoryDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Expenses { get; set; }
    }
}