using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Bll;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.User;
using InternetServiceBack.Filters;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;


namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class UserController : ControllerBase
    {
        private readonly UserBll _userBll;

        public UserController(DatabaseInternetServiceContext dbContext)
        {
            _userBll = new UserBll(dbContext);
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var response = _userBll.GetUsers();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(Guid id)
        {
            var response = _userBll.GetUserById(id);
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


        // POST api/<UserController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] UserDto userDto)
        {
            if (userDto.userName.Length < 4 || userDto.userName.Length > 16)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterUserErrorParamUserName,
                });
            }
            if (userDto.password.Length < 8 || userDto.password.Length > 16 || !Regex.IsMatch(userDto.password, "^[a-zA-Z_0-9]+$"))
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterUserErrorParamPassword,
                });
            }
            var response = _userBll.CreateUser(userDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] UserDto userDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "UserID required",
                });
            }
            userDto.userID = id;
            var response = _userBll.UpdateUser(userDto);
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

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _userBll.DeleteUser(id);
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
