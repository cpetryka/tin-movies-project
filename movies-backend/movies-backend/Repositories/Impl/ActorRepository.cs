using Microsoft.EntityFrameworkCore;
using movies_backend.Data;
using movies_backend.DTOs;
using movies_backend.Model;

namespace movies_backend.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly ApplicationContext _context;

    public ActorRepository(ApplicationContext context)
    {
        _context = context;
    }

    /*************************************************************************************************
     * GENDERS MANAGEMENT
     *************************************************************************************************/

    public async Task<ICollection<GetGenderDto>> GetAllGenders()
    {
        var gendersList = await _context.Genders.ToListAsync();

        return gendersList.Select(g => new GetGenderDto
        {
            Name = g.Name
        }).ToList();
    }

    /*************************************************************************************************
     * ACTOR ROLES MANAGEMENT
     *************************************************************************************************/

    public async Task<bool> DoesActorRoleExist(int actorRoleId)
    {
        return await _context.ActorRoles.AnyAsync(ar => ar.Id == actorRoleId);
    }

    public async Task<ICollection<GetActorRoleDto>> GetAllActorRoles()
    {
        var actorRolesList = await _context.ActorRoles.ToListAsync();

        return actorRolesList.Select(ar => new GetActorRoleDto
        {
            Name = ar.Name
        }).ToList();
    }

    public async Task<int> AddNewActorRole(string newActorRoleName)
    {
        var actorRole = new ActorRole { Name = newActorRoleName };
        await _context.AddAsync(actorRole);
        await _context.SaveChangesAsync();

        return actorRole.Id;
    }

    /*************************************************************************************************
     * ACTORS MANAGEMENT
     *************************************************************************************************/

    public async Task<bool> DoesActorExist(int actorId)
    {
        return await _context.Actors.AnyAsync(a => a.Id == actorId);
    }

    public async Task<GetActorDto?> GetActorById(int id)
    {
        var actor = await _context.Actors
            .Include(a => a.Gender)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actor == null)
        {
            return null;
        }

        return new GetActorDto()
        {
            Id = actor.Id,
            Name = actor.Name,
            GenderId = actor.GenderId,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        };
    }

    public async Task<GetActorDto?> GetActorByName(string name)
    {
        var actor = await _context.Actors
            .Include(a => a.Gender)
            .FirstOrDefaultAsync(a => a.Name == name);

        if (actor == null)
        {
            return null;
        }

        return new GetActorDto()
        {
            Id = actor.Id,
            Name = actor.Name,
            GenderId = actor.GenderId,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        };
    }

    public async Task<ICollection<GetActorDto>> GetAllActors()
    {
        var actorsList = await _context.Actors
            .Include(a => a.Gender)
            .ToListAsync();

        return actorsList.Select(actor => new GetActorDto
        {
            Id = actor.Id,
            Name = actor.Name,
            GenderId = actor.GenderId,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        }).ToList();
    }

    public async Task<int> AddNewActor(AddNewOrUpdateActorDto addNewOrUpdateActorDto)
    {
        var actor = new Actor
        {
            Name = addNewOrUpdateActorDto.Name,
            GenderId = addNewOrUpdateActorDto.GenderId,
            BirthDate = new DateOnly(addNewOrUpdateActorDto.BirthDate.Year, addNewOrUpdateActorDto.BirthDate.Month, addNewOrUpdateActorDto.BirthDate.Day),
            DeathDate = new DateOnly(addNewOrUpdateActorDto.DeathDate.Year, addNewOrUpdateActorDto.DeathDate.Month, addNewOrUpdateActorDto.DeathDate.Day),
            Biography = addNewOrUpdateActorDto.Biography
        };

        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();

        return actor.Id;
    }

    public async Task<GetActorDto?> UpdateActorById(int id, AddNewOrUpdateActorDto addNewOrUpdateActorDto)
    {
        var actor = await _context.Actors
            .Include(a => a.Gender)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (actor == null)
            return null;

        actor.Name = addNewOrUpdateActorDto.Name;
        actor.GenderId = addNewOrUpdateActorDto.GenderId;
        actor.BirthDate = new DateOnly(addNewOrUpdateActorDto.BirthDate.Year, addNewOrUpdateActorDto.BirthDate.Month, addNewOrUpdateActorDto.BirthDate.Day);
        actor.DeathDate = new DateOnly(addNewOrUpdateActorDto.DeathDate.Year, addNewOrUpdateActorDto.DeathDate.Month,
            addNewOrUpdateActorDto.DeathDate.Day);
        actor.Biography = addNewOrUpdateActorDto.Biography;

        await _context.SaveChangesAsync();

        return new GetActorDto
        {
            Id = actor.Id,
            Name = actor.Name,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        };
    }

    public async Task<bool> DeleteActor(int actorId)
    {
        if (!DoesActorExist(actorId).Result)
        {
            return false;
        }

        _context.Actors.Remove(new Actor { Id = actorId });
        await _context.SaveChangesAsync();

        return true;
    }
}
