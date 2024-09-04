using Blueprint41.Core;
using Blueprint41.TypeConversion;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#region Type Conversions

[assembly: Conversion(typeof(DecimalToLong))]
[assembly: Conversion(typeof(LongToDecimal))]
[assembly: Conversion(typeof(DateTimeToLong))]
[assembly: Conversion(typeof(LongToDateTime))]
[assembly: Conversion(typeof(GuidToString))]
[assembly: Conversion(typeof(StringToGuid))]

// Auto generated converters can be found in: ... 

#endregion