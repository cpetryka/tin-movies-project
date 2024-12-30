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

    [HttpGet("get-all-actors")]
    public async Task<IActionResult> GetAllActors()
    {
        var actors = await _actorRepository.GetAllActors();

        return Ok(actors);
    }

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

    [HttpPost("add-new-actor")]
    public async Task<IActionResult> AddNewActor([FromBody] AddNewActorDto addNewActorDto)
    {
        var id = await _actorRepository.AddNewActor(addNewActorDto);

        return Created("", new
        {
            message = "Actor added successfully",
            addedActorId = id
        });
    }

    [HttpDelete("delete-actor")]
    public async Task<IActionResult> DeleteActor([FromQuery] int id)
    {
        var deletedStatus = await _actorRepository.DeleteActor(id);

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

    [HttpGet("get-all-actor-roles")]
    public async Task<IActionResult> GetAllActorRoles()
    {
        var actorRoles = await _actorRepository.GetAllActorRoles();

        return Ok(actorRoles);
    }

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
}