﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    internal class TimeDependentAddRelationshipAction : TimeDependentRelationshipAction
    {
        internal TimeDependentAddRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, DateTime? moment, Dictionary<string, object>? properties)
            : base(persistenceProvider, relationship, inItem, outItem, moment)
        {
            Properties = properties;
        }

        public Dictionary<string, object>? Properties { get; private set; }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.Add(relationship, InItem!, OutItem!, Moment, true, Properties);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            bool wasUpdated = false;
            int[] indexes = target.IndexOf(target.ForeignItem(this)!);
            foreach (int index in indexes)
            {
                CollectionItem? item = target.GetItem(index);
                if (item is not null)
                {
                    if (item.IsAfter(Moment))
                    {
                        target.RemoveAt(index);
                    }
                    else if (item.OverlapsOrIsAttached(Moment))
                    {
                        target.SetItem(index, target.NewCollectionItem(target.Parent, item.Item, item.StartDate, null));
                        wasUpdated = true;
                    }
                }
            }
            if (!wasUpdated)
                target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this)!, Moment, null));
        }
    }
}
