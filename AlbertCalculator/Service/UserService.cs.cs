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
            var existingUser = await _userRepository.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new Exception($"User with email {userDto.Email} already exist.");
            }

            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password, 13);

            var userCredations = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = passwordHash
            };

            var user = await _userRepository.CreateAsync(userCredations);

            var tokens = _tokenService.GenerateTokens(user);

            return new AuthUserDto
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
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
            var user = await _userRepository.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                throw new Exception($"User with email {userDto.Email} doesn't exist.");
            }

            var isPasswordCorrect = BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, user.Password);
            if (!isPasswordCorrect)
            {
                throw new Exception("Incorrect password.");
            }

            var tokens = _tokenService.GenerateTokens(user);

            return new AuthUserDto
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
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
