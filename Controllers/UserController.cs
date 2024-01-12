using Microsoft.AspNetCore.Mvc;
using User_Management_Application.Models;
using User_Management_Application.Service;
using User_Management_Application.Utilities;


namespace User_Management_Application.Controllers
{
 
   

    [Route("api/users")]
    public class UserManagementAppController : Controller
    {
        private readonly UserService _userService;

        public UserManagementAppController(UserService userService)
        {
            _userService = userService;
        }



        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            var response = _userService.CreateUser(user);

            if (response.ResponseCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var response = _userService.GetAllUsers();

            if (response.ResponseCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            var response = _userService.GetUserById(userId);

            if (response.ResponseCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int userId, [FromBody] UserModel updatedUser)
        {
            var response = _userService.UpdateUser(userId, updatedUser);

            if (response.ResponseCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }



        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            var response = _userService.DeleteUser(userId);

            if (response.ResponseCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


    }

}
