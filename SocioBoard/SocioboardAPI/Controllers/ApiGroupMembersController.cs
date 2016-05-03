using Api.Socioboard.Model;
using Api.Socioboard.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiGroupMembersController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiGroupMembersController));
        private UserRepository userrepo = new UserRepository();
        private GroupMembersRepository groupMembersRepository = new GroupMembersRepository();
        private GroupsRepository groupRepo = new GroupsRepository();

        [HttpGet]
        public IHttpActionResult AddGroupMembers(string Emails, string GroupId, string UserId)
        {
            Domain.Socioboard.Domain.Groups group = groupRepo.getGroupDetailsbyId(Guid.Parse(UserId), Guid.Parse(GroupId));
            string ret = string.Empty;
            string SentMails = string.Empty;
            try
            {
                List<string> arr = new List<string>();
                string[] arr1 = new string[] { };
                string NotSentMails = string.Empty;
                string selectedgroupid = string.Empty;

                if (Emails.Contains(','))
                {
                    arr = Emails.Split(',').ToList();
                }
                else
                {
                    //arr[0] = email;
                    arr.Add(Emails);
                }

                foreach (var item in arr)
                {
                    if (item.Contains(':'))
                    {
                        arr1 = item.Split(':');
                    }

                    //string res = "";
                    Domain.Socioboard.Domain.User objuserinfo = userrepo.getUserInfoByEmail(arr1[0]);

                    if (objuserinfo != null)
                    {
                        string[] name = objuserinfo.UserName.Split(' ');
                        string fname = name[0];
                        string lname = string.Empty;
                        for (int i = 1; i < name.Length; i++)
                        {
                            lname += name[i];
                        }

                        // res = ApiobjTeam.AddTeam(objuserinfo.Id.ToString(), "0", fname, lname, arr1[0], "", selectedgroupid, objUser.EmailId, objUser.UserName);
                        Domain.Socioboard.Domain.Groupmembers groupMemeber = new Domain.Socioboard.Domain.Groupmembers();
                        groupMemeber.Id = Guid.NewGuid();
                        groupMemeber.Emailid = objuserinfo.EmailId;
                        groupMemeber.Groupid = GroupId;
                        groupMemeber.Status = Domain.Socioboard.Domain.GroupUserStatus.Pending;
                        groupMemeber.Userid = objuserinfo.Id.ToString();
                        groupMemeber.Membercode = Api.Socioboard.Helper.Utility.GenerateRandomUniqueString();
                        groupMembersRepository.AddGroupMemeber(groupMemeber);

                        string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/GroupInvitation.html");
                        string html = File.ReadAllText(mailpath);
                        html = html.Replace("[group_name]", group.GroupName);
                        html = html.Replace("[join link]", "Home/GroupMember?MemberId=" + groupMemeber.Id + "&code=" + groupMemeber.Membercode);
                        MailSender objMailSender = new MailSender();
                        ret = objMailSender.SendChangePasswordMail(objuserinfo.EmailId, html, "Group Invitation");
                    }
                    else
                    {
                        //res = ApiobjTeam.AddTeam(objUser.Id.ToString(), "0", arr1[1], arr1[2], arr1[0], "", selectedgroupid, objUser.EmailId, objUser.UserName);
                        Domain.Socioboard.Domain.Groupmembers groupMemeber = new Domain.Socioboard.Domain.Groupmembers();
                        groupMemeber.Id = Guid.NewGuid();
                        groupMemeber.Emailid = arr1[0];
                        groupMemeber.Groupid = GroupId;
                        groupMemeber.Status = Domain.Socioboard.Domain.GroupUserStatus.Pending;
                        groupMemeber.Membercode = Api.Socioboard.Helper.Utility.GenerateRandomUniqueString();
                        // groupMemeber.Userid = objuserinfo.Id.ToString();
                        groupMembersRepository.AddGroupMemeber(groupMemeber);


                        string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/GroupInvitation.html");
                        string html = File.ReadAllText(mailpath);
                        html = html.Replace("[group_name]", group.GroupName);
                        html = html.Replace("[join link]", "Home/GroupMember?MemberId=" + groupMemeber.Id + "&code=" + groupMemeber.Membercode);
                        MailSender objMailSender = new MailSender();
                        ret = objMailSender.SendChangePasswordMail(arr1[0], html, "Group Invitation");
                    }
                }
                SentMails = "{\"SentMails\":" + "\"" + SentMails + "\",\"NotSentMails\":" + "\"" + NotSentMails + "\"}";
                return Ok(SentMails);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest();
            }

        }

        [HttpGet]
        public IHttpActionResult GetPendingGroupMembers(string GroupId)
        {
            try
            {
                List<Domain.Socioboard.Domain.Groupmembers> grpMembers = groupMembersRepository.GetGroupMemebersByStatus(GroupId, 0);
                return Ok(grpMembers);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                return BadRequest();
            }

        }

        [HttpGet]
        public IHttpActionResult GetAcceptedGroupMembers(string GroupId)
        {
            try
            {
                List<Domain.Socioboard.Domain.Groupmembers> grpMembers = groupMembersRepository.GetGroupMemebersByStatus(GroupId, 1);
                return Ok(grpMembers);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                return BadRequest();
            }
        }


        [HttpGet]
        public IHttpActionResult VadifyGroupMemeber(string MemberId, string Code, string UserId)
        {
            Domain.Socioboard.Domain.Groupmembers grpMember = groupMembersRepository.GetGroupMember(Guid.Parse(MemberId));
            Domain.Socioboard.Domain.User user = userrepo.getUsersById(Guid.Parse(UserId));
            if (grpMember == null)
            {
                return Ok("Member Not Exist");
            }
            if (grpMember != null && user.EmailId == grpMember.Emailid)
            {
                if (grpMember.Membercode.Equals(Code))
                {
                    grpMember.Status = Domain.Socioboard.Domain.GroupUserStatus.Accepted;
                    grpMember.Userid = user.Id.ToString();
                    groupMembersRepository.updateBoard(grpMember);
                    return Ok("added");
                }
                else
                {
                    return Ok("Wrong Code");
                }

            }
            else
            {
                return Ok("Email Doesn't match");
            }


        }



    }
}
