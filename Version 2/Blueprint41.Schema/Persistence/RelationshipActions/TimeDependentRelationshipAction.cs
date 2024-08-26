using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    internal abstract class TimeDependentRelationshipAction : RelationshipAction
    {
        internal TimeDependentRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM? inItem, OGM? outItem, DateTime? moment)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
            // WATCH OUT: null should be interpreted as "since forever", if TransactionDate was intended please pass that instead when constructing this object.
            Moment = moment.HasValue ? moment.Value : Conversion.MinDateTime;
        }

        public DateTime Moment { get; protected set; }
    }
}
