﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public class AuthEmployeeRes
    {
        public string Token { get; set; }

        public int ExpireTime { get; set; }
    }
}
