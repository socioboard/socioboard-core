using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class RssReader
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PublishedDate { get; set; }
        public bool Status { get; set; }
        public string Link { get; set; }
        public string FeedsUrl { get; set; }

    }
}