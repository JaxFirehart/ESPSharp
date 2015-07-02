﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using ESPSharp.Enums;
using ESPSharp.Enums.Flags;
using ESPSharp.Interfaces;
using ESPSharp.Subrecords;
using ESPSharp.SubrecordCollections;
using ESPSharp.DataTypes;

namespace ESPSharp.Subrecords
{
	public partial class ValueWeight : Subrecord, ICloneable<ValueWeight>
	{
		public Int32 Value { get; set; }
		public Single Weight { get; set; }

		public ValueWeight()
		{
			Value = new Int32();
			Weight = new Single();
		}

		public ValueWeight(Int32 Value, Single Weight)
		{
			this.Value = Value;
			this.Weight = Weight;
		}

		public ValueWeight(ValueWeight copyObject)
		{
			Value = copyObject.Value;
			Weight = copyObject.Weight;
		}
	
		protected override void ReadData(ESPReader reader)
		{
			using (MemoryStream stream = new MemoryStream(reader.ReadBytes(size)))
			using (ESPReader subReader = new ESPReader(stream))
			{
				try
				{
					Value = subReader.ReadInt32();
					Weight = subReader.ReadSingle();
				}
				catch
				{
					return;
				}
			}
		}

		protected override void WriteData(ESPWriter writer)
		{
			writer.Write(Value);			
			writer.Write(Weight);			
		}

		protected override void WriteDataXML(XElement ele, ElderScrollsPlugin master)
		{
			XElement subEle;

			ele.TryPathTo("Value", true, out subEle);
			subEle.Value = Value.ToString();

			ele.TryPathTo("Weight", true, out subEle);
			subEle.Value = Weight.ToString();
		}

		protected override void ReadDataXML(XElement ele, ElderScrollsPlugin master)
		{
			XElement subEle;

			if (ele.TryPathTo("Value", false, out subEle))
			{
				Value = subEle.ToInt32();
			}

			if (ele.TryPathTo("Weight", false, out subEle))
			{
				Weight = subEle.ToSingle();
			}
		}

		public ValueWeight Clone()
		{
			return new ValueWeight(this);
		}

	}
}