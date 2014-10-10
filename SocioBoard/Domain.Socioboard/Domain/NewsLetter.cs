using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class NewsLetter
    {
        public Guid	Id { get; set; }
        public string Subject { get; set; }
	    public string NewsLetterBody { get; set; }
	    public Guid UserId{ get; set; }
	    public bool SendStatus{ get; set; }
        public DateTime SendDate { get; set; }
        public DateTime EntryDate { get; set; }
    }
}