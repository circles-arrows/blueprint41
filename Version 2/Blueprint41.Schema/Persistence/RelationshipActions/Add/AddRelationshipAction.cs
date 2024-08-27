﻿using System;
using System.Collections.Generic;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Persistence
{
    internal class AddRelationshipAction : RelationshipAction
    {
        internal AddRelationshipAction(RelationshipPersistenceProvider persistenceProvider, Relationship relationship, OGM inItem, OGM outItem, Dictionary<string, object>? properties)
            : base(persistenceProvider, relationship, inItem, outItem)
        {
            Properties = properties;
        }

        public Dictionary<string, object>? Properties { get; private set; }

        protected override void InDatastoreLogic(Relationship relationship)
        {
            PersistenceProvider.Add(relationship, InItem!, OutItem!, null, false, Properties);
        }

        protected override void InMemoryLogic(EntityCollectionBase target)
        {
            bool contains = target.IndexOf(target.ForeignItem(this)!).Length != 0;
            if (!contains)
                target.Add(target.NewCollectionItem(target.Parent, target.ForeignItem(this)!, null, null));
        }


    }
}