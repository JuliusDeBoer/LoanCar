using LoanCar.Api.DTOs;
using LoanCar.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext _db = db;

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{_id}")]
        public IActionResult GetById(int _id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewUserDTO _userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
