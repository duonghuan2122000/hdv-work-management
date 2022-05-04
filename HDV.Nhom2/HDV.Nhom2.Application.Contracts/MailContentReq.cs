using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public class MailContentReq
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
