﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPSharp.Enums.Flags
{
    [Flags]
    public enum EyesFlags : byte
    {
        Playable = 0x01,
        NotMale = 0x02,
        NotFemale = 0x04
    }
}
