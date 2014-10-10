using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Domain.Socioboard.Domain
{
    public interface ISocialProfilesRepository
    {

        List<SocialProfile> getAllSocialProfilesOfUser(Guid userid);
        void addNewProfileForUser(SocialProfile socio);
        bool checkUserProfileExist(SocialProfile socio);
        int updateSocialProfile(SocialProfile socio);
    }
}