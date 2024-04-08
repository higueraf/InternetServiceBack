using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Helpers;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.AttentionType;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InternetServiceBack.Filters;

namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class AttentionTypeController : ControllerBase
    {
        private readonly AttentionTypeBll _attentionTypeBll;

        public AttentionTypeController(DatabaseInternetServiceContext dbContext)
        {
            _attentionTypeBll = new AttentionTypeBll(dbContext);
        }

        // GET: api/<AttentionTypeController>
        [HttpGet]
        public ActionResult<IEnumerable<AttentionTypeDto>> Get()
        {
            var response = _attentionTypeBll.GetAttentionTypes();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<AttentionTypeController>/5
        [HttpGet("{id}")]
        public ActionResult<AttentionTypeDto> Get(Guid id)
        {
            var response = _attentionTypeBll.GetAttentionTypeById(id);
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


        // POST api/<AttentionTypeController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] AttentionTypeDto attentionTypeDto)
        {
            var response = _attentionTypeBll.CreateAttentionType(attentionTypeDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<AttentionTypeController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] AttentionTypeDto attentionTypeDto)
        {
            if (id != attentionTypeDto.AttentionTypeID)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "AttentionTypeID in URL does not match AttentionTypeID in body",
                });
            }

            var response = _attentionTypeBll.UpdateAttentionType(attentionTypeDto);
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

        // DELETE api/<AttentionTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _attentionTypeBll.DeleteAttentionType(id);
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
