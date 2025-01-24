using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Repositories;
namespace AlbertCalculator.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserService(UserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthUserDto> SignupUserAsync(UserDto userDto)
        {
            User? existingUser = await _userRepository.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new Exception($"User with email {userDto.Email} already exist.");
            }

            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password, 13);

            var userCredations = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name!,
                Email = userDto.Email,
                Password = passwordHash
            };

            User user = await _userRepository.CreateAsync(userCredations);

            var (AccessToken, RefreshToken) = _tokenService.GenerateTokens(user);

            return new AuthUserDto
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }

        public async Task<AuthUserDto> SigninUserAsync(UserDto userDto)
        {
            User? user = await _userRepository.FindByEmailAsync(userDto.Email)
                ?? throw new Exception($"User with email {userDto.Email} doesn't exist.");

            Boolean isPasswordCorrect = BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, user.Password);
            if (!isPasswordCorrect)
            {
                throw new Exception("Incorrect password.");
            }

            var (AccessToken, RefreshToken) = _tokenService.GenerateTokens(user);

            return new AuthUserDto
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }

        public async Task<AuthUserDto> RefreshUserAsync(string refreshToken)
        {
            UserDto userDto = _tokenService.ValidateRefresh(refreshToken);

            User? user = await _userRepository.FindAsync(userDto.Id)
                ?? throw new Exception($"User with email {userDto.Email} doesn't exist.");

            var (AccessToken, RefreshToken) = _tokenService.GenerateTokens(user);

            return new AuthUserDto
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }
    }
}
