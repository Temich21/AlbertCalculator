using AlbertCalculator.Data;
using AlbertCalculator.Dtos;
using AlbertCalculator.Models;

namespace AlbertCalculator.Repositories
{
    public class FileRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public FileRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<FileModel> CreateFileAsync(FileDto fileDto)
        {
            var newFile = new FileModel
            {
                Id = fileDto.Id,
                FileName = fileDto.FileName,
                Data = fileDto.Data,
            };

            _context.Files.Add(newFile);
            await _context.SaveChangesAsync();

            return newFile;
        }

        public async Task<FileModel?> FindFileAsync(Guid fileId)
        {
            return await _context.Files.FindAsync(fileId);
        }
    }
}
