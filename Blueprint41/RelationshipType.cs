using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41
{
    public enum RelationshipType
    {
        None,
        Lookup,
        Lookup_Lookup,
        Lookup_Collection,
        Collection,
        Collection_Lookup,
        Collection_Collection,
    }

    public static partial class ExtensionMethods
    {
        internal static bool IsCompatible(this RelationshipType self, RelationshipType other)
        {
            if (self == RelationshipType.None || other == RelationshipType.None)
                return true;

            switch (self)
            {
                case RelationshipType.Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Collection_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Collection:
                            case RelationshipType.Collection_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Collection:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Collection:
                                return true;
                            default:
                                return false;
                        }
                    }
                case RelationshipType.Lookup_Lookup:
                    {
                        switch (other)
                        {
                            case RelationshipType.Lookup:
                            case RelationshipType.Lookup_Lookup:
                                return true;
                            default:
                                return false;
                        }
                    }
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitLookup(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                    return false;
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                case RelationshipType.Lookup_Lookup:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
        internal static bool IsImplicitCollection(this RelationshipType self)
        {
            switch (self)
            {
                case RelationshipType.None:
                case RelationshipType.Collection:
                case RelationshipType.Collection_Collection:
                case RelationshipType.Collection_Lookup:
                case RelationshipType.Lookup:
                case RelationshipType.Lookup_Collection:
                    return true;
                case RelationshipType.Lookup_Lookup:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
