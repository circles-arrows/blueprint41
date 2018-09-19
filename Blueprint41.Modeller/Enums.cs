using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public enum Multiplicity : int
    {
        Lookup = 0,
        LookupLookup = 1,
        LookupCollection = 2,
        Collection = 3,
        CollectionLookup = 4,
        CollectionCollection = 5
    }

    public enum PropertyType
    {
        None,
        Lookup,
        Collection
    }

    public enum PropertyIndex
    {
        None,
        Indexed,
        Unique
    }

    public enum RelationshipDirection
    {
        In,
        Out,
        Both
    }

    public enum EditorViewMode
    {
        Draggable,
        Fix,
    }
}
