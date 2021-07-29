using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces ("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;


        public SecurityController(ISecurityService securityService, IMapper mapper,IPasswordService passwordService) 
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(SecurityDTO securityDTO)
        {
            var security = _mapper.Map<Security>(securityDTO);

            security.Contrasena = _passwordService.Hash(security.Contrasena);

            await _securityService.RegisterUser(security);

            securityDTO = _mapper.Map<SecurityDTO>(security);
            var response = new ApiResponse<SecurityDTO>(securityDTO);
            return Ok(response);
        }
    }
}
