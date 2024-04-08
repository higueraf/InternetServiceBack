using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Bll;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Client;
using InternetServiceBack.Filters;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;


namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class ClientController : ControllerBase
    {
        private readonly ClientBll _clientBll;

        public ClientController(DatabaseInternetServiceContext dbContext)
        {
            _clientBll = new ClientBll(dbContext);
        }

        // GET: api/<ClientController>
        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> Get()
        {
            var response = _clientBll.GetClients();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public ActionResult<ClientDto> Get(Guid id)
        {
            var response = _clientBll.GetClientById(id);
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


        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] ClientDto clientDto)
        {
            if (clientDto.Identification.Length < 10 || clientDto.Identification.Length > 13 || !Regex.IsMatch(clientDto.Identification, "^[0-9]+$"))
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterClientErrorIdentification,
                });
            }
            if (clientDto.Address.Length < 20 || clientDto.Address.Length > 100)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterClientErrorAddress,
                });
            }
            var response = _clientBll.CreateClient(clientDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ClientDto clientDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "ClientID required",
                });
            }
            clientDto.ClientID = id;
            var response = _clientBll.UpdateClient(clientDto);
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

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _clientBll.DeleteClient(id);
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
