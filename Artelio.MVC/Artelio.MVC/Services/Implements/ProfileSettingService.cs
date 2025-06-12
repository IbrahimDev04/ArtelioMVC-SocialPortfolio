using System;
using System.Collections.Generic;
using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.Entities;
using Artelio.MVC.Enums;
using Artelio.MVC.Exceptions.Common;
using Artelio.MVC.Extensions;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Artelio.MVC.Services.Implements
{
    public class ProfileSettingService(ArtelioContext _context, UserManager<AppUser> _userManager) : IProfileSettingService
    {
        //Get
        public async Task<UpdateBasicInfoDTO> GetBasicInfo(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            UpdateBasicInfoDTO dto = new UpdateBasicInfoDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Job = user.Job,
                Company = user.Company,
                City = user.City,
                WebUrl = user.WebUrl,
                About = user.About,
                Countries = Enum.GetValues(typeof(CountryEnum))
                      .Cast<CountryEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                ImageUrl = user.ImageUrl,
                Country = Enum.TryParse(typeof(CountryEnum), user.Country, ignoreCase: true, out var result) ? (CountryEnum)result : default,
            };

            return dto;
        }

        public async Task<UpdateUserExperienceDTO> GetWorkExperience(string workId)
        {
            Work work = await _context.Works.FirstOrDefaultAsync(x => x.Id == workId);

            UpdateUserExperienceDTO dto = new UpdateUserExperienceDTO
            {
                Company = work.Company,
                JobName = work.JobName,
                Countries = Enum.GetValues(typeof(CountryEnum))
                      .Cast<CountryEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                Country = Enum.TryParse(typeof(CountryEnum), work.Country, ignoreCase: true, out var result) ? (CountryEnum)result : default,
                City = work.City,
                About = work.About,
                StartDate = work.StatDate,
                EndDate = work.EndDate,
                PhoneNumber = work.PhoneNumber,
                WebUrl = work.WebUrl
            };

            return dto;
        }

        public async Task<UpdateSocialMediaInfoDTO> GetSocialMediaInfo(string userId)
        {
            UserSocialMedia data = await _context.UserSocialMedias.Include(u => u.SocialMedia).Include(u => u.User).FirstOrDefaultAsync(u => u.User.Id == userId);

            UpdateSocialMediaInfoDTO dto = new UpdateSocialMediaInfoDTO
            {
                FacebookUrl = data.SocialMedia.FacebookUrl,
                GitHubUrl = data.SocialMedia.GitHubUrl,
                InstagramUrl = data.SocialMedia.InstagramUrl,
                LinkedInUrl = data.SocialMedia.LinkedInUrl,
                TwitterUrl = data.SocialMedia.TwitterUrl,
                YouTubeUrl = data.SocialMedia.YouTubeUrl,
            };

            return dto;
        }

        public async Task<List<GetUserWorkExperienceDTO>> GetWorkExperiences(string UserId)
        {
            List<GetUserWorkExperienceDTO> dto = await _context.WorkExperiences.Where(u => u.UserId == UserId).Select(u => new GetUserWorkExperienceDTO
            {
                Id = u.Work.Id,
                UserId = u.UserId,
                Company = u.Work.Company,
                JobName = u.Work.JobName,
                About = u.Work.About,
                City = u.Work.City,
                Country = u.Work.Country,
                EndDate = u.Work.EndDate != default ? Convert.ToDateTime(u.Work.EndDate).ToString("dd.MM.yyyy") : "Continue",
                PhoneNumber = u.Work.PhoneNumber,
                StatDate = u.Work.StatDate.ToString("dd.MM.yyyy")
            }).ToListAsync();

            return dto;
        }

        public async Task<List<GetUserAwardDTO>> GetUserAwards(string UserId)
        {

            List<GetUserAwardDTO> data = await _context.UserAwards.Where(u => u.UserId == UserId).Select(u => new GetUserAwardDTO
            {
                Id = u.Award.Id,
                Company = u.Award.Company,
                AwardName = u.Award.AwardName,
                Date = u.Award.Date.ToString("dd.MM.yyyy"),
                ImageUrl = u.Award.ImageUrl,

            }).ToListAsync();

            

            return data;
        }

        public async Task<UpdateUserAwardDTO> GetUserAward(string awardId)
        {
            Award award = await _context.Awards.FirstOrDefaultAsync(u => u.Id ==  awardId);

            UpdateUserAwardDTO dto = new UpdateUserAwardDTO
            {
                Company = award.Company,
                AwardName = award.AwardName,
                Date = award.Date,
                ImageUrl = award.ImageUrl
            };

            return dto;
        }

        public async Task<List<GetLanguageDTO>> GetAllLanguages(string UserId)
        {
            List<GetLanguageDTO> dto = await _context.UserLanguages.Where(u => u.UserId == UserId).Select(u => new GetLanguageDTO
            {
                ExpertiseLevel = u.ExpertiseLevel,
                LanguageName = u.LanguageName,
                UserId = UserId,
                Id = u.Id
            }).ToListAsync();

            return dto;
        }

        public async Task<List<GetAbilityDTO>> GetAllAbility(string UserId)
        {
            List<GetAbilityDTO> dto = await _context.UserAbilities.Where(u => u.UserId == UserId).Select(u => new GetAbilityDTO
            {
                Id = u.Id,
                AbilityName = u.AbilityName,
                UserId = u.UserId,
            }).ToListAsync();

            return dto;
        }

        public async Task<GetProfileSocialMediaDTO> GetSocialMediaProfile(string UserId)
        {
            UserSocialMedia data = await _context.UserSocialMedias.Include(u => u.SocialMedia).FirstOrDefaultAsync(u => u.UserId == UserId);

            GetProfileSocialMediaDTO dto = new GetProfileSocialMediaDTO
            {
                FacebookUrl = data.SocialMedia.FacebookUrl,
                GitHubUrl = data.SocialMedia.GitHubUrl,
                InstagramUrl = data.SocialMedia.InstagramUrl,
                LinkedInUrl = data.SocialMedia.LinkedInUrl,
                TwitterUrl = data.SocialMedia.TwitterUrl,
                YouTubeUrl = data.SocialMedia.YouTubeUrl
            };

            return dto;
        }

        public async Task<List<GetEducationDTO>> GetUserEducations(string UserId)
        {
            List<GetEducationDTO> dto = await _context.UserEducations.Where(u => u.UserId == UserId).Select(u => new GetEducationDTO
            {
                SchoolName = u.Education.SchoolName,
                Faculty = u.Education.Faculty,
                Specialty = u.Education.Specialty,
                About = u.Education.About,
                StartDate = u.Education.StartDate.ToString("dd.MM.yyyy"),
                EndDate = u.Education.EndDate != default ? Convert.ToDateTime(u.Education.EndDate).ToString("dd.MM.yyyy") : "Continue",
                City = u.Education.City,
                Id = u.Education.Id,
                Country = u.Education.Country,
                Degree = u.Education.Degree                
            }).ToListAsync();

            return dto;
        }

        public async Task<UpdateUserEducationDTO> GetUserEducation(string educationId)
        {
            Education education = await _context.Educations.FirstOrDefaultAsync(u => u.Id == educationId);

            UpdateUserEducationDTO dto = new UpdateUserEducationDTO
            {
                SchoolName = education.SchoolName,
                Faculty = education.Faculty,
                Specialty = education.Specialty,
                About = education.About,
                City = education.City,
                Countries = Enum.GetValues(typeof(CountryEnum))
                      .Cast<CountryEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                Degrees = Enum.GetValues(typeof(DegreeEnum))
                      .Cast<DegreeEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                Country = Enum.TryParse(typeof(CountryEnum), education.Country, ignoreCase: true, out var resulta) ? (CountryEnum)resulta : default,
                Degree = Enum.TryParse(typeof(DegreeEnum), education.Degree, ignoreCase: true, out var resultb) ? (DegreeEnum)resultb : default,
                EndDate = education.EndDate,
                StartDate = education.StartDate,
            };

            return dto;
        }


        //Create
        public async Task CreateSocialMediaAsync(string userId)
        {
            SocialMediaLink media = new SocialMediaLink
            {
                FacebookUrl = null,
                GitHubUrl = null,
                InstagramUrl = null,
                LinkedInUrl = null,
                TwitterUrl = null,
                YouTubeUrl = null
            };

            UserSocialMedia userMedia = new UserSocialMedia
            {
                SocialMediaId = media.Id,
                UserId = userId,
            };

            var res = await _context.SocialMediaLinks.AddAsync(media);
            await _context.UserSocialMedias.AddAsync(userMedia);


            await _context.SaveChangesAsync();
        }

        public async Task CreateUserExperienceAsync(CreateUserExperienceDTO dto, string UserId)
        {
            if (dto.StartDate >= dto.EndDate && dto.EndDate != default)
                throw new Exception();

            Work work = new Work
            {
                Company = dto.Company,
                JobName = dto.JobName,
                Country = dto.Country.ToString(),
                City = dto.City,
                StatDate = Convert.ToDateTime(dto.StartDate),
                EndDate = dto.EndDate,
                About = dto.About,
                PhoneNumber = dto.PhoneNumber,
                WebUrl = dto.WebUrl,
            };

            WorkExperience experience = new WorkExperience
            {
                UserId = UserId,
                WorkId = work.Id,
            };

            await _context.Works.AddAsync(work);
            await _context.WorkExperiences.AddAsync(experience);

            await _context.SaveChangesAsync();
        }

        public async Task CreateUserAward(CreateAwardDTO dto, string UserId, string environment)
        {
            if (dto.Image != null)
            {
                if (!dto.Image.IsValidType("image"))
                {
                    throw new FileNotSuitableException("Type Error");
                }
                if (!dto.Image.IsValidSize(1000))
                {
                    throw new FileNotSuitableException("Size Error");
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            string fileName = await dto.Image.FileManagedAsync(Path.Combine(environment, "assets", "imgs", "Award"));

            Award award = new Award
            {
                Company = dto.Company,
                AwardName = dto.AwardName,
                Date = dto.Date,
                ImageUrl = Path.Combine("assets", "imgs", "Award", fileName)
            };

            UserAward userAward = new UserAward
            {
                UserId = UserId,
                AwardId = award.Id,
            };

            await _context.Awards.AddAsync(award);
            await _context.UserAwards.AddAsync(userAward);

            await _context.SaveChangesAsync();
        }

        public async Task CreateUserLanguage(CreateLanguageDTO dto, string UserId, string[] Level)
        {
            UserLanguage language = new UserLanguage
            {
                UserId = UserId,
                LanguageName = dto.Language.ToString(),
                ExpertiseLevel = Level[dto.ExpertiseLevel],
            };

            await _context.UserLanguages.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAbiliy(CreateAbilityDTO dto, string UserId)
        {
            if (dto.AbilityName != null && dto.AbilityName.Length > 0)
            {
                foreach (var skill in dto.AbilityName)
                {
                    // Boş və ya whitespace checking
                    if (string.IsNullOrWhiteSpace(skill))
                        continue;

                    UserAbility userSkill = new UserAbility
                    {
                        UserId = UserId,
                        AbilityName = skill.Trim()
                    };
                    await _context.UserAbilities.AddAsync(userSkill);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateEducation(CreateEducationDTO dto, string UserId)
        {
            if (dto.StartDate >= dto.EndDate && dto.EndDate != default)
                throw new Exception();

            Education education = new Education
            {
                SchoolName = dto.SchoolName,
                Faculty = dto.Faculty,
                Specialty = dto.Specialty,
                About = dto.About,
                Country = dto.Country.ToString(),
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Degree = dto.Degree.ToString(),
            };

            UserEducation userEducation = new UserEducation
            {
                EducationId = education.Id,
                UserId = UserId,
            };

            await _context.Educations.AddAsync(education);
            await _context.UserEducations.AddAsync(userEducation);

            await _context.SaveChangesAsync();
        }


        //Update
        public async Task UpdateBasicInfo(UpdateBasicInfoDTO data, string userId, string environment)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

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

                string fileName = await data.Image.FileManagedAsync(Path.Combine(environment, "assets", "imgs", "Profile"));
                user.ImageUrl = Path.Combine("assets", "imgs", "Profile", fileName);
            }


            user.Name = data.Name;
            user.Surname = data.Surname;
            user.Job = data.Job;
            user.Company = data.Company;
            user.Country = data.Country.ToString();
            user.City = data.City;
            user.WebUrl = data.WebUrl;
            user.About = data.About;


            await _context.SaveChangesAsync();

            

        }

        public async Task UpdateSocialMediaInfo(UpdateSocialMediaInfoDTO data, string userId)
        {
            SocialMediaLink mediaLink = await _context.SocialMediaLinks.FirstOrDefaultAsync(u => _context.UserSocialMedias.Any(a => a.UserId == userId && a.SocialMediaId == u.Id));

            mediaLink.InstagramUrl = data.InstagramUrl;
            mediaLink.FacebookUrl = data.FacebookUrl;
            mediaLink.YouTubeUrl = data.YouTubeUrl;
            mediaLink.GitHubUrl = data.GitHubUrl;
            mediaLink.TwitterUrl = data.TwitterUrl;
            mediaLink.LinkedInUrl = data.LinkedInUrl;

            await _context.SaveChangesAsync();

        }

        public async Task UpdateUserExperienceAsync(UpdateUserExperienceDTO data, string workId)
        {
            if (data.StartDate >= data.EndDate && data.EndDate != default)
                throw new Exception();

            Work work = await _context.Works.FirstOrDefaultAsync(x => x.Id == workId);

            work.Company = data.Company;
            work.JobName = data.JobName;
            work.About = data.About;
            work.PhoneNumber = data.PhoneNumber;
            work.Country = data.Country.ToString();
            work.City = data.City;
            work.WebUrl = data.WebUrl;
            work.StatDate = data.StartDate;
            work.EndDate = data.EndDate;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAwardAsync(UpdateUserAwardDTO data, string awardId, string environment)
        {
            Award award = await _context.Awards.FirstOrDefaultAsync(u => u.Id == awardId);

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

                string fileName = await data.Image.FileManagedAsync(Path.Combine(environment, "assets", "imgs", "Award"));

                award.ImageUrl = Path.Combine("assets", "imgs", "Award", fileName);

            }

            award.Company = data.Company;
            award.AwardName = data.AwardName;
            award.Date = data.Date;

            await _context.SaveChangesAsync();

        }

        public async Task UpdateUserEducationAsync(UpdateUserEducationDTO data, string educationId)
        {
            if (data.StartDate >= data.EndDate && data.EndDate != default)
                throw new Exception();

            Education education = await _context.Educations.FirstOrDefaultAsync(u => u.Id == educationId);

            education.Faculty = data.Faculty;
            education.Specialty = data.Specialty;
            education.SchoolName = data.SchoolName;
            education.About = data.About;
            education.Country = data.Country.ToString();
            education.City = data.City;
            education.StartDate = data.StartDate;
            education.EndDate = data.EndDate;
            education.Degree = data.Degree.ToString();

            await _context.SaveChangesAsync();


        }


        //Delete
        public async Task DeleteUserExperienceAsync(string workId)
        {
            Work work = await _context.Works.FirstOrDefaultAsync(x => x.Id == workId);

            _context.Works.Remove(work);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAwardAsync(string awardId)
        {
            Award award = await _context.Awards.FirstOrDefaultAsync(u => u.Id == awardId);

            _context.Awards.Remove(award);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserLanguageAsync(string languageId)
        {
            UserLanguage language = await _context.UserLanguages.FirstOrDefaultAsync(u => u.Id == languageId);

            _context.UserLanguages.Remove(language);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAbilityAsync(string abilityId)
        {
            UserAbility ability = await _context.UserAbilities.FirstOrDefaultAsync(u => u.Id == abilityId);
            
            _context.UserAbilities.Remove(ability);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserEducationAsync(string educationId)
        {
            Education education = await _context.Educations.FirstOrDefaultAsync(u => u.Id == educationId);

            _context.Educations.Remove(education);

            await _context.SaveChangesAsync();
        }
    }
}
