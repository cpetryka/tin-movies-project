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

    public async Task<bool> DoesActorExist(int actorId)
    {
        return await _context.Actors.AnyAsync(a => a.Id == actorId);
    }

    public async Task<int> AddNewActor(AddNewActorDto addNewActorDto)
    {
        var actor = new Actor
        {
            Name = addNewActorDto.Name,
            GenderId = addNewActorDto.GenderId,
            BirthDate = new DateOnly(addNewActorDto.BirthDate.Year, addNewActorDto.BirthDate.Month, addNewActorDto.BirthDate.Day),
            DeathDate = new DateOnly(addNewActorDto.DeathDate.Year, addNewActorDto.DeathDate.Month, addNewActorDto.DeathDate.Day),
            Biography = addNewActorDto.Biography
        };

        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();

        return actor.Id;
    }

    public async Task<ICollection<GetActorDto>> GetAllActors()
    {
        var actorsList = await _context.Actors
            .Include(a => a.Gender)
            .ToListAsync();

        return actorsList.Select(actor => new GetActorDto
        {
            Name = actor.Name,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        }).ToList();
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
            Name = actor.Name,
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
            Name = actor.Name,
            GenderName = actor.Gender.Name,
            BirthDate = actor.BirthDate,
            DeathDate = actor.DeathDate,
            Biography = actor.Biography
        };
    }

    public async Task<bool> DeleteActor(int id)
    {
        if (!DoesActorExist(id).Result)
        {
            return false;
        }

        _context.Actors.Remove(new Actor { Id = id });
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DoesActorRoleExist(int actorRoleId)
    {
        return await _context.ActorRoles.AnyAsync(ar => ar.Id == actorRoleId);
    }

    public async Task<int> AddNewActorRole(string newActorRoleName)
    {
        var actorRole = new ActorRole { Name = newActorRoleName };
        await _context.AddAsync(actorRole);
        await _context.SaveChangesAsync();

        return actorRole.Id;
    }

    public async Task<ICollection<GetActorRoleDto>> GetAllActorRoles()
    {
        var actorRolesList = await _context.ActorRoles.ToListAsync();

        return actorRolesList.Select(ar => new GetActorRoleDto
        {
            Name = ar.Name
        }).ToList();
    }
}
