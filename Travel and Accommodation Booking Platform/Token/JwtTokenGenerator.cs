using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Interfaces.IRepository;

namespace Travel_and_Accommodation_Booking_Platform.Token
{
    /// <summary>
    /// Generates and validates JWT tokens for user authentication.
    /// </summary>
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<AppUser> _appUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the token generator.</param>
        /// <param name="appUserRepository">The repository for <see cref="AppUser"/>.</param>
        public JwtTokenGenerator(IConfiguration configuration, IRepository<AppUser> appUserRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(IRepository<AppUser>));
        }

        /// <summary>
        /// Generates a JWT token based on user credentials.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="secretKey">The secret key for token signing.</param>
        /// <param name="issuer">The issuer of the token.</param>
        /// <param name="audience">The audience of the token.</param>
        /// <param name="role">The role of the user.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateToken(string? email, string? password, string secretKey, string issuer, string audience, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("username", email),
                new Claim("password", password),
                new Claim("role", role)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer,
                audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// Validates user credentials.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The user if credentials are valid; otherwise, null.</returns>
        public AppUser? ValidateUserCredentials(string? email, string? password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt: "$2a$12$6FjKqlXYrjM/oHxRpmHGSu");
            var user = _appUserRepository.GetAll().Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
                return null;

            if (user.PasswordHash == passwordHash)
                return user;

            return null;
        }

        /// <summary>
        /// Validates the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["Authentication:Issuer"],
                    ValidAudience = _configuration["Authentication:Audience"],
                    IssuerSigningKey = key
                }, out _);
                return true; // Token is valid
            }
            catch
            {
                return false; // Token validation failed
            }
        }
    }
}
