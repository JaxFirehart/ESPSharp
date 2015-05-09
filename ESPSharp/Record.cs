﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace ESPSharp
{
    public class Record : IESPSerializable
    {
        public char[] Tag { get; protected set; }
        public uint Size { get; protected set; }
        public RecordFlag Flags { get; set; }
        public FormID FormID { get; set; }
        public uint VersionControlInfo1 { get; set; }
        public ushort FormVersion { get; protected set; }
        public ushort VersionControlInfo2 { get; set; }
        public byte[] Bytes { get; protected set; }

        protected bool corrupted = false;

        public void WriteXML(string destinationfolder)
        {
            XDocument doc = new XDocument();

            XElement root = new XElement("Record", 
                                new XAttribute("Tag", Tag));

            doc.Add(root);

            root.Add(
                new XElement("Flags", Flags),
                new XElement("FormID", FormID),
                new XElement("FormVersion", FormVersion),
                new XElement("VersionControlInfo",
                    new XElement("Info1", VersionControlInfo1),
                    new XElement("Info2", VersionControlInfo2)),
                new XElement("Data", Bytes.ToBase64())
                );
        }

        public void ReadXML(string sourceFile)
        {
            XDocument doc = XDocument.Load(sourceFile);
            XElement root = (XElement)doc.FirstNode;

            Tag = root.Attribute("Tag").Value.ToCharArray();
            Flags = root.Element("Flags").ToEnum<RecordFlag>();
            FormID = root.Element("FormID").ToUInt32();
            FormVersion = root.Element("FormVersion").ToUInt16();
            VersionControlInfo1 = root.Element("VersionControlInfo").Element("Info1").ToUInt32();
            VersionControlInfo2 = root.Element("VersionControlInfo").Element("Info2").ToUInt16();
            Bytes = root.Element("Data").ToBytes();
        }

        public void WriteBinary(BinaryWriter writer)
        {
            writer.Write(Tag);

            byte[] outBytes;
            if (Flags.HasFlag(RecordFlag.Compressed) && !corrupted)
            {
                outBytes = (Zlib.Compress(Bytes));
                writer.Write((uint)outBytes.Length + 4);
            }
            else
            {
                outBytes = Bytes;
                writer.Write((uint)outBytes.Length);
            }

            writer.Write((uint)Flags);
            writer.Write(FormID);
            writer.Write(VersionControlInfo1);
            writer.Write(FormVersion);
            writer.Write(VersionControlInfo2);

            if (Flags.HasFlag(RecordFlag.Compressed))
            {
                writer.Write(Bytes.Length);
            }

            writer.Write(outBytes);
        }

        public virtual void ReadBinary(BinaryReader reader)
        {
            Tag = reader.ReadTag();
            Size = reader.ReadUInt32();
            Flags = (RecordFlag)reader.ReadUInt32();
            FormID = reader.ReadUInt32();
            VersionControlInfo1 = reader.ReadUInt32();
            FormVersion = reader.ReadUInt16();
            VersionControlInfo2 = reader.ReadUInt16();

            if (Flags.HasFlag(RecordFlag.Compressed))
            {
                uint origSize = reader.ReadUInt32();
                byte[] compressedBytes = reader.ReadBytes((int)Size - 4);
                try
                {
                    using(MemoryStream stream = new MemoryStream(compressedBytes))
                        Bytes = Zlib.Decompress(stream, origSize - 4);
                }
                catch
                {
                    corrupted = true;
                    List<byte> temp = BitConverter.GetBytes(origSize).ToList();
                    temp.AddRange(compressedBytes);
                    Bytes = temp.ToArray();
                }
            }
            else
            {
                Bytes = reader.ReadBytes((int)Size);
            }
        }
    }
}
