using Business.Dto.UserDto;
using Business.Dtos.MailDto;
using Business.Services.UserService;
using Business.Ultilities;
using FormApp.Dtos.UserDto;
using FormApp.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailjetSend _mailjet;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IMailjetSend mailjet, IConfiguration configuration, IAuthService authService)
        {
            _userService = userService;
            _mailjet = mailjet;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var l = new List<int> { 1, 2, 2, 3, 3, 4, };
     

            var listAccount = await _userService.GetListAccount();
            return Ok(listAccount);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("CheckAccountInfo")]
        public async Task<ActionResult> CheckAccountInfo(CheckAccountDto checkAccountDto)
        {
            var account = await _userService.CheckAccountInfo(checkAccountDto.UserName, checkAccountDto.Password);
            if (account.AccountID > 0)
            {
                var auhRes = _authService.GenerateToken(account.UserName, "");
                return Ok(new
                {
                    message = "Login Successfully",
                    role = account.RoleName,
                    token = auhRes.Token,
                    totalSeconds = auhRes.TotalSecond
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [Authorize]
        [HttpGet("GetStaffEmailList")]
        public async Task<ActionResult> GetStaffEmailList(string staffRole)
        {
            var data = await _userService.GetListStaffEmail(staffRole);
            if (data != null)
            {
                return Ok(data);
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpPost("CreateNewAccount")]
        public async Task<ActionResult> CreateNewAccount([FromBody] NewAccountDto newAccountDto)
        {
            try
            {
                await _userService.CreateNewAccount(newAccountDto);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(HttpStatusCode.BadRequest);
                throw;
            }
        }

        [HttpPost("SendNewPassword")]
        public async Task<ActionResult> SendNewPassword(string email)
        {
            try
            {
                var apiKey = _configuration.GetValue<string>("Mailjet_API_KEY");
                var apiPrivateKey = _configuration.GetValue<string>("Mailjet_PRIVATE_KEY");
                var gmailUser = _configuration.GetValue<string>("Gmail_APP_USER");
                var gmailApppw = _configuration.GetValue<string>("Gmail_APP_PW");

                await _userService.SendMailNewPassword(email, apiKey, apiPrivateKey, gmailUser , gmailApppw);


                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"Error sending email: {ex.Message}");
                return BadRequest(HttpStatusCode.BadRequest);
                throw;
            }

        }

 
        [HttpGet("GetStudentEmail")]
        public async Task<ActionResult> GetStudentEmail(string requestor)
        {
            var data = await _userService.GetProfileByUserName(requestor);
            if (data != null)
            {
                return Ok(data);
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpGet("GetListRole")]
        public async Task<ActionResult> GetListRole()
        {
            var data = await _userService.GetListRole();
            if (data != null)
            {
                return Ok(data);
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }
    }
}
