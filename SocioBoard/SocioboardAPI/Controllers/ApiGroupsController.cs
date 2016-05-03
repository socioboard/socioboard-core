using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiGroupsController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiShareathonController));
        private GroupsRepository grouprepo = new GroupsRepository();
        private UserRepository objUserRepository = new UserRepository();
        private GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        private GroupMembersRepository grpMemberRepo = new GroupMembersRepository();

       // [Authorize]
        [HttpPost]
        public IHttpActionResult AddGroup(Domain.Socioboard.Domain.Groups group) 
        {
            try
            {
                // GroupRepository grouprepo = new GroupRepository();
                if (!grouprepo.checkGroupExists(group.UserId, group.GroupName))
                {
                    // Domain.Socioboard.Domain.Groups group = new Domain.Socioboard.Domain.Groups();
                    group.EntryDate = DateTime.UtcNow;
                    group.Id = Guid.NewGuid();
                    grouprepo.AddGroup(group);
                    //code to add admin as group member
                    Domain.Socioboard.Domain.User objUser = objUserRepository.getUsersById(group.UserId);

                    Domain.Socioboard.Domain.Groupmembers grpMember = new Domain.Socioboard.Domain.Groupmembers();
                    grpMember.Id = Guid.NewGuid();
                    grpMember.Groupid = group.Id.ToString();
                    grpMember.Userid = group.UserId.ToString();
                    grpMember.Status = Domain.Socioboard.Domain.GroupUserStatus.Accepted ;
                    grpMember.Emailid = objUser.EmailId;
                    grpMember.IsAdmin = true;
                    grpMemberRepo.AddGroupMemeber(grpMember);


                    Api.Socioboard.Services.BusinessSetting ApiobjBusinesssSetting = new Api.Socioboard.Services.BusinessSetting();
                    Domain.Socioboard.Domain.BusinessSetting ObjBsnsStng = new Domain.Socioboard.Domain.BusinessSetting();
                    ObjBsnsStng.Id = Guid.NewGuid();
                    ObjBsnsStng.BusinessName = group.GroupName;
                    ObjBsnsStng.GroupId = group.Id;
                    ObjBsnsStng.AssigningTasks = false;
                    ObjBsnsStng.TaskNotification = false;
                    ObjBsnsStng.FbPhotoUpload = 0;
                    ObjBsnsStng.UserId = group.UserId;
                    ObjBsnsStng.EntryDate = DateTime.Now;
                    string ObjBsnsStg = (new JavaScriptSerializer().Serialize(ObjBsnsStng));
                    string BsnsMessage = ApiobjBusinesssSetting.AddBusinessByUser(ObjBsnsStg);
                    //return new JavaScriptSerializer().Serialize(group);
                    return Ok("Added Sucessfully");
                }
                else
                {
                    return BadRequest("Group Exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest("Group Exist.");
            }
        }

        [HttpGet]
        public IHttpActionResult GetGroupDetailsByGroupId(string GroupId)
        {
            try
            {
                Domain.Socioboard.Domain.Groups objGroups = grouprepo.getGroupName(Guid.Parse(GroupId));
                return Ok(objGroups);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }

        [HttpGet]
        public IHttpActionResult GetGroupDetailsByUserId(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.Groups> lstGroups = grouprepo.getAllGroups(Guid.Parse(UserId));
                return Ok(lstGroups);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }


       [HttpGet]
        public IHttpActionResult DeleteGroupByUserId(string UserId)
        {
            try
            {
                int i = grouprepo.DeleteGroup(Guid.Parse(UserId));
                if (i == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid UserId");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }

        [HttpPost]
       public IHttpActionResult DeleteGroupByName(Groups group)
       {
           try
           {
               int i = grouprepo.DeleteGroup(group);
               if (i == 1)
               {
                   return Ok("Group Deleted Successfully");
               }
               else
               {
                   return BadRequest("Invalid UserId");
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.StackTrace);
               return BadRequest("Something Went Wrong");
           }
       }


      [HttpPost]
       public IHttpActionResult DeleteGroupById(string GroupId, string Userid)
       {
           try
           {

               objGroupProfileRepository.DeleteAllGroupProfile(Guid.Parse(GroupId));

               grouprepo.DeleteGroup(Guid.Parse(GroupId));
               return Ok();
              
           }
           catch (Exception ex)
           {
               logger.Error(ex.Message);
               logger.Error(ex.StackTrace);
               return BadRequest("Something Went Wrong");
           }
       }




      [HttpGet]
       public IHttpActionResult GetGroupDeUserId(string UserId)
       {
           try
           {
               List<Domain.Socioboard.Domain.Groups> lstGroups = grouprepo.getAllGroups(Guid.Parse(UserId));
               return Ok(lstGroups[0]);
           }
           catch (Exception ex)
           {
               logger.Error(ex.Message);
               logger.Error(ex.StackTrace);
               return BadRequest("Something Went Wrong");
           }
       }



      [HttpGet]
      public IHttpActionResult GetGroupsOfUser(string UserId)
      {
          List<Domain.Socioboard.Domain.Groupmembers> lstGroupMember = new List<Domain.Socioboard.Domain.Groupmembers>();
          List<Domain.Socioboard.Domain.Groups> lstGroups = new List<Domain.Socioboard.Domain.Groups>();
          lstGroupMember = grpMemberRepo.GetUserGroupmembers(UserId);
          foreach (var member in lstGroupMember)
          {
              Domain.Socioboard.Domain.Groups group = grouprepo.getGroupById(Guid.Parse(member.Groupid));
              if (group != null)
              {
                  lstGroups.Add(group);
              }
          }
          return Ok(lstGroups);
      }
    }
}
