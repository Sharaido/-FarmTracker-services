using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FarmTracker_services.Data;
using FarmTracker_services.Models;
using FarmTracker_services.Models.DB;
using FarmTracker_services.Models.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FarmTracker_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IFarmTrackerRepo _repositroy;
        private readonly IActionContextAccessor _accessor;

        public MembersController(IFarmTrackerRepo repository, IActionContextAccessor accessor)
        {
            _repositroy = repository;
            _accessor = accessor;
        }

        [HttpGet("signin")]
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
                if (hashedPass == theUser.Password)
                {
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
            return Ok(signInResponse);
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

        [HttpGet("GetUsers/{UUID}")]
        public ActionResult<User> GetUsers(Guid UUID)
        {
            var u = _repositroy.GetUser(UUID);
            if (u == null)
            {
                return NotFound();
            }
            return Ok(u);
        }

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
            return CreatedAtAction(
                nameof(GetUsers), 
                new { UUID = generatedCode.ForUuid },
                new User
                {
                    Username = signUpRequest.Username,
                    Name = signUpRequest.Name,
                    Surname = signUpRequest.Surname
                });
        }
    }
}
