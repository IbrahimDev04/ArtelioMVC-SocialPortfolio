﻿using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageGetWorkExperienceDTO
    {
        public GetUserInfoDTO GetUserInfoDTO {  get; set; }
        public List<GetUserWorkExperienceDTO> GetUserWorkExperienceDTOs {  get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

    }
}
