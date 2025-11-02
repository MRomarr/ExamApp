using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamApp.Infrastructure.Services.TokenProvider
{
    internal class JwtTokeProvider : ITokenProvider
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        public JwtTokeProvider(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var userClimas = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClimas = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var climas = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty)
            };
            climas.AddRange(userClimas);
            climas.AddRange(rolesClimas);

            var issuer = _jwtSettings.Issuer ?? throw new InvalidOperationException("JWT issuer is missing.");
            var audience = _jwtSettings.Audience ?? throw new InvalidOperationException("JWT audience is missing.");
            var key = _jwtSettings.Secret ?? throw new InvalidOperationException("JWT key is missing.");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: climas,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            ;
        }
        public async Task<string> GenerateAndStoreRefreshTokenAsync(ApplicationUser user)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                Token = token,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return token;
        }

    }
}
