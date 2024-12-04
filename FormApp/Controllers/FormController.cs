using Business.Dto.FormDto;
using Business.Services.FormService;
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
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }
        // GET: api/<FormController>
        [HttpGet("getDynamicFormList")]
        public async Task<ActionResult> GetDynamicFormList()
        {
            try
            {
                var data = await _formService.GetDynamicFormList();

                return Ok(data);      
            }
            catch (Exception)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FormController>
        [HttpPost("createNewDynamicForm")]
        public async Task<ActionResult> CreateNewDynamicForm([FromBody] FormDynamicDto formDynamic)
        {
            try
            {
                var data = await _formService.CreateNewDynamicForm(formDynamic);

                return Ok(new {
                    message = "Successfully Created New Form",
                });
            }
            catch (Exception)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<FormController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FormController>/5
        [HttpDelete("DeleteDynamicFormById/{id}")]
        public async Task<ActionResult> DeleteDynamicForm (int id)
        {
            var rs = await _formService.DeleteDynamicForm(id);
            if (rs > 0)
            {
                return Ok(new
                {
                    message = "Successfully",
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpGet("getFormLinkList")]
        public async Task<ActionResult> GetFormLinkList()
        {
            try
            {
                var data = await _formService.GetFormLinkList();

                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("createNewFormLink")]
        public async Task<ActionResult> CreateNewFormLink([FromBody] FormLinkDto formLink)
        {
            try
            {
                var data = await _formService.CreateFormLink(formLink);

                return Ok(new
                {
                    message = "Successfully Created New Form",
                });
            }
            catch (Exception)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut("UpdateDynamicForm")]
        public async Task<ActionResult> UpdateDynamicForm([FromBody] UpdateDynamicFormDto updateDynamicFormDto)
        {
            var rs = await _formService.UpdateDynamicForm(updateDynamicFormDto);

            if(rs > 0)
            {
                return Ok(new
                {
                    message = "Successfully Updated Form",
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpPut("UpdateFormLink")]
        public async Task<ActionResult> UpdateFormLink([FromBody] UpdateFormLinkDto updateFormLinkDto)
        {
            var rs = await _formService.UpdateFormLink(updateFormLinkDto);

            if (rs > 0)
            {
                return Ok(new
                {
                    message = "Successfully Updated Form",
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpDelete("DeleteFormLinkById/{id}")]
        public async Task<ActionResult> DeleteFormLinkById(int id)
        {
            var rs = await _formService.DeleteDynamicFormLink(id);
            if (rs > 0)
            {
                return Ok(new
                {
                    message = "Successfully",
                });
            }

            return BadRequest(HttpStatusCode.BadRequest);
        }
    }
}
