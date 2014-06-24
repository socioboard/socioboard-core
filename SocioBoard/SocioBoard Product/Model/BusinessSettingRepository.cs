using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Helper;
using System.Collections;

namespace SocioBoard.Model
{

    public class BusinessSettingRepository : IBusinessSettingRepository
    {
        /// <AddBusinessSetting>
        /// Check If User Id is exist or Not. If User Id is Exist then It will Update that same Users all Business Setting. Otherwise It add a new Business Setting Data.
        /// </summary>
        /// <param name="businessSetting">Set Values in a BusinessSetting Class Property and Pass the same Object of BusinessSetting Class.(Domain.BusinessSetting)</param>
        public void AddBusinessSetting(BusinessSetting businessSetting)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action. to check if user is Exist or not.
                    List<BusinessSetting> lstBusinessSetting = CheckUserId(businessSetting);

                    //if user is Exist the it update all BusinessSetting of the Same user, 
                    //Otherwise it add New BusinessSetting.
                    if (lstBusinessSetting.Count > 0)
                    {
                        UpdateBusinessSetting(businessSetting);
                    }
                    else
                    {
                        session.Save(businessSetting);
                        transaction.Commit();
                    }
                }//End Transaction
            }//End session
        }

        /// <CheckUserId>
        /// Get all Business Setting by UserId
        /// </summary>
        /// <param name="businessSetting">Set Values in a BusinessSetting Class Property and Pass the same Object of BusinessSetting Class.(Domain.BusinessSetting)</param>
        /// <returns>List of BusinessSetting Data Objects.</returns>
        public List<BusinessSetting> CheckUserId(BusinessSetting businessSetting)
        {
            List<BusinessSetting> lstBusinessSetting = new List<BusinessSetting>();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to get BusinessSetting By UserId
                    lstBusinessSetting = session.CreateQuery("From BusinessSetting where UserId = :userid")
                                          .SetParameter("userid", businessSetting.UserId)
                                          .List<BusinessSetting>()
                                          .ToList<BusinessSetting>();
                }//End Transaction
            }//End session

            return lstBusinessSetting;
        }


        /// <UpdateBusinessSetting>
        /// Update BusinessSetting by UserId.
        /// </summary>
        /// <param name="businessSetting">Set Values in a BusinessSetting Class Property and Pass the same Object of BusinessSetting Class.(Domain.BusinessSetting)</param>
        public void UpdateBusinessSetting(BusinessSetting businessSetting)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // Proceed action to Update Data.
                    // And Set the reuired paremeters to find the specific values.
                    session.CreateQuery("Update BusinessSetting set BusinessName =:businessName,AssigningTasks =:assigningTasks,TaskNotification =:taskNotification,FbPhotoUpload =:fbPhotoUpload,EntryDate =:entryDate where UserId =:userId")
                        .SetParameter("businessName", businessSetting.BusinessName)
                        .SetParameter("assigningTasks", businessSetting.AssigningTasks)
                        .SetParameter("taskNotification", businessSetting.TaskNotification)
                        .SetParameter("fbPhotoUpload", businessSetting.FbPhotoUpload)
                        .SetParameter("entryDate", DateTime.Now)
                        .SetParameter("userId", businessSetting.UserId)
                        .ExecuteUpdate();

                    transaction.Commit();
                }//End Transaction
            }//End session
        }



        /// <DeleteBusinessSettingByUserid>
        /// Delete BusinessSettingBy Userid(Guid)
        /// </summary>
        /// <param name="userid">Userid of BusinessSetting (Guid).</param>
        /// <returns>Return Integer 1 for True 0 for False. </returns>
        public int DeleteBusinessSettingByUserid(Guid userid)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action delete a data By UserId.
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
            }//End Transaction
        }//End session

    }
}