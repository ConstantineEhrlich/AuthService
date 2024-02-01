using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profiles;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ProfileService profiles, ILogger<ProfileController> logger)
    {
        _profiles = profiles;
        _logger = logger;
    }
    
    
    [HttpGet("")]
    public async Task<ActionResult<Profile>> GetProfile()
    {
        return await GetProfile(User.Identity!.Name!);
    }
    

    [HttpGet("{profileLogin}")]
    public async Task<ActionResult<Profile>> GetProfile(string profileLogin)
    {
        return Ok(await _profiles.GetProfile(profileLogin));
    }

    [HttpPut("update")]
    public async Task<ActionResult<Profile>> UpdateProfile([FromBody] Profile updatedProfile)
    {
        await _profiles.UpdateProfile(updatedProfile);
        return await GetProfile();
    }
}