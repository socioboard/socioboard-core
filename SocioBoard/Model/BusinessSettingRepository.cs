using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{
    public class BusinessSettingRepository:IBusinessSettingRepository
    {
        public void AddBusinessSetting(BusinessSetting businessSetting)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    List<BusinessSetting> lstBusinessSetting = CheckUserId(businessSetting);

                    if (lstBusinessSetting.Count > 0)
                    {
                        UpdateBusinessSetting(businessSetting);
                    }
                    else
                    {
                        session.Save(businessSetting);
                        transaction.Commit();
                    }
                }
            }
        }

        public List<BusinessSetting> CheckUserId(BusinessSetting businessSetting)
        {
            List<BusinessSetting> lstBusinessSetting = new List<BusinessSetting>();
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    lstBusinessSetting = session.CreateQuery("From BusinessSetting where UserId = :userid")
                                          .SetParameter("userid", businessSetting.UserId)
                                          .List<BusinessSetting>()
                                          .ToList<BusinessSetting>();
                }
            }

            return lstBusinessSetting;
        }

        public void UpdateBusinessSetting(BusinessSetting businessSetting)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    session.CreateQuery("Update BusinessSetting set BusinessName =:businessName,AssigningTasks =:assigningTasks,TaskNotification =:taskNotification,FbPhotoUpload =:fbPhotoUpload,EntryDate =:entryDate where UserId =:userId")
                        .SetParameter("businessName", businessSetting.BusinessName)
                        .SetParameter("assigningTasks", businessSetting.AssigningTasks)
                        .SetParameter("taskNotification", businessSetting.TaskNotification)
                        .SetParameter("fbPhotoUpload", businessSetting.FbPhotoUpload)
                        .SetParameter("entryDate",DateTime.Now)
                        .SetParameter("userId", businessSetting.UserId)
                        .ExecuteUpdate();

                    transaction.Commit();
                }
            }
        }


        public int DeleteBusinessSettingByUserid(Guid userid)
        {
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        NHibernate.IQuery query = session.CreateQuery("delete from BusinessSetting where UserId = :userid")
                                        .SetParameter("userid", userid);
                        int isUpdated = query.ExecuteUpdate();
                        transaction.Commit();
                        return isUpdated;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return 0;
                    }
                }
            }
        }

    }
}