﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESPSharp.Subrecords;

namespace ESPSharp
{
    public interface IEditorID
    {
        SimpleSubrecord<string> EditorID { get; set; }
    }
}
