﻿using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Interfaces
{
    interface ITokenService
    {
        Token CheckToken(string tokenId);
    }
}
