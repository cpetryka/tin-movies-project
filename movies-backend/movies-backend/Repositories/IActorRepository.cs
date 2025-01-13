using movies_backend.DTOs;

namespace movies_backend.Repositories;

public interface IActorRepository
{
    /*************************************************************************************************
     * GENDERS MANAGEMENT
     *************************************************************************************************/
    Task<ICollection<GetGenderDto>> GetAllGenders();

    /*************************************************************************************************
     * ACTOR ROLES MANAGEMENT
     *************************************************************************************************/
    Task<bool> DoesActorRoleExist(int actorRoleId);
    Task<ICollection<GetActorRoleDto>> GetAllActorRoles();
    Task<int> AddNewActorRole(string newActorRoleName);

    /*************************************************************************************************
     * ACTORS MANAGEMENT
     *************************************************************************************************/
    Task<bool> DoesActorExist(int actorId);
    Task<GetActorDto?> GetActorById(int id);
    Task<GetActorDto?> GetActorByName(string name);
    Task<ICollection<GetActorDto>> GetAllActors();
    Task<int> AddNewActor(AddNewOrUpdateActorDto addNewOrUpdateActorDto);
    Task<GetActorDto?> UpdateActorById(int id, AddNewOrUpdateActorDto addNewOrUpdateActorDto);
    Task<bool> DeleteActor(int id);
}