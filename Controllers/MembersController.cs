using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FarmTracker_services.Data;
using FarmTracker_services.Models;
using FarmTracker_services.Models.DB;
using FarmTracker_services.Models.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FarmTracker_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IFarmTrackerRepo _repositroy;
        private readonly IActionContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public MembersController(IFarmTrackerRepo repository, IActionContextAccessor accessor, IConfiguration configuration)
        {
            _repositroy = repository;
            _accessor = accessor;
            _configuration = configuration;
        }

        [HttpPost("signin")]
        public ActionResult<SignInResponse> SignIn([FromBody] SignInRequest signInRequest)
        {
            SignInResponse signInResponse = new SignInResponse();
            Users theUser = _repositroy.GetUserFromSignInKey(signInRequest.SignInKey);

            if (theUser != null)
            {
                //If there are more than 5 failed requests for sign in in the last 5 minute, then ignore requests for the user.
                if (_repositroy.GetFailedSignInRequestsInTheLast5MinFromUUID(theUser.Uuid) >= 5)
                {
                    signInResponse.TooManyAttempts = true;
                    goto Exit;
                }

                var hashedPass = GetHashedPassword(signInRequest.Password);
                if (hashedPass == theUser.Password || signInRequest.SessionUID != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim("UUID", theUser.Uuid.ToString()),
                        new Claim(ClaimTypes.Name, theUser.Name),
                        new Claim(ClaimTypes.Surname, theUser.Surname),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, _repositroy.GetRoleFromRUID(theUser.Ruid).Name)
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    signInResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    signInResponse.Expiration = token.ValidTo;
                    signInResponse.Result = true;
                }
                else
                {
                    signInResponse.InvalidPassword = true;
                }

                var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                _repositroy.InsertSignInLog(new SignInLogs 
                { 
                    IpAdd = ip,
                    Uuid = theUser.Uuid,
                    AttemptedResult = signInResponse.Result,
                    AttemptedPassword = hashedPass,
                    Date = DateTime.UtcNow
                });
                _repositroy.SaveChanges();
            }
            else
            {
                signInResponse.InvalidSignInKey= true;
            }

            Exit:
            if (signInResponse.Result)
            {
                return Ok(signInResponse);
            }
            return Unauthorized(signInResponse);
        }
        private string GetHashedPassword(string password)
        {
            string hashedMd5 = "";
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                hashedMd5 = Convert.ToBase64String(result);
            }
            return hashedMd5;
        }

        /*[HttpGet("GetUsers/{UUID}")]
        public ActionResult<User> GetUsers(Guid UUID)
        {
            var u = _repositroy.GetUser(UUID);
            if (u == null)
            {
                return NotFound();
            }
            return Ok(u);
        }*/

        [HttpGet("GetNewUCodeForSignUp")]
        public ActionResult<GeneratedUcodes> GetNewUCodeForSignUp()
        {
            var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            var uCode = _repositroy.GetNewUCodeForSignUp(ip);
            if (uCode == null)
            {
                return BadRequest();
            }
            return Ok(uCode);
        }

        [HttpPost("signup")]
        public ActionResult<SignUpResponse> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            SignUpResponse signUpResponse = new SignUpResponse();

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            signUpRequest.Name = textInfo.ToTitleCase(signUpRequest.Name.Trim());
            signUpRequest.Surname = textInfo.ToTitleCase(signUpRequest.Surname.Trim());

            signUpRequest.Password = GetHashedPassword(signUpRequest.Password);

            var generatedCode = _repositroy
                .InsertUser(
                new Users
                {
                    Username = signUpRequest.Username,
                    Email = signUpRequest.Email,
                    Password = signUpRequest.Password,
                    Name = signUpRequest.Name,
                    Surname = signUpRequest.Surname,
                },
                signUpRequest.GUC
                );
            if (generatedCode != null)
            {
                signUpResponse.Result = true;
            }
            else
            {
                return BadRequest();
            }
            return Ok(signUpResponse);
        }
        [HttpPost("CreateSession/{UUID}")]
        [Authorize]
        public ActionResult<Sessions> CreateSession(Guid UUID)
        {
            return _repositroy.InsertSession(UUID);
        }
        [HttpGet("GetUserFromSignInKey/{SignInKey}")]
        [Authorize]
        public ActionResult<Users> GetUserFromSignInKey(string SignInKey)
        {
            return _repositroy.GetUserFromSignInKey(SignInKey);
        }
        [HttpGet("GetUsers/{UUID}")]
        [Authorize]
        public ActionResult<Users> GetUsers(Guid UUID)
        {
            var user = _repositroy.GetUserFromUUID(UUID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("AuthenticateFromCookies")]
        public ActionResult<SignInResponse> AuthenticateFromCookies([FromBody] Sessions session)
        {
            Sessions s = _repositroy.GetSession(session.Suid, session.Uuid);
            if (s != null)
            {
                var theUser = _repositroy.GetUserFromUUID(session.Uuid);

                var signInRequest = new SignInRequest{ 
                    SessionUID = session.Suid.ToString(),
                    SignInKey = theUser.Username,
                    Password = "Ignore"
                };

                var response = SignIn(signInRequest).Result;

                _repositroy.UpdateSessionLastUsed(s.Suid);
                return response;
            }
            else
            {
                return Unauthorized(new SignInResponse { Result = false});
            }
        }
        [HttpPost("InactiveteSession/{SUID}")]
        [Authorize]
        public ActionResult InactiveteSession(Guid SUID)
        {
            _repositroy.InactivateSession(SUID);
            return Ok();
        }
        [HttpGet("IsUsedUsername/{Username}")]
        public ActionResult<bool> IsUsedUsername(string Username)
        {
            return Ok(_repositroy.IsUsedUsername(Username));
        }
        [HttpGet("IsUsedEmail/{Email}")]
        public ActionResult<bool> IsUsedEmail(string Email)
        {
            return Ok(_repositroy.IsUsedEmail(Email));
        }
        [HttpPut("MemberType")]
        [Authorize]
        public ActionResult<bool> UpdateUserMemberType(Users user)
        {
            return Ok(_repositroy.UpdateMemberType(user.Uuid, user.Mtuid));
        }
        [HttpGet("SearchUser/{key}")]
        public ActionResult<IEnumerable<Users>> SearchUser(string key)
        {
            var r = _repositroy.SearchUser(key);
            if (r == null || r.Count() == 0)
            {
                return NotFound();
            }
            return Ok(r);
        }
    }
}
