﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPSharp
{
    public class Header : Record
    {
        public override void ReadData(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override byte[] WriteData()
        {
            throw new NotImplementedException();
        }

        public override void ReadDataXML(System.Xml.Linq.XElement ele)
        {
            throw new NotImplementedException();
        }

        public override void WriteDataXML(System.Xml.Linq.XElement ele)
        {
            throw new NotImplementedException();
        }
    }
}
