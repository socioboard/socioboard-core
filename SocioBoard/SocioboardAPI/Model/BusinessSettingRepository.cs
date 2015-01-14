using System;
using System.Collections.Generic;
using System.Linq;
using Api.Socioboard.Helper;
using Domain.Socioboard.Domain;

namespace Api.Socioboard.Services
{

    public class BusinessSettingRepository : IBusinessSettingRepository
    {
        /// <AddBusinessSetting>
        /// Check If User Id is exist or Not. If User Id is Exist then It will Update that same Users all Business Setting. Otherwise It add a new Business Setting Data.
        /// </summary>
        /// <param name="businessSetting">Set Values in a BusinessSetting Class Property and Pass the same Object of BusinessSetting Class.(Domain.BusinessSetting)</param>
        public void AddBusinessSetting(Domain.Socioboard.Domain.BusinessSetting businessSetting)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action. to check if user is Exist or not.
                    List<Domain.Socioboard.Domain.BusinessSetting> lstBusinessSetting = CheckUserId(businessSetting.UserId, businessSetting.GroupId, businessSetting.BusinessName);

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
        public List<Domain.Socioboard.Domain.BusinessSetting> CheckUserId(Guid UserId, Guid GroupId,string GroupName)
        {
            List<Domain.Socioboard.Domain.BusinessSetting> lstBusinessSetting = new List<Domain.Socioboard.Domain.BusinessSetting>();
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    //Proceed action, to get BusinessSetting By UserId
                    lstBusinessSetting = session.CreateQuery("From BusinessSetting where UserId = :userid and groupId=:groupId and BusinessName=:GroupName")
                                          .SetParameter("userid",UserId)
                                          .SetParameter("groupId",GroupId)
                                          .SetParameter("GroupName",GroupName)
                                          .List<Domain.Socioboard.Domain.BusinessSetting>()
                                          .ToList<Domain.Socioboard.Domain.BusinessSetting>();
                }//End Transaction
            }//End session

            return lstBusinessSetting;
        }


        /// <UpdateBusinessSetting>
        /// Update BusinessSetting by UserId.
        /// </summary>
        /// <param name="businessSetting">Set Values in a BusinessSetting Class Property and Pass the same Object of BusinessSetting Class.(Domain.BusinessSetting)</param>
        public void UpdateBusinessSetting(Domain.Socioboard.Domain.BusinessSetting businessSetting)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {

                    // Proceed action to Update Data.
                    // And Set the reuired paremeters to find the specific values.
                    session.CreateQuery("Update BusinessSetting set BusinessName =:businessName,AssigningTasks =:assigningTasks,TaskNotification =:taskNotification,FbPhotoUpload =:fbPhotoUpload,EntryDate =:entryDate where UserId =:userId and groupId=:groupId and BusinessName=:GroupName")
                        .SetParameter("businessName", businessSetting.BusinessName)
                        .SetParameter("assigningTasks", businessSetting.AssigningTasks)
                        .SetParameter("taskNotification", businessSetting.TaskNotification)
                        .SetParameter("fbPhotoUpload", businessSetting.FbPhotoUpload)
                        .SetParameter("entryDate", DateTime.Now)
                        .SetParameter("userId", businessSetting.UserId)
                        .SetParameter("groupId", businessSetting.GroupId)
                        .SetParameter("GroupName", businessSetting.BusinessName)
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
        public int DeleteBusinessSettingByUserid(Guid GroupId)
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
                        NHibernate.IQuery query = session.CreateQuery("delete from BusinessSetting where GroupId = :id")
                                         .SetParameter("id", GroupId);
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

        public BusinessSetting IsAssignTaskEnable(Guid groupId)
        {
            // bool flag = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    

                        List<BusinessSetting> lstdetails = session.CreateQuery("from BusinessSetting where groupId=:groupId")
                            .SetParameter("groupId", groupId).List<BusinessSetting>().ToList<BusinessSetting>();

                        BusinessSetting objBsnsStng = lstdetails[0];
                        //flag = objBsnsStng.AssigningTasks;
                        //if (objBsnsStng.AssigningTasks == true)
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        return objBsnsStng;
                   

                }
            }
        }

        public Domain.Socioboard.Domain.BusinessSetting GetDetailsofBusinessOwner(Guid groupId)
        {
            // bool flag = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {


                    List<Domain.Socioboard.Domain.BusinessSetting> lstdetails = session.CreateQuery("from BusinessSetting where groupId=:groupId")
                        .SetParameter("groupId", groupId).List<Domain.Socioboard.Domain.BusinessSetting>().ToList<Domain.Socioboard.Domain.BusinessSetting>();

                    Domain.Socioboard.Domain.BusinessSetting objBsnsStng = new Domain.Socioboard.Domain.BusinessSetting();
                    if (lstdetails.Count > 0)
                    {
                        objBsnsStng = lstdetails[0];
                    }
                    else {
                        objBsnsStng = null;
                    }
                    return objBsnsStng;


                }
            }
        }

        public Domain.Socioboard.Domain.BusinessSetting IsNotificationTaskEnable(Guid groupId)
        {
            // bool flag = 0;
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {


                    List<Domain.Socioboard.Domain.BusinessSetting> lstdetails = session.CreateQuery("from BusinessSetting where GroupId=:groupId")
                        .SetParameter("groupId", groupId).List<Domain.Socioboard.Domain.BusinessSetting>().ToList<Domain.Socioboard.Domain.BusinessSetting>();

                    Domain.Socioboard.Domain.BusinessSetting objBsnsStng = lstdetails[0];

                    return objBsnsStng;


                }
            }
        }


        public bool checkBusinessExists(Guid userid, string groupname)
        {
            //Creates a database connection and opens up a session
            using (NHibernate.ISession session = SessionFactory.GetNewSession())
            {
                //After Session creation, start Transaction.
                using (NHibernate.ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Proceed action, to find group by and and group name
                        NHibernate.IQuery query = session.CreateQuery("from BusinessSetting where UserId = :userid and BusinessName =:groupname");
                        //  query.SetParameter("userid", group.UserId);  UserId =:userid and
                        query.SetParameter("userid", userid);
                        query.SetParameter("groupname", groupname);
                        var result = query.UniqueResult();
                        if (result == null)
                            return false;
                        else
                            return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        return true;
                    }
                }//End Transaction
            }//End Session
        }

     


        public List<BusinessSetting> GetBusinessSettingByUserId(Guid UserId)
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
                                          .SetParameter("userid", UserId)
                                          .List<BusinessSetting>()
                                          .ToList<BusinessSetting>();
                }//End Transaction
            }//End session

            return lstBusinessSetting;
        }

    }
}