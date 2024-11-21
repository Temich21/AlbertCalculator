namespace AlbertCalculator.Dtos
{
    public class ProductDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Expenses { get; set; }
    }
}
