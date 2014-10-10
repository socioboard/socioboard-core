using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Socioboard.Domain
{
    public interface IBusinessSettingRepository
    {
        void AddBusinessSetting(BusinessSetting businessSetting);
        List<BusinessSetting> CheckUserId(Guid userId,Guid GroupId, string GroupName);
        void UpdateBusinessSetting(BusinessSetting businessSetting);
    }
}
