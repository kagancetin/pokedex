using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using BeastBattle.Server.Data;
using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Entities;
using BeastBattle.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BeastBattle.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly BeastBattleDbContext beastBattleDbContext;
        private readonly IConfiguration configuration;
        public AuthService(BeastBattleDbContext beastBattleDbContext, IConfiguration configuration)
        {
            this.beastBattleDbContext = beastBattleDbContext;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Username))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            this.beastBattleDbContext.Users.Add(user);
            await this.beastBattleDbContext.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = user.Id,
                Message = "Registration successful!"
            };
        }


        public async Task<bool> UserExists(string username)
        {
            if (await this.beastBattleDbContext.Users.AnyAsync(user => user.Username
                 .Equals(username)))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await this.beastBattleDbContext.Users
                        .FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(this.configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}