using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManager.Site.Models
{
    public class ConfirmVM
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
    }
}