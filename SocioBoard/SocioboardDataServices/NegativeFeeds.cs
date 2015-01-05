using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    public class NegativeFeeds
    {
        public string getAllNegativeFeeds()
        {
            //  string ret =string.Empty;
            try
            {
                Api.SentimentalAnalysis.SentimentalAnalysis Apiobjsentimentalanalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
                Api.Team.Team ApiobjTeam = new Api.Team.Team();
                Api.TicketAssigneeStatus.TicketAssigneeStatus ApiobjTicketAssigneeStatus = new Api.TicketAssigneeStatus.TicketAssigneeStatus();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeed = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.Team> lstGroupMember = new List<Domain.Socioboard.Domain.Team>();
                List<Domain.Socioboard.Domain.TicketAssigneeStatus> lstAllAssignedMembers = new List<Domain.Socioboard.Domain.TicketAssigneeStatus>();

                lstNegativeFeed = (List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>)(new JavaScriptSerializer().Deserialize(Apiobjsentimentalanalysis.getAllNegativeFeedsOfProfile(), typeof(List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>)));
                int FeedNumber = lstNegativeFeed.Count;
                if (FeedNumber > 0)
                {
                    lstGroupMember = (List<Domain.Socioboard.Domain.Team>)(new JavaScriptSerializer().Deserialize(ApiobjTeam.getAllGroupMembersofTeam(), typeof(List<Domain.Socioboard.Domain.Team>)));
                }
                foreach (var lstGroupMember_item in lstGroupMember)
                {
                    ApiobjTicketAssigneeStatus.AddTicketAssigneeStatus(lstGroupMember_item.UserId);
                }
                lstAllAssignedMembers = (List<Domain.Socioboard.Domain.TicketAssigneeStatus>)(new JavaScriptSerializer().Deserialize(ApiobjTicketAssigneeStatus.getAllAssignedMembers(), typeof(List<Domain.Socioboard.Domain.TicketAssigneeStatus>)));
                int Assigneemember = lstAllAssignedMembers.Count;
                int MemberNumber = lstGroupMember.Count;
               
                if (Assigneemember > 0)
                {
                   
                    Domain.Socioboard.Domain.Team Member = new Domain.Socioboard.Domain.Team();
                    Domain.Socioboard.Domain.TicketAssigneeStatus objTicketAssigneeStatus = new Domain.Socioboard.Domain.TicketAssigneeStatus();
                    int j = 0;
                    for (int i = 0; i < FeedNumber; i++)
                    {
                        Domain.Socioboard.Domain.FeedSentimentalAnalysis Feed = lstNegativeFeed[i];
                        if (j == Assigneemember)
                        {
                            j = 0;
                        }
                        objTicketAssigneeStatus = lstAllAssignedMembers[j];
                        j++;

                        Guid Id = Feed.Id;
                        Guid ToAssignUserId = objTicketAssigneeStatus.AssigneeUserId;
                        Apiobjsentimentalanalysis.updateAssignedStatus(Id.ToString(), ToAssignUserId.ToString());
                        Domain.Socioboard.Domain.TicketAssigneeStatus AssigneeDetails =(Domain.Socioboard.Domain.TicketAssigneeStatus)(new JavaScriptSerializer().Deserialize(ApiobjTicketAssigneeStatus.getAssignedMembers(ToAssignUserId.ToString()),typeof(Domain.Socioboard.Domain.TicketAssigneeStatus)));
                        int AssignedCount = AssigneeDetails.AssignedTicketCount;
                        int Count = ++AssignedCount;
                        ApiobjTicketAssigneeStatus.updateAssigneeCount(objTicketAssigneeStatus.AssigneeUserId.ToString(), Count);
                    }
                }

                //#region Old Code
                //if (MemberNumber > 0)
                //{
                //    Domain.Socioboard.Domain.Team Member = new Domain.Socioboard.Domain.Team();
                //    int j = 0;
                //    for (int i = 0; i < FeedNumber; i++)
                //    {
                //        Domain.Socioboard.Domain.FeedSentimentalAnalysis Feed = lstNegativeFeed[i];
                //        if (j == MemberNumber)
                //        {
                //            j = 0;
                //        }
                //        Member = lstGroupMember[j];
                //        j++;

                //        Guid Id = Feed.Id;
                //        Guid ToAssignUserId = Member.UserId;
                //        Apiobjsentimentalanalysis.updateAssignedStatus(Id.ToString(), ToAssignUserId.ToString());
                //    }
                //} 
                //#endregion

                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}
