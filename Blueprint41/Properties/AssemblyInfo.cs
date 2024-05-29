﻿using Blueprint41.Core;
using Blueprint41.TypeConversion;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Blueprint41")]
[assembly: AssemblyDescription("An Object Graph Mapper for CSharp to connect to Neo4j or Memgraph. It has support for defining the object model as a schema. It support refactor scripts written in CSharp, which you can add to your project. When you run your program and the graph is of an older version, the upgrade script will automatically be executed against the graph. It also support generation of type-safe data objects, so you know at compile time if your code is compatible with the latest upgrades.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CIRCLES ARROWS LIMITED")]
[assembly: AssemblyProduct("Blueprint41")]
[assembly: AssemblyCopyright("Copyright © 2017, 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c2e15838-2187-461f-aa9a-d00f0123f9c2")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.4.0")]
[assembly: AssemblyFileVersion("1.1.4.0")]

[assembly: InternalsVisibleTo("Blueprint41.Modeller.Compare")]
[assembly: InternalsVisibleTo("Blueprint41.Neo4jDriver.v3")]
[assembly: InternalsVisibleTo("Blueprint41.Neo4jDriver.v4")]
[assembly: InternalsVisibleTo("Blueprint41.Neo4jDriver.v5")]
[assembly: InternalsVisibleTo("Blueprint41.MemgraphDriver")]
[assembly: InternalsVisibleTo("Blueprint41.Snapshot")]

#region Type Conversions

[assembly: Conversion(typeof(DecimalToLong))]
[assembly: Conversion(typeof(LongToDecimal))]
[assembly: Conversion(typeof(DateTimeToLong))]
[assembly: Conversion(typeof(LongToDateTime))]
[assembly: Conversion(typeof(GuidToString))]
[assembly: Conversion(typeof(StringToGuid))]

// Auto generated converters can be found in: ... 

#endregion
