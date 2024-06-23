using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordKeepingProject.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
    }
}