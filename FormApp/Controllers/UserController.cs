using Business.Services.UserService;
using FormApp.Dtos.UserDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
                return Ok(new
                {
                    message = "Login Successfully",
                    role = account.RoleName
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

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
    }
}
