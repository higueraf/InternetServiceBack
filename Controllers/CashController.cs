using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Helpers;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Cash;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InternetServiceBack.Filters;

namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class CashController : ControllerBase
    {
        private readonly CashBll _cashBll;

        public CashController(DatabaseInternetServiceContext dbContext)
        {
            _cashBll = new CashBll(dbContext);
        }

        // GET: api/<CashController>
        [HttpGet]
        public ActionResult<IEnumerable<CashDto>> Get()
        {
            var response = _cashBll.GetCashs();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<CashController>/5
        [HttpGet("{id}")]
        public ActionResult<CashDto> Get(Guid id)
        {
            var response = _cashBll.GetCashById(id);
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


        // POST api/<CashController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] CashDto cashDto)
        {
            var response = _cashBll.CreateCash(cashDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<CashController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] CashDto cashDto)
        {
            if (id != cashDto.CashID)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "CashID in URL does not match CashID in body",
                });
            }

            var response = _cashBll.UpdateCash(cashDto);
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

        // DELETE api/<CashController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _cashBll.DeleteCash(id);
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
