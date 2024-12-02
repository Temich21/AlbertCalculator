namespace AlbertCalculator.Dtos
{
    public class FileDto
    {
        public required Guid Id { get; set; }
        public required string FileName { get; set; }
        public required string Data { get; set; }
    }
}
