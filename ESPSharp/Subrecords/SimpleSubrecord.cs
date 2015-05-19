﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace ESPSharp.Subrecords
{
    public class SimpleSubrecord<T> : Subrecord
    {
        public T Value { get; set; }

        protected override void ReadData(ESPReader reader)
        {
            Type tType = typeof(T);
            Type readType = tType;

            if (tType.IsEnum)
                readType = Enum.GetUnderlyingType(tType);

            string typeName = readType.FullName;


            switch (typeName)
            {
                case "System.Byte":
                    Debug.Assert(size == 1);
                    Value = (T)(object)reader.ReadByte();
                    break;
                case "System.SByte":
                    Debug.Assert(size == 1);
                    Value = (T)(object)reader.ReadSByte();
                    break;
                case "System.Char":
                    Debug.Assert(size == 1);
                    Value = (T)(object)reader.ReadChar();
                    break;
                case "System.UInt16":
                    Debug.Assert(size == 2);
                    Value = (T)(object)reader.ReadUInt16();
                    break;
                case "System.Int16":
                    Debug.Assert(size == 2);
                    Value = (T)(object)reader.ReadInt16();
                    break;
                case "System.UInt32":
                    Debug.Assert(size == 4);
                    Value = (T)(object)reader.ReadUInt32();
                    break;
                case "System.Int32":
                    Debug.Assert(size == 4);
                    Value = (T)(object)reader.ReadInt32();
                    break;
                case "System.Single":
                    Debug.Assert(size == 4);
                    Value = (T)(object)reader.ReadSingle();
                    break;
                case "System.UInt64":
                    Debug.Assert(size == 8);
                    Value = (T)(object)reader.ReadUInt64();
                    break;
                case "System.Int64":
                    Debug.Assert(size == 8);
                    Value = (T)(object)reader.ReadInt64();
                    break;
                case "System.Byte[]":
                    Value = (T)(object)reader.ReadBytes(size);
                    break;
                case "System.Char[]":
                    Value = (T)(object)reader.ReadChars(size);
                    break;
                case "System.String":
                    string outString = new String(reader.ReadChars(size - 1));
                    reader.ReadByte();
                    Value = (T)(object)outString;
                    break;
                default:
                    throw new NotImplementedException(typeName + " is not yet implemented.");
            }
        }

        protected override void WriteData(ESPWriter writer)
        {
            Type tType = typeof(T);
            Type readType = tType;

            if (tType.IsEnum)
                readType = Enum.GetUnderlyingType(tType);

            string typeName = readType.FullName;


            switch (typeName)
            {
                case "System.Byte":
                    writer.Write((byte)(object)Value);
                    break;
                case "System.SByte":
                    writer.Write((sbyte)(object)Value);
                    break;
                case "System.Char":
                    writer.Write((char)(object)Value);
                    break;
                case "System.UInt16":
                    writer.Write((ushort)(object)Value);
                    break;
                case "System.Int16":
                    writer.Write((short)(object)Value);
                    break;
                case "System.UInt32":
                    writer.Write((uint)(object)Value);
                    break;
                case "System.Int32":
                    writer.Write((int)(object)Value);
                    break;
                case "System.Single":
                    writer.Write((float)(object)Value);
                    break;
                case "System.UInt64":
                    writer.Write((ulong)(object)Value);
                    break;
                case "System.Int64":
                    writer.Write((long)(object)Value);
                    break;
                case "System.Byte[]":
                    writer.Write((byte[])(object)Value);
                    break;
                case "System.Char[]":
                    writer.Write((char[])(object)Value);
                    break;
                case "System.String":
                    writer.Write(((string)(object)Value).ToCharArray());
                    writer.Write((byte)0);
                    break;
            }
        }

        protected override void WriteDataXML(XElement ele)
        {
            ele.Value = Value.ToString();
        }

        protected override void ReadDataXML(XElement ele)
        {
            Type tType = typeof(T);
            Type readType = tType;

            if (tType.IsEnum)
            {
                Value = ele.ToEnum<T>();
                return;
            }

            string typeName = readType.FullName;


            switch (typeName)
            {
                case "System.Byte":
                    Value = (T)(object)ele.ToByte();
                    break;
                case "System.SByte":
                    Value = (T)(object)ele.ToSByte();
                    break;
                case "System.Char":
                    Value = (T)(object)ele.ToChar();
                    break;
                case "System.UInt16":
                    Value = (T)(object)ele.ToUInt16();
                    break;
                case "System.Int16":
                    Value = (T)(object)ele.ToInt16();
                    break;
                case "System.UInt32":
                    Value = (T)(object)ele.ToUInt32();
                    break;
                case "System.Int32":
                    Value = (T)(object)ele.ToInt32();
                    break;
                case "System.Single":
                    Value = (T)(object)ele.ToSingle();
                    break;
                case "System.UInt64":
                    Value = (T)(object)ele.ToUInt64();
                    break;
                case "System.Int64":
                    Value = (T)(object)ele.ToInt64();
                    break;
                case "System.Byte[]":
                    Value = (T)(object)ele.ToBytes();
                    break;
                case "System.Char[]":
                    Value = (T)(object)ele.ToChars();
                    break;
                case "System.String":
                    string outString = ele.Value;
                    Value = (T)(object)outString;
                    break;
                default:
                    throw new NotImplementedException(typeName + " is not yet implemented.");
            }
        }
    }
}
