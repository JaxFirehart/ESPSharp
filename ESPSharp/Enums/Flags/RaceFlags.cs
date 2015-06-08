﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPSharp.Enums.Flags
{
    [Flags]
    public enum RaceFlags : uint
    {
        Playable = 0x01,
        Child = 0x04
    }
}
