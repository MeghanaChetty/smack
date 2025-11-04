using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.application.Common
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        
       public List<string> Errors { get; set; } = new List<string>();

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
