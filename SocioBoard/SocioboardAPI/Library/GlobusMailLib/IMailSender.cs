using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobusMailLib
{
    public interface IMailSender
    {
        string SendMail(string from, string passsword, string to, string bcc, string cc, string subject, string body, string UserName="", string Password="");
        string SendMailWithAttachment(string from, string passsword, string to, string bcc, string cc, string subject, string body, string filepath, string filename, string filetype, string UserName = "", string Password = "");

    }
}
