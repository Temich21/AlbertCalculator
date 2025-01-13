using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using AlbertCalculator.Data;
using AlbertCalculator.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace AlbertCalculator.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string AccessToken, string RefreshToken) GenerateTokens(User user)
        {
            // Access Token Generation
            var accessToken = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm()) // Encryption algorithm
                .WithSecret(GetAccessSecret())                 // Secret key
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(GetAccessTokenTime()).ToUnixTimeSeconds()) // Token lifetime
                .AddClaim("id", user.Id.ToString())        // User Data
                .AddClaim("name", user.Name)
                .AddClaim("email", user.Email)
                .Encode(); // Generate token

            // Refresh Token Generation
            var refreshToken = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(GetRefreshSecret())
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(GetRefreshTokenTime()).ToUnixTimeSeconds())
                .AddClaim("id", user.Id.ToString())
                .Encode();

            return (accessToken, refreshToken);
        }

        public string ValidateRefresh(string refreshToken)
        {
            try
            {
                var payload = new JwtBuilder()
                    .WithSecret(GetRefreshSecret())
                    .MustVerifySignature()
                    .Decode(refreshToken); 

                return payload;
            }
            catch (TokenExpiredException)
            {
                throw new Exception("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                throw new Exception("Invalid token signature");
            }
        }

        private string GetAccessSecret()
        {
            return _configuration["Jwt:AccessSecret"];
        }

        private int GetAccessTokenTime()
        {
            return int.Parse(_configuration["Jwt:AccessTokenTime"]);
        }

        private string GetRefreshSecret()
        {
            return _configuration["Jwt:RefreshSecret"];
        }

        private int GetRefreshTokenTime()
        {
            return int.Parse(_configuration["Jwt:RefreshTokenTime"]);
        }
    }
}
