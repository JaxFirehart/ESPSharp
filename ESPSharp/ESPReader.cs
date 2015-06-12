﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using ESPSharp.Interfaces;

namespace ESPSharp
{
    public class ESPReader : BinaryReader
    {
        public ElderScrollsPlugin Master { get; set; }
        public ESPReader(Stream stream, ElderScrollsPlugin master)
            : base(stream, Utility.WesternEncoding) 
        {
            Master = master;
        }

        public string ReadTag()
        {
            return Utility.SanitizeTag(new string(ReadChars(4)));
        }

        public string PeekTag()
        {
            string peek = ReadTag();
            BaseStream.Seek(-4, SeekOrigin.Current);

            return peek;
        }

        public T ReadEnum<T>()
        {
            Type enumType = Enum.GetUnderlyingType(typeof(T));

            switch (enumType.FullName)
            {
                case "System.Byte":
                    return (T)(object)ReadByte();
                case "System.SByte":
                    return (T)(object)ReadSByte();
                case "System.UInt16":
                    return (T)(object)ReadUInt16();
                case "System.Int16":
                    return (T)(object)ReadInt16();
                case "System.UInt32":
                    return (T)(object)ReadUInt32();
                case "System.Int32":
                    return (T)(object)ReadInt32();
                case "System.UInt64":
                    return (T)(object)ReadUInt64();
                case "System.Int64":
                    return (T)(object)ReadInt64();
                default:
                    throw new NotImplementedException(enumType + " is not yet implemented.");
            }
        }

        public T Read<T>() where T : new()
        {
            Type tType = typeof(T);
            Type readType = tType;

            if (tType.IsEnum)
            {
                return ReadEnum<T>();
            }

            string typeName = readType.FullName;


            switch (typeName)
            {
                case "System.Byte":
                    return (T)(object)ReadByte();
                case "System.SByte":
                    return (T)(object)ReadSByte();
                case "System.Char":
                    return (T)(object)ReadChar();
                case "System.UInt16":
                    return (T)(object)ReadUInt16();
                case "System.Int16":
                    return (T)(object)ReadInt16();
                case "System.UInt32":
                    return (T)(object)ReadUInt32();
                case "System.Int32":
                    return (T)(object)ReadInt32();
                case "System.Single":
                    return (T)(object)ReadSingle();
                case "System.UInt64":
                    return (T)(object)ReadUInt64();
                case "System.Int64":
                    return (T)(object)ReadInt64();
            }

            if (typeof(T).GetInterfaces().Any(i => i == typeof(IESPSerializable)))
            {
                    IESPSerializable outT = new T() as IESPSerializable;
                    outT.ReadBinary(this);
                    return (T)(object)outT;
            }
            
            throw new NotImplementedException(typeName + " is not yet implemented.");
        }
    }
}
