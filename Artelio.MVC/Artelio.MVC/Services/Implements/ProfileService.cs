using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.Entities;
using Artelio.MVC.Exceptions.Common;
using Artelio.MVC.Extensions;
using Artelio.MVC.Objects;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Artelio.MVC.Services.Implements
{
    public class ProfileService(ArtelioContext _context, UserManager<AppUser> _userManager, INotificationService _notificationService) : IProfileService
    {
        //Get

        public async Task<UpdateProjectDTO> GetProject(string projectId)
        {
            Project dto = await _context.Projects.FirstOrDefaultAsync(u => u.Id == projectId);
            List<UpdateImageProjectDTO> images = await _context.ProjectImages.Where(u => u.ProjectId == dto.Id).Select(u => new UpdateImageProjectDTO
            {
                Id = u.Id,
                ImageUrl = u.ImageUrl,
                ProjectId = dto.Id
            }).ToListAsync();

            UpdateProjectDTO updateDto = new UpdateProjectDTO
            {
                ProjectName = dto.ProjectName,
                Title = dto.Title,
                About = dto.About,
                ImageUrl = dto.ImageUrl,
                ImageUrls = images
            };

            return updateDto;
        }

        public async Task<GetProjectOfUserDTO> GetProjectOfUser(string projectId)
        {
            UserProject dto = await _context.UserProjects.Include(u => u.User).Include(u => u.Project).FirstOrDefaultAsync(u => u.ProjectId == projectId);
            List<GetImageProjectDTO> images = await _context.ProjectImages.Where(u => u.ProjectId == dto.ProjectId).Select(u => new GetImageProjectDTO
            {
                Id = u.Id,
                ImageUrl = u.ImageUrl,
                ProjectId = dto.Id,
                Order = u.Order
                
            }).OrderBy(u => u.Order).ToListAsync();

            GetProjectOfUserDTO getDto = new GetProjectOfUserDTO
            {
                ProjectName = dto.Project.ProjectName,
                Title = dto.Project.Title,
                About = dto.Project.About,
                ImageUrl = dto.Project.ImageUrl,
                UserId = dto.UserId,
                UserImage = dto.User.ImageUrl,
                UserName = dto.User.UserName,
                ImageUrls = images
            };

            return getDto;
        }

        public async Task<List<GetAllUserProjectDTO>> GetUserProjects(string UserId)
        {
            List<GetAllUserProjectDTO> dto = await _context.UserProjects.Include(u => u.Project).Where(u => u.UserId == UserId).Select(u => new GetAllUserProjectDTO
            {
                Id = u.Project.Id,
                ProjectName = u.Project.ProjectName,
                Title = u.Project.Title,
                About = u.Project.About,
                ImageUrl = u.Project.ImageUrl,
                ImageUrls = _context.ProjectImages.Where(x => x.ProjectId == u.ProjectId).Select(x => new GetAllImageProjectDTO
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    ImageUrl = x.ImageUrl
                }).ToList()
            }).ToListAsync();

            return dto;
        }

        public async Task<List<GetAllProject>> GetAllProject()
        {
            List<GetAllProject> dto = await _context.UserProjects.Include(u => u.Project).Include(u => u.User).Select(u => new GetAllProject
            {
                Id = u.Project.Id,
                ProjectName = u.Project.ProjectName,
                ImageUrl = u.Project.ImageUrl,
                UserId = u.User.Id,
                UserImageUrl = u.User.ImageUrl,
                Fullname = u.User.Name + " " + u.User.Surname,
            }).ToListAsync();
            Random rnd = new Random();
            dto = dto.OrderBy(x => rnd.Next()).ToList();


            return dto;
        }

        public async Task<bool> CheckViewer(string projectId, string userId)
        {
            UserProject project = await _context.UserProjects.FirstOrDefaultAsync(u => u.ProjectId == projectId);
            bool res = await _context.viewerProjects.Include(u => u.Project).AnyAsync(u => u.UserId == userId && project.Id == u.ProjectId);

            return res;
        }

        public Task<List<GetUserFriendsDTO>> GetUserFriends(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetUserProjectsDTO>> GetOnlyUserProjects(string userId)
        {
            
            List<GetUserProjectsDTO> dto = await _context.UserProjects.Where(u => u.UserId == userId).Select(u => new GetUserProjectsDTO
            {
                Id = u.ProjectId,
                ProjectName = u.Project.ProjectName,
                ImageUrl= u.Project.ImageUrl.Substring(20),
                UserId = u.User.Id,
                UserImageUrl = u.User.ImageUrl,
                UserName = u.User.UserName,
                ViewerCount = _context.viewerProjects.Where(a => a.ProjectId == u.Id).Count(),
            }).ToListAsync();

            return dto;
        }

        public async Task<int> GetUserViewerCount(string userId)
        {
            int viewer = _context.viewerProjects.Where(u => u.Project.UserId == userId).Count();

            return viewer;
        }

        public async Task<int> GetUserPostCount(string userId)
        {
            int post = _context.UserProjects.Where(u => u.UserId == userId).Count();

            return post; 
        }
            


        //Create
        public async Task CreateProject(CreateProjectDTO data, string UserId, string environment)
        {
            if (data.Image != null)
            {
                if (!data.Image.IsValidType("image"))
                {
                    throw new FileNotSuitableException("Type Error");
                }
                if (!data.Image.IsValidSize(1000))
                {
                    throw new FileNotSuitableException("Size Error");
                }
            }

            string fileName = await data.Image.FileManagedAsync(Path.Combine(environment, "assets", "imgs", "Project"));

            Project project = new Project
            {
                ProjectName = data.ProjectName,
                Title = data.Title,
                About = data.About,
                ImageUrl = Path.Combine("assets", "imgs", "Project", fileName)
            };

            await _context.Projects.AddAsync(project);


            for (int i = 0; i < data.Images.Count; i++)
            {


                string imgsName = await data.Images[i].FileManagedAsync(Path.Combine(environment, "assets", "imgs", "Project"));

                ProjectImages projectImg = new ProjectImages
                {
                    ImageUrl = Path.Combine("assets", "imgs", "Project", imgsName),
                    ProjectId = project.Id,
                    Order = i,
                };

                await _context.ProjectImages.AddAsync(projectImg);


                await _context.SaveChangesAsync();

            }

            UserProject userProject = new UserProject
            {
                UserId = UserId,
                ProjectId = project.Id,
            };

            await _context.UserProjects.AddAsync(userProject);

            NotificationObject @object = new NotificationObject
            {
                ContentId = project.Id,
                ContentAction = "Detail",
                ContentController = "Home",
                ContentText = "shared project. You can look him project."
            };

            await _context.SaveChangesAsync();
            await _notificationService.CreateNotification(UserId, @object);
        }

        public async Task ViewProject(string projectId, string userId)
        {
            UserProject project = await _context.UserProjects.FirstOrDefaultAsync(u => u.ProjectId ==  projectId);

            ViewerProject viewer = new ViewerProject
            {
                UserId = userId,
                ProjectId = project.Id
            };

            await _context.viewerProjects.AddAsync(viewer);
            await _context.SaveChangesAsync();
        }


    }
}
