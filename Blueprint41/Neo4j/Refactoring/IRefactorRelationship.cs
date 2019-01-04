﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Refactoring
{
    public interface IRefactorRelationship
    {
        void Rename(string newName, string alias);

        void SetInEntity(Entity target, bool allowLosingData = false);
        void SetOutEntity(Entity target, bool allowLosingData = false);

        void RemoveTimeDependance();

        void Merge(Relationship target);

        void Deprecate();
    }
}
