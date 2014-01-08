using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    public interface IBusinessSettingRepository
    {
        void AddBusinessSetting(BusinessSetting businessSetting);
        List<BusinessSetting> CheckUserId(BusinessSetting businessSetting);
        void UpdateBusinessSetting(BusinessSetting businessSetting);
    }
}
