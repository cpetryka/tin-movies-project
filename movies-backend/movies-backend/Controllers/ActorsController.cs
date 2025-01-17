using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_backend.DTOs;
using movies_backend.Repositories;

namespace movies_backend.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorRepository _actorRepository;

    public ActorsController(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    /*************************************************************************************************
     * GENDERS MANAGEMENT
     *************************************************************************************************/

    [HttpGet("get-all-genders")]
    public async Task<IActionResult> GetAllGenders()
    {
        var genders = await _actorRepository.GetAllGenders();

        return Ok(genders);
    }

    /*************************************************************************************************
     * ACTOR ROLES MANAGEMENT
     *************************************************************************************************/

    [HttpGet("get-all-actor-roles")]
    public async Task<IActionResult> GetAllActorRoles()
    {
        var actorRoles = await _actorRepository.GetAllActorRoles();

        return Ok(actorRoles);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add-new-actor-role")]
    public async Task<IActionResult> AddNewActorRole([FromBody] String addNewActorRoleDto)
    {
        var id = await _actorRepository.AddNewActorRole(addNewActorRoleDto);

        return Created("", new
        {
            message = "Actor role added successfully",
            addedActorRoleId = id
        });
    }

    /*************************************************************************************************
     * ACTORS MANAGEMENT
     *************************************************************************************************/

    [HttpGet("get-actor-by-id")]
    public async Task<IActionResult> GetActorById([FromQuery] int id)
    {
        var actor = await _actorRepository.GetActorById(id);

        if (actor == null)
        {
            return NotFound(new
            {
                message = "Actor not found"
            });
        }

        return Ok(actor);
    }

    [HttpGet("get-actor-by-name")]
    public async Task<IActionResult> GetActorByName([FromQuery] string name)
    {
        var actor = await _actorRepository.GetActorByName(name);

        if (actor == null)
        {
            return NotFound(new
            {
                message = "Actor not found"
            });
        }

        return Ok(actor);
    }

    [HttpGet("get-all-actors")]
    public async Task<IActionResult> GetAllActors()
    {
        var actors = await _actorRepository.GetAllActors();

        return Ok(actors);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add-new-actor")]
    public async Task<IActionResult> AddNewActor([FromBody] AddNewOrUpdateActorDto addNewOrUpdateActorDto)
    {
        var id = await _actorRepository.AddNewActor(addNewOrUpdateActorDto);

        return Created("", new
        {
            message = "Actor added successfully",
            addedActorId = id
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update-actor-by-id")]
    public async Task<IActionResult> UpdateActor([FromQuery] int actorId, [FromBody] AddNewOrUpdateActorDto addNewOrUpdateActorDto)
    {
        var updatedActor = await _actorRepository.UpdateActorById(actorId, addNewOrUpdateActorDto);

        if (updatedActor == null)
            return NotFound("Actor not found.");

        return Ok(updatedActor);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-actor-by-id")]
    public async Task<IActionResult> DeleteActor([FromQuery] int actorId)
    {
        var deletedStatus = await _actorRepository.DeleteActor(actorId);

        if (!deletedStatus)
        {
            return NotFound(new
            {
                message = "Actor not found"
            });
        }

        return Ok(new
        {
            message = "Actor deleted successfully"
        });
    }
}