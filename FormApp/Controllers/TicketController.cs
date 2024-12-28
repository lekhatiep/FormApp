using Business.Dto.TicketDto;
using Business.Services.TickerService;
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
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        // GET: api/<TicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost("postDynamicFormData")]
        public async Task<ActionResult> postDynamicFormData([FromBody] CreateTicketDto createTicketDto)
        {
           var rs =  await _ticketService.CreateNewTicket(createTicketDto);
            if(rs > 0)
            {
                return Ok();
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("GetTicketInfo")]
        public async Task<ActionResult> GetTicketInfo()
        {
            try
            {
                var rs = await _ticketService.GetListTicket();
                return Ok(rs);
            }
            catch (Exception)
            {

                return BadRequest(HttpStatusCode.BadRequest);
                throw;
            }        

        }
        [HttpGet("getDataByTicketID/{ID}")]
        public async Task<ActionResult> getDataByTicketID(int ID)
        {
            try
            {
                var rs = await _ticketService.GetDataByTicketID(ID);
                return Ok(rs);
            }
            catch (Exception e)
            {

                return BadRequest(HttpStatusCode.BadRequest);
                throw;
            }

        }

    }
}
