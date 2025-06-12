using Artelio.MVC.DTOs.Profile;

namespace Artelio.MVC.Services.Interfaces
{
    public interface IProfileService
    {
        //Get
        Task<List<GetAllUserProjectDTO>> GetUserProjects(string UserId);
        Task<UpdateProjectDTO> GetProject(string projectId);
        Task<GetProjectOfUserDTO> GetProjectOfUser(string projectId);
        Task<List<GetAllProject>> GetAllProject();
        Task<bool> CheckViewer(string projectId, string userId);
        Task<List<GetUserFriendsDTO>> GetUserFriends(string userId);
        Task<List<GetUserProjectsDTO>> GetOnlyUserProjects(string userId);
        Task<int> GetUserViewerCount(string userId);
        Task<int> GetUserPostCount(string userId);


        //Create
        Task CreateProject(CreateProjectDTO data, string UserId, string environment);
        Task ViewProject(string projectId, string userId);
    }
}
