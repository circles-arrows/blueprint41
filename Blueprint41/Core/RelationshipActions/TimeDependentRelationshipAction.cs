using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal abstract class TimeDependentRelationshipAction : RelationshipAction
    {
        internal TimeDependentRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? moment)
            : base(persistenceProvider, relationship, inItem, outItem)
        {

            // WATCH OUT!!!!
            // you cannot set a NULL datetime to DateTime.MinValue, the default is to have actions happen at the TransactionDate!!!
            Moment = (moment.HasValue) ? moment.Value : Transaction.RunningTransaction.TransactionDate;
        }

        //static private readonly DateTime MinDateTime = DateTime.MinValue; 

        public DateTime Moment { get; private set; }
    }
}
