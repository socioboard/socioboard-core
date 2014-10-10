using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string TaskMessage { get; set; }
        public Guid UserId { get; set; }
        public Guid AssignTaskTo { get; set; }
        public bool TaskStatus { get; set; }
        public string AssignDate { get; set; }
        public DateTime CompletionDate { get; set; }
    }
    public class TaskByUser
    {
        public Guid Id { get; set; }
        public string TaskMessage { get; set; }
        public Guid UserId { get; set; }
        public Guid AssignTaskTo { get; set; }
        public bool TaskStatus { get; set; }
        public string AssignDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public Guid Uid { get; set; }
        public string UserName { get; set; }
        public string ProfileUrl { get; set; }
        public string EmailId {get;set;}
        public string AccountType { get; set; }
        public DateTime acccreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserStatus { get; set; }
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public string PaymentStatus { get; set; }
    }
}