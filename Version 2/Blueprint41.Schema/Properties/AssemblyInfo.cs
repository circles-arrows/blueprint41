using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Blueprint41.Core;
using Blueprint41.TypeConversion;

[assembly: InternalsVisibleTo("Blueprint41.OGM")]
[assembly: InternalsVisibleTo("Blueprint41.Query")]

#region Type Conversions

[assembly: Conversion(typeof(DecimalToLong))]
[assembly: Conversion(typeof(LongToDecimal))]
[assembly: Conversion(typeof(DateTimeToLong))]
[assembly: Conversion(typeof(LongToDateTime))]
[assembly: Conversion(typeof(GuidToString))]
[assembly: Conversion(typeof(StringToGuid))]

// Auto generated converters can be found in: ... 

#endregion