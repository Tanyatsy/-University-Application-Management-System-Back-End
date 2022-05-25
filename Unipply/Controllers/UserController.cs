using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipply.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unipply.Models;
using System;
using Unipply.Models.Faculty;
using System.Linq;
using Unipply.Context;
using Microsoft.EntityFrameworkCore;
using Unipply.Models.Specialty;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Unipply.Repositories;
using Unipply.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRecommendationsService _recommendationsService;
        private readonly IUserProfileDataService _userProfileDataService;
        private readonly IUserDataService _userDataService;
        private readonly ISpecialtyDataUserProfileDataRepository _specialtyDataUserProfileDataRepository;
        private readonly DBContext _context;

        public UserController(
            ILogger<UserController> logger,
            IRecommendationsService recommendationsService,
            IUserProfileDataService userProfileDataService,
            IUserDataService userDataService,
            ISpecialtyDataUserProfileDataRepository specialtyDataUserProfileDataRepository,
            DBContext dbContext)
        {
            _logger = logger;
            _recommendationsService = recommendationsService;
            _userProfileDataService = userProfileDataService;
            _userDataService = userDataService;
            _specialtyDataUserProfileDataRepository = specialtyDataUserProfileDataRepository;
            _context = dbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var userProfile = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));
            if (userProfile == null)
            {
                return BadRequest("User Profile does not exist");
            }
            else
            {
                var userModel = new UserModel
                {
                    Id = userProfile.UserDataId,
                    UserName = userProfile.UserData.UserName,
                    Age = userProfile.Age,
                    AboutMe = userProfile.AboutMe,
                    Email = userProfile.UserData.Email,
                    Phone = userProfile.Phone,
                    Gender = userProfile.Gender,
                    Avatar = userProfile.Avatar,
                    Documents = userProfile.Documents,
                    FavoriteSpecialties = userProfile.FavouritesSpecialties
                    .Select(x => new SpecialtyModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Title = x.Title
                    })
                    .ToList(),
                    Hobbies = userProfile.Hobbies,
                    Recommendations = _recommendationsService.ParseRecomendationsFaculties(userProfile.Recommendations).ToList(),
                };

                return Ok(userModel);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserProfileAsync([FromBody] UserProfileData data)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var user = await _userDataService.FindUserByIdAsync(Guid.Parse(userId));
            var userProfile = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));

            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                if (userProfile != null)
                {
                    await _userProfileDataService.UpdateAsync(data);
                    return Ok(data);
                }
                else 
                {
                    data.Id = Guid.NewGuid();
                    data.UserDataId = Guid.Parse(userId);
                    await _userProfileDataService.CreateAsync(data);
                    var userProfileCreated = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));
                    return Ok(userProfileCreated);
                }
            }
        }

        [HttpGet]
        [Route("favourites")]
        public async Task<IActionResult> GetFavouritesSpecialtyForUserAsync()
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var userProfile = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));
            if (userProfile == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                return Ok(userProfile.FavouritesSpecialties.Select(f => f.Id));
            }
        }

        [HttpPut]
        [Route("favourites/{specialtyId}")]
        public async Task<IActionResult> SetFavouritesSpecialtyForUserAsync([FromRoute] Guid specialtyId)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var userProfile = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));
            if (userProfile == null)
            {
                return BadRequest("User does not exist");
            }
            else 
            {
                await _specialtyDataUserProfileDataRepository.CreateAsync(
                       new SpecialtyDataUserProfileData
                       {
                           FavouritesSpecialtiesId = specialtyId,
                           UserProfileDatasId = userProfile.Id,
                       });

                return Ok();
            }
        }

        [HttpDelete]
        [Route("favourites/{specialtyId}")]
        public async Task<IActionResult> DeleteFavouritesSpecialtyForUserAsync([FromRoute] Guid specialtyId)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var userProfile = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));
            if (userProfile == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                await _specialtyDataUserProfileDataRepository.CreateAsync(
                       new SpecialtyDataUserProfileData
                       {
                           FavouritesSpecialtiesId = specialtyId,
                           UserProfileDatasId = userProfile.Id,
                       });

                return Ok();
            }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("recommendedFaculties")]
        public async Task<ActionResult<List<FacultyModel>>> GetRecommendedFacultiesForUserAsync()
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
            var userId = securityToken.Claims.Where(claim => claim.Type.Equals("UserID")).FirstOrDefault()?.Value;
            var userProfileData = await _userProfileDataService.FindUserProfileDataByUserIdAsync(Guid.Parse(userId));

            return userProfileData == null ? NotFound() : Ok(userProfileData.FavouritesFaculties.Select(f =>
            new FacultyModel
            {
                Id = f.Id,
                Title = f.Title,
                Info = f.Info,
                Specialties = f.Specialties
                .Select(s => new SpecialtyModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    Title = s.Title
                }).ToList()
            }).ToList());
        }
    }
}
