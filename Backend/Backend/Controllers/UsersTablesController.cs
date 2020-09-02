using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Backend.Handlers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Backend.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class UsersTablesController : ControllerBase
    {
        private readonly NorvoDBContext _context;
        private readonly JWTSettings _jwtsettings;

        public UsersTablesController(NorvoDBContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        // GET: api/UsersTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersTable>>> GetUsersTable()
        {
            return await _context.UsersTables.ToListAsync();
        }

        // GET: api/UsersTables/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<UsersTable>> GetUsersTable(string id)
        {
            var usersTable = await _context.UsersTables.FirstOrDefaultAsync();

            if (usersTable == null)
            {
                return NotFound();
            }

            return usersTable;
        }
        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult<UsersTable>> Get()
        {
            string emailAdress = HttpContext.User.Identity.Name;
            var usersTable = await _context.UsersTables.Where(user=>user.Email==emailAdress).FirstOrDefaultAsync();

            usersTable.Password = null;
            if (usersTable == null)
            {
                return NotFound();
            }

            return usersTable;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserWithToken>> Register([FromBody] UsersTable user)
        {
            UsersTable userInDb = await _context.UsersTables.Where(prop => prop.Email == user.Email).FirstOrDefaultAsync();
            if (userInDb==null)
            {
                await _context.UsersTables.AddAsync(user);
                await _context.SaveChangesAsync();
                return await Login(user);
            }
            return  NotFound();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] UsersTable user)
        {
            var userTable = await _context.UsersTables.Where(u => u.Email == user.Email && u.Password== user.Password).FirstOrDefaultAsync();
            UserWithToken userWithToken = null ;


            if( userTable != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                userTable.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();
                userWithToken = new UserWithToken(userTable);
                userWithToken.refreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            //sign your token here..
            userWithToken.token = GenerateAccessToken(userTable.Id);
            userWithToken.Password = null;
            return userWithToken;
        }
        [HttpPost("Refresh")]
        public ActionResult<UserWithToken> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            UsersTable user = GetUserFromAccessToken(refreshRequest.token);

            if (user !=null && ValidateRefreshToken(user,refreshRequest.refreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user)
                {
                    token = GenerateAccessToken(user.Id)
                };
                return userWithToken;
            }
            return null;
        }

        [HttpPost("logOut")]
        [Authorize]
        public async Task<ActionResult<bool>> LogOut([FromBody]UsersTable user)
        {
            RefreshToken refreshToken = await _context.RefreshTokens.Where(prop=>prop.UserId==user.Id).FirstOrDefaultAsync();
            if (refreshToken == null)
            {
                return NotFound();
            }

            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ValidateRefreshToken(UsersTable user, string refreshToken)
        {
           RefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken).OrderByDescending(rt => rt.ExpiryDate).FirstOrDefault();

            if( refreshTokenUser !=null && refreshTokenUser.UserId==user.Id && 
                refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        private UsersTable GetUserFromAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

            IdentityModelEventSource.ShowPII = true;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime=false,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal;
            SecurityToken securityToken;
            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch(Exception e)
            {
                return null;
            }


            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if(jwtSecurityToken !=null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
            {
                var userId = principal.FindFirst(ClaimTypes.Name)?.Value;
                return _context.UsersTables.Where(user => user.Id == Convert.ToInt32(userId)).FirstOrDefault();
            }
            return null;
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using(var rng=RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);
            return refreshToken;
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // PUT: api/UsersTables/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersTable(int id, UsersTable usersTable)
        {
            if (id != usersTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(usersTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersTables
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UsersTable>> PostUsersTable(UsersTable usersTable)
        {
            usersTable.ConfirmEmail = 0;
            _context.UsersTables.Add(usersTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersTable", new { id = usersTable.Id }, usersTable);
        }

        // DELETE: api/UsersTables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsersTable>> DeleteUsersTable(int id)
        {
            var usersTable = await _context.UsersTables.FindAsync(id);
            if (usersTable == null)
            {
                return NotFound();
            }

            _context.UsersTables.Remove(usersTable);
            await _context.SaveChangesAsync();

            return usersTable;
        }

        private bool UsersTableExists(int id)
        {
            return _context.UsersTables.Any(e => e.Id == id);
        }
    }
}
