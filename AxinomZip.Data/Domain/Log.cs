using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AxinomZip.Data.Domain
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Application { get; set; }
        public string Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
    }
}
