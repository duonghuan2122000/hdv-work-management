using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Dto
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; }
    }
}
