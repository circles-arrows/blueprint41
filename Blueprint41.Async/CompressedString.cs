using System;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Runtime;
using System.Text;
using System.Xml;

namespace System
{
    public sealed class CompressedString : IComparable, ICloneable, IConvertible, IEquatable<string>
    {
        private Lazy<string> m_Value;
        public string Value { get { return m_Value.Value; } }
        public int Length => Value.Length;

        public CompressedString(string? x)
        {
            m_Value = new Lazy<string>(
                delegate()
                {
                    return x ?? "";
                }, true);
        }
        public CompressedString(byte[] bytes)
        {
            m_Value = new Lazy<string>(
                delegate () 
                {
                    return Decompress(bytes);
                }, true);
        }

        public CompressedString(CompressedString compressed)
        {
            m_Value = compressed.m_Value;
        }

        #region To CompressedString

        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<bool> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(bool x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<byte> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(byte x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<sbyte> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(sbyte x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<short> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(short x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<ushort> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(ushort x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<int> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(int x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<uint> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(uint x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<long> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(long x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<ulong> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(ulong x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<decimal> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(decimal x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<float> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(float x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<double> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(double x)
        {
            return new CompressedString(x.ToString());
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(byte[]? x)
        {
            if (x is null) return null;
            return new CompressedString(x);
        }
        [return: NotNullIfNotNull("x")]
        public static implicit operator CompressedString?(string? x)
        {
            if (x is null) return null;
            return new CompressedString(x);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<DateTime> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(string.Format("{0:yyyy-MM-dd HH:mm:ss}", x));
        }
        public static explicit operator CompressedString(DateTime x)
        {
            return new CompressedString(x.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator CompressedString?(Nullable<Guid> x)
        {
            if (x.HasValue == false) return null;
            return new CompressedString(x.ToString());
        }
        public static explicit operator CompressedString(Guid x)
        {
            return new CompressedString(x.ToString());
        }

        #endregion

        #region from CompressedString

        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<byte>(CompressedString? x)
        {
            if (x is null) return null;
            return byte.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<sbyte>(CompressedString? x)
        {
            if (x is null) return null;
            return sbyte.Parse(x.Value);
        }
        public static explicit operator byte(CompressedString x)
        {
            return byte.Parse(x.Value);
        }
        public static explicit operator sbyte(CompressedString x)
        {
            return sbyte.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<short>(CompressedString? x)
        {
            if (x is null) return null;
            return short.Parse(x.Value);
        }
        public static explicit operator short(CompressedString x)
        {
            return short.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<ushort>(CompressedString? x)
        {
            if (x is null) return null;
            return ushort.Parse(x.Value);
        }
        public static explicit operator ushort(CompressedString x)
        {
            return ushort.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<int>(CompressedString? x)
        {
            if (x is null) return null;
            return int.Parse(x.Value);
        }
        public static explicit operator int(CompressedString x)
        {
            return int.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<uint>(CompressedString? x)
        {
            if (x is null) return null;
            return uint.Parse(x.Value);
        }
        public static explicit operator uint(CompressedString x)
        {
            return uint.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<long>(CompressedString? x)
        {
            if (x is null) return null;
            return long.Parse(x.Value);
        }
        public static explicit operator long(CompressedString x)
        {
            return long.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<ulong>(CompressedString? x)
        {
            if (x is null) return null;
            return ulong.Parse(x.Value);
        }
        public static explicit operator ulong(CompressedString x)
        {
            return ulong.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<decimal>(CompressedString? x)
        {
            if (x is null) return null;
            return decimal.Parse(x.Value);
        }
        public static explicit operator decimal(CompressedString x)
        {
            return decimal.Parse(x.Value);
        }
        public static explicit operator SqlDecimal(CompressedString x)
        {
            return SqlDecimal.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<float>(CompressedString? x)
        {
            if (x is null) return null;
            return float.Parse(x.Value);
        }
        public static explicit operator float(CompressedString x)
        {
            return float.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<double>(CompressedString? x)
        {
            if (x is null) return null;
            return double.Parse(x.Value);
        }
        public static explicit operator double(CompressedString x)
        {
            return double.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static implicit operator string?(CompressedString? x)
        {
            if (x is null) return null;
            return x.Value;
        }
        [return: NotNullIfNotNull("x")]
        public static implicit operator Lazy<string>?(CompressedString? x)
        {
            if (x is null) return null;
            return new Lazy<string>(() => x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<bool>(CompressedString x)
        {
            if (x is null || x.Value is null) return null;
            string value = x.Value.Trim();

            if (value.ToUpper() == "TRUE" || value.ToUpper() == "YES" || value == "1")
                return true;
            else if (value.ToUpper() == "FALSE" || value.ToUpper() == "NO" || value == "0")
                return false;

            return bool.Parse(value);
        }
        public static explicit operator bool(CompressedString x)
        {
            string value = x.Value.Trim();

            if (value.ToUpper() == "TRUE" || value.ToUpper() == "YES" || value == "1")
                return true;
            else if (value.ToUpper() == "FALSE" || value.ToUpper() == "NO" || value == "0")
                return false;

            return bool.Parse(value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<DateTime>(CompressedString? x)
        {
            if (x is null) return null;
            return DateTime.Parse(x.Value);
        }
        public static explicit operator DateTime(CompressedString x)
        {
            return DateTime.Parse(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator byte[]?(CompressedString? x)
        {
            if (x is null) return null;
            return Compress(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator XmlDocument?(CompressedString? x)
        {
            if (x is null) return null;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.InnerText = x.Value;
            return xmldoc;
        }
        public static explicit operator Guid(CompressedString x)
        {
            return new Guid(x.Value);
        }
        [return: NotNullIfNotNull("x")]
        public static explicit operator Nullable<Guid>(CompressedString? x)
        {
            if (x is null) return null;
            return new Guid(x.Value);
        }

        #endregion

        #region Operators
        public static bool operator ==(string? a, CompressedString? b)
        {
            if (a is null && b is null) { return true; }
            if (a is null) { return false; }
            if (b is null) { return false; }
            return a.Equals(b.Value);
        }
        public static bool operator !=(string? a, CompressedString? b)
        {

            return !(a == b);
        }

        public static CompressedString operator +(CompressedString? a, CompressedString? b)
        {
            if (a is null && b is null) { return new CompressedString(""); }
            if (a is null) { return b!; }
            if (b is null) { return a; }
            return new CompressedString(a.Value ?? "" + b.Value ?? "");
        }
        public static bool operator >(CompressedString? a, CompressedString? b)
        {
            if (a is null || b is null) { return false; }
            int max = Math.Min(a.Value.Length, b.Value.Length);

            int i = -1;
            do i++; while (i < max && a.Value[i] == b.Value[i]);

            if (i == max)
                return a.Value.Length > b.Value.Length;
            else
                return (a.Value[i] > b.Value[i]);
        }
        public static bool operator <(CompressedString? a, CompressedString? b)
        {
            if (a is null || b is null) { return false; }
            int max = Math.Min(a.Value.Length, b.Value.Length);

            int i = -1;
            do i++; while (i < max && a.Value[i] == b.Value[i]);

            if (i == max)
                return a.Value.Length < b.Value.Length;
            else
                return (a.Value[i] < b.Value[i]);
        }
        public static bool operator >=(CompressedString? a, CompressedString? b)
        {
            if (a is null && b is null) { return true; }
            if (a is null || b is null) { return false; }
            int max = Math.Min(a.Value.Length, b.Value.Length);

            int i = -1;
            do i++; while (i < max && a.Value[i] == b.Value[i]);

            if (i == max)
                return a.Value.Length >= b.Value.Length;
            else
                return (a.Value[i] >= b.Value[i]);
        }
        public static bool operator <=(CompressedString? a, CompressedString? b)
        {
            if (a is null && b is null) { return true; }
            if (a is null || b is null) { return false; }
            int max = Math.Min(a.Value.Length, b.Value.Length);

            int i = -1;
            do i++; while (i < max && a.Value[i] == b.Value[i]);

            if (i == max)
                return a.Value.Length <= b.Value.Length;
            else
                return (a.Value[i] <= b.Value[i]);

        }
        #endregion

        #region IConvertible Members

        TypeCode IConvertible.GetTypeCode()
        {
            return ((IConvertible)Value).GetTypeCode();
        }

        bool IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            if (conversionType == typeof(byte[]))
                return (byte[])this!;

            return ((IConvertible)Value).ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return ((IConvertible)Value).ToUInt64(provider);
        }
        #endregion

        public object Clone()
        {
            return new CompressedString(this);
        }
        public int CompareTo(object? value)
        {
            if (value is CompressedString)
            {
                return Value.CompareTo(value);
            }
            else
            {
                return 0;
            }
        }
        public bool Equals(CompressedString? other)
        {
            if (other is null) return false;
            return Value.Equals(other.Value, StringComparison.Ordinal);
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(string? obj)
        {
            if (obj is null)
                return false;

            return (this == obj);
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public int IndexOf(string x)
        {
            if (x is null) { return -1; }
            return Value.IndexOf(x, StringComparison.OrdinalIgnoreCase);
        }
        public int IndexOf(CompressedString x)
        {
            if (x is null) { return -1; }
            return Value.IndexOf(x.Value, StringComparison.OrdinalIgnoreCase);
        }
        public int IndexOf(string x, int y)
        {
            if (x is null) { return -1; }
            return Value.IndexOf(x, y, StringComparison.OrdinalIgnoreCase);
        }
        public int LastIndexOf(string x)
        {
            if (x is null) { return -1; }
            return Value.LastIndexOf(x, StringComparison.OrdinalIgnoreCase);
        }
        public int LastIndexOf(CompressedString x)
        {
            if (x is null) { return -1; }
            return Value.LastIndexOf(x.Value, StringComparison.OrdinalIgnoreCase);
        }
        public int LastIndexOf(string x, int y)
        {
            if (x is null) { return -1; }
            return Value.LastIndexOf(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public CompressedString ToLower()
        {
            return Value.ToLower()!;
        }
        public CompressedString ToUpper()
        {
            return Value.ToUpper()!;
        }

        public CompressedString Trim()
        {
            return Value.Trim()!;
        }
        public CompressedString Trim(string trimText)
        {
            return TrimStart(trimText).TrimEnd(trimText);
        }
        public CompressedString Trim(CompressedString trimText)
        {
            return TrimStart(trimText).TrimEnd(trimText);
        }
        public CompressedString Trim(params char[] trimChars)
        {
            return Value.Trim(trimChars)!;
        }
        public CompressedString TrimStart()
        {
            return Value.TrimStart()!;
        }
        public CompressedString TrimStart(string trimText)
        {
            if ((!string.IsNullOrEmpty(trimText)) && StartsWith(trimText))
                return Substring(trimText.Length);

            return this;
        }
        public CompressedString TrimStart(CompressedString trimText)
        {
            if ((!string.IsNullOrEmpty(trimText)) && StartsWith(trimText))
                return Substring(trimText.Length);

            return this;
        }
        public CompressedString TrimStart(params char[] trimChars)
        {
            return Value.TrimStart(trimChars)!;
        }
        public CompressedString TrimEnd()
        {
            return Value.TrimEnd()!;
        }
        public CompressedString TrimEnd(string trimText)
        {
            if ((!string.IsNullOrEmpty(trimText)) && EndsWith(trimText))
                return Substring(0, Length - trimText.Length);

            return this;
        }
        public CompressedString TrimEnd(CompressedString trimText)
        {
            if ((!string.IsNullOrEmpty(trimText)) && EndsWith(trimText))
                return Substring(0, Length - trimText.Length);

            return this;
        }
        public CompressedString TrimEnd(params char[] trimChars)
        {
            return Value.TrimEnd(trimChars)!;
        }

        public CompressedString Substring(int x)
        {
            return Value.Substring((int)x)!;
        }
        public CompressedString Substring(int x, int y)
        {
            return Value.Substring((int)x, (int)y)!;
        }
        public CompressedString Replace(string x, string y)
        {
            return Value.Replace(x, y)!;
        }
        public CompressedString Replace(CompressedString x, CompressedString y)
        {
            return Value.Replace(x.Value, y.Value)!;
        }

        public bool StartsWith(string? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.StartsWith(value);
        }
        public bool StartsWith(CompressedString? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.StartsWith(value.Value);
        }
        public bool EndsWith(string? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.EndsWith(value);
        }
        public bool EndsWith(CompressedString? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.EndsWith(value.Value);
        }
        public bool Contains(string? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.Contains(value);
        }
        public bool Contains(CompressedString? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return Value.Contains(value.Value);
        }

        static public string[] Split(string input, string delimiter)
        {
            return input.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        }
        static public string[] Split(string input, string delimiter, string qualifier)
        {
            return Split(input, delimiter, qualifier, false);
        }
        static public string[] Split(string input, string? delimiter, string? qualifier, bool ignoreCase)
        {
            bool qualifierState = false;
            int startIndex = 0;
            System.Collections.ArrayList values = new System.Collections.ArrayList();

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (!(qualifier is null) && (string.Compare(input.Substring(i, qualifier.Length), qualifier, ignoreCase) == 0))
                {
                    //edited by nitz : will still split according to delimiter despite faulty qualifiers
                    string nextStr = input.Substring(i + 1, qualifier.Length);
                    if ((qualifierState == false) || (string.Compare(nextStr, delimiter, ignoreCase) == 0))
                        qualifierState = !(qualifierState);
                }
                else if (!(qualifierState) && (delimiter is not null) && (string.Compare(input.Substring(i, delimiter.Length), delimiter, ignoreCase) == 0))
                {
                    values.Add(input.Substring(startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            if (startIndex < input.Length)
                values.Add(input.Substring(startIndex, input.Length - startIndex));

            string[] returnValues = new string[values.Count];
            values.CopyTo(returnValues);

            return returnValues;
        }
        static public CompressedString[] Split(CompressedString input, string? delimiter, string? qualifier)
        {
            return Split(input, delimiter, qualifier, false);
        }
        static public CompressedString[] Split(CompressedString input, string? delimiter, string? qualifier, bool ignoreCase)
        {
            bool qualifierState = false;
            int startIndex = 0;
            System.Collections.ArrayList values = new System.Collections.ArrayList();

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (!(qualifier is null) && (string.Compare(input.Substring(i, qualifier.Length), qualifier, ignoreCase) == 0))
                {
                    qualifierState = !(qualifierState);
                }
                else if (!(qualifierState) && (delimiter is not null) && (string.Compare(input.Substring(i, delimiter.Length), delimiter, ignoreCase) == 0))
                {
                    values.Add(input.Substring(startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            if (startIndex < input.Length)
                values.Add(input.Substring(startIndex, input.Length - startIndex));

            CompressedString[] returnValues = new CompressedString[values.Count];
            values.CopyTo(returnValues);

            return returnValues;
        }

        private static byte[] Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);                
                gZipStream.Flush();
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length + 4];
            int bytesRead = 0;
            while (bytesRead < memoryStream.Length)
            {
                bytesRead += memoryStream.Read(compressedData, bytesRead + 4, (int)memoryStream.Length - bytesRead);
            }

            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, compressedData, 0, 4);

            return compressedData;
        }
        private static string Decompress(byte[] gZipBuffer)
        {
            GZipStream? gZipStream;
            MemoryStream? memoryStream;
            byte[] buffer;

            using (memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);
                memoryStream.Flush();

                try
                {
                    buffer = new byte[dataLength];
                }
                catch (OutOfMemoryException)
                {
                    GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

                    buffer = new byte[dataLength];
                }

                memoryStream.Position = 0;
                using (gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    int bytesRead = 0;
                    while (bytesRead < buffer.Length)
                    {
                        bytesRead += gZipStream.Read(buffer, bytesRead, buffer.Length - bytesRead);
                    }
                }
            }
            memoryStream = null;
            gZipStream = null;

            try
            {
                return Encoding.UTF8.GetString(buffer);
            }
            catch (OutOfMemoryException)
            {
                GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
