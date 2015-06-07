﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using ESPSharp.Enums;
using ESPSharp.Enums.Flags;
using ESPSharp.Subrecords;
using ESPSharp.SubrecordCollections;

namespace ESPSharp.Records
{
	public partial class Hair : Record, IEditorID	{
		public SimpleSubrecord<String> EditorID { get; set; }
		public SimpleSubrecord<String> Name { get; set; }
		public Model Model { get; set; }
		public SimpleSubrecord<String> Texture { get; set; }
		public SimpleSubrecord<HairFlags> HairFlags { get; set; }
	
		public override void ReadData(ESPReader reader, long dataEnd)
		{
			while (reader.BaseStream.Position < dataEnd)
			{
				string subTag = reader.PeekTag();

				switch (subTag)
				{
					case "EDID":
						if (EditorID == null)
							EditorID = new SimpleSubrecord<String>();

						EditorID.ReadBinary(reader);
						break;
					case "FULL":
						if (Name == null)
							Name = new SimpleSubrecord<String>();

						Name.ReadBinary(reader);
						break;
					case "MODL":
						if (Model == null)
							Model = new Model();

						Model.ReadBinary(reader);
						break;
					case "ICON":
						if (Texture == null)
							Texture = new SimpleSubrecord<String>();

						Texture.ReadBinary(reader);
						break;
					case "DATA":
						if (HairFlags == null)
							HairFlags = new SimpleSubrecord<HairFlags>();

						HairFlags.ReadBinary(reader);
						break;
				}
			}
		}

		public override void WriteData(ESPWriter writer)
		{
			if (EditorID != null)
				EditorID.WriteBinary(writer);
			if (Name != null)
				Name.WriteBinary(writer);
			if (Model != null)
				Model.WriteBinary(writer);
			if (Texture != null)
				Texture.WriteBinary(writer);
			if (HairFlags != null)
				HairFlags.WriteBinary(writer);
		}

		public override void WriteDataXML(XElement ele)
		{
			XElement subEle;
			if (EditorID != null)		
			{		
				ele.TryPathTo("EditorID", true, out subEle);
				EditorID.WriteXML(subEle);
			}
			if (Name != null)		
			{		
				ele.TryPathTo("Name", true, out subEle);
				Name.WriteXML(subEle);
			}
			if (Model != null)		
			{		
				ele.TryPathTo("Model", true, out subEle);
				Model.WriteXML(subEle);
			}
			if (Texture != null)		
			{		
				ele.TryPathTo("Texture", true, out subEle);
				Texture.WriteXML(subEle);
			}
			if (HairFlags != null)		
			{		
				ele.TryPathTo("Flags", true, out subEle);
				HairFlags.WriteXML(subEle);
			}
		}

		public override void ReadDataXML(XElement ele)
		{
			XElement subEle;

			if (ele.TryPathTo("EditorID", false, out subEle))
			{
				if (EditorID == null)
					EditorID = new SimpleSubrecord<String>();
					
				EditorID.ReadXML(subEle);
			}
			if (ele.TryPathTo("Name", false, out subEle))
			{
				if (Name == null)
					Name = new SimpleSubrecord<String>();
					
				Name.ReadXML(subEle);
			}
			if (ele.TryPathTo("Model", false, out subEle))
			{
				if (Model == null)
					Model = new Model();
					
				Model.ReadXML(subEle);
			}
			if (ele.TryPathTo("Texture", false, out subEle))
			{
				if (Texture == null)
					Texture = new SimpleSubrecord<String>();
					
				Texture.ReadXML(subEle);
			}
			if (ele.TryPathTo("Flags", false, out subEle))
			{
				if (HairFlags == null)
					HairFlags = new SimpleSubrecord<HairFlags>();
					
				HairFlags.ReadXML(subEle);
			}
		}

	}
}