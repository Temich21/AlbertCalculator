namespace AlbertCalculator.Dtos
{
    public class PurchaseDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid UserId { get; set; }
        public required string Date { get; set; }
        public required FileDto File { get; set; }
    }
}
