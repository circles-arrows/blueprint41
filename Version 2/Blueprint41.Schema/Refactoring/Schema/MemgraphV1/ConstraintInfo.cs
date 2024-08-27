﻿using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.Persistence;

namespace Blueprint41.Refactoring.Schema
{
    public class ConstraintInfo_MemgraphV1: ConstraintInfo
    {
        public override bool IsKey { get; }

        internal ConstraintInfo_MemgraphV1(IDictionary<string, object> record, PersistenceProvider persistenceProvider) : base(record, persistenceProvider) { }

        protected override void Initialize(IDictionary<string, object> record)
        {
            //Name = record.Values["name"].As<string>();
            IsUnique = false;
            IsMandatory = false;
            string constraintType = record["constraint type"]?.As<string>()?.ToLowerInvariant() ?? "";
            if (constraintType.Contains("unique") || constraintType.Contains("key"))
                IsUnique = true;

            if (constraintType.Contains("exists") || constraintType.Contains("key"))
                IsMandatory = true;

            Entity = new List<string>() { record["label"].ToString()! };
            Field = IsMandatory ? new List<string>() { record["properties"].ToString()! } : record["properties"]?.As<List<object>>()?.Cast<string>()?.ToList()!;
        }
    }
}