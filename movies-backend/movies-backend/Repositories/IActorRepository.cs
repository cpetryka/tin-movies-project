using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface IActorRepository
{
    Task<bool> DoesActorExist(int actorId);
    Task<int> AddNewActor(AddNewActorDto addNewActorDto);
    Task<ICollection<GetActorDto>> GetAllActors();
    Task<GetActorDto?> GetActorById(int id);
    Task<GetActorDto?> GetActorByName(string name);
    Task<bool> DeleteActor(int id);

    Task<bool> DoesActorRoleExist(int actorRoleId);
    Task<int> AddNewActorRole(string newActorRoleName);
    Task<ICollection<GetActorRoleDto>> GetAllActorRoles();
}