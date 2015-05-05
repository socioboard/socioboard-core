using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobusMailLib
{
    public class MailSenderFactory
    {
        private IMailSender objMailSender = null;

        private MailSendingType _mailSendingType { get; set; }

        public MailSenderFactory(MailSendingType mailSendingType)
        {
            this._mailSendingType = mailSendingType;
        }

        public IMailSender GetMailSenderInstance()
        {
            switch (_mailSendingType)
            {
                case MailSendingType.Mandrill:
                    objMailSender = new MailSenderMandrill();
                    break;
                case MailSendingType.Sendgrid:
                    objMailSender = new MailSenderSendgrid();
                    break;
                case MailSendingType.Gmail:
                    objMailSender = new MailSenderGmail();
                    break;
                default:
                    break;
            }

            return objMailSender;
        }
    }
}
