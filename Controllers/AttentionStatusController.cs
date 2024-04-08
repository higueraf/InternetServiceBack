using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Helpers;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.AttentionStatus;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InternetServiceBack.Filters;

namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class AttentionStatusController : ControllerBase
    {
        private readonly AttentionStatusBll _attentionStatusBll;

        public AttentionStatusController(DatabaseInternetServiceContext dbContext)
        {
            _attentionStatusBll = new AttentionStatusBll(dbContext);
        }

        // GET: api/<AttentionStatusController>
        [HttpGet]
        public ActionResult<IEnumerable<AttentionStatusDto>> Get()
        {
            var response = _attentionStatusBll.GetAttentionStatuss();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<AttentionStatusController>/5
        [HttpGet("{id}")]
        public ActionResult<AttentionStatusDto> Get(Guid id)
        {
            var response = _attentionStatusBll.GetAttentionStatusById(id);
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            else if (response.statusCode == 404)
            {
                return NotFound(response.message);
            }
            return StatusCode(response.statusCode, response.message);
        }


        // POST api/<AttentionStatusController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] AttentionStatusDto attentionStatusDto)
        {
            var response = _attentionStatusBll.CreateAttentionStatus(attentionStatusDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<AttentionStatusController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] AttentionStatusDto attentionStatusDto)
        {
            if (id != attentionStatusDto.AttentionStatusID)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "AttentionStatusID in URL does not match AttentionStatusID in body",
                });
            }

            var response = _attentionStatusBll.UpdateAttentionStatus(attentionStatusDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            else if (response.statusCode == 404)
            {
                return NotFound(response.message);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // DELETE api/<AttentionStatusController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _attentionStatusBll.DeleteAttentionStatus(id);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            else if (response.statusCode == 404)
            {
                return NotFound(response.message);
            }
            return StatusCode(response.statusCode, response.message);
        }

    }
}
