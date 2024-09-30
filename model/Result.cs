using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dc.net.automation.webservice.gls.model
{
    public class Result
    {
        public string Status { get; set; }
        public List<string> Data { get; set; }
        public List<string> Errors { get; set; }
        public byte[] PDF { get; set; }
        public Result()
        {
            Errors = new List<string>();
            Data = new List<string>();
        }

    }
}
