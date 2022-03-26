using aninja_auth_service.Authorization;
using aninja_auth_service.Commands;
using aninja_auth_service.Dtos;
using aninja_auth_service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace aninja_auth_service.Controllers
{
    [ApiController]
    [Route("api/a/user")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private IJwtService _jwtService;
        public UserController(IMediator mediator, IMapper mapper, IJwtService jwtService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtService = jwtService;
         }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser([FromBody] UserLoginDto userLogin)
        {
            var request = new UserLoginCommand() { Name = userLogin.Name, Password = userLogin.Password };
            var result = await _mediator.Send(request);
            if (result is null) return NotFound();
            var token = _jwtService.GetJwtToken(result);
            HttpContext.Response.Headers.Add("AuthToken", token);
            return Ok(_mapper.Map<UserDto>(result));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegisterDto userRegister)
        {
            var request = new UserRegisterCommand() { Email = userRegister.Email, Name = userRegister.Name, Password = userRegister.Password };
            var result = await _mediator.Send(request);
            if (result is null) return Forbid();
            return Ok(_mapper.Map<UserDto>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            Guid guid;
            try
            {
                guid = Guid.Parse(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            var request = new GetUserByIdQuery() { Id = guid };
            var result = await _mediator.Send(request);
            if(result is null) return NotFound();
            return Ok(_mapper.Map<UserDto>(result));
        }
    }
}
