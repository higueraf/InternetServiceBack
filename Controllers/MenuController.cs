using InternetServiceBack.Bll;
using InternetServiceBack.Models;
using Microsoft.AspNetCore.Mvc;
using InternetServiceBack.Helpers;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Menu;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using InternetServiceBack.Filters;

namespace InternetServiceBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserSessionFilter))]
    public class MenuController : ControllerBase
    {
        private readonly MenuBll _menuBll;

        public MenuController(DatabaseInternetServiceContext dbContext)
        {
            _menuBll = new MenuBll(dbContext);
        }

        // GET: api/<MenuController>
        [HttpGet]
        public ActionResult<IEnumerable<MenuDto>> Get()
        {
            var response = _menuBll.GetMenus();
            if (response.statusCode == 200)
            {
                return Ok(response.data);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public ActionResult<MenuDto> Get(Guid id)
        {
            var response = _menuBll.GetMenuById(id);
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


        // POST api/<MenuController>
        [HttpPost]
        public ActionResult<GenericResponseDto<bool>> Post([FromBody] MenuDto menuDto)
        {
            var response = _menuBll.CreateMenu(menuDto);
            if (response.statusCode == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.statusCode, response.message);
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] MenuDto menuDto)
        {
            if (id != menuDto.MenuID)
            {
                return BadRequest(new GenericResponseDto<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = "MenuID in URL does not match MenuID in body",
                });
            }

            var response = _menuBll.UpdateMenu(menuDto);
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

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = _menuBll.DeleteMenu(id);
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
