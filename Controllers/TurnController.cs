using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Helpers;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Turn;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InternetServiceBack.Filters;

namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(UserSessionFilter))]
    [ApiController]
    public class TurnController : ControllerBase
    {
        private readonly TurnBll _turnBll;

        public TurnController(DatabaseInternetServiceContext dbContext)
        {
            _turnBll = new TurnBll(dbContext);
        }

        // GET: api/<TurnController>
        [HttpGet]
        public ActionResult<IEnumerable<TurnDto>> Get()
        {
            var response = _turnBll.GetTurns();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<TurnController>/5
        [HttpGet("{id}")]
        public ActionResult<TurnDto> Get(Guid id)
        {
            var response = _turnBll.GetTurnById(id);
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


        // POST api/<TurnController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] TurnDto turnDto)
        {
            var response = _turnBll.CreateTurn(turnDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<TurnController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] TurnDto turnDto)
        {
            turnDto.TurnID = id;
            var response = _turnBll.UpdateTurn(turnDto);
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

        // DELETE api/<TurnController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _turnBll.DeleteTurn(id);
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
