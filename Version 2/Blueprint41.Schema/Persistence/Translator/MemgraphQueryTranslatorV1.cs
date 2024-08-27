﻿using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;

namespace Blueprint41.Persistence
{
    internal class MemgraphQueryTranslatorV1 : QueryTranslator
    {
        internal MemgraphQueryTranslatorV1(DatastoreModel datastoreModel) : base(datastoreModel)
        {
        }

        #region Compile Functions

        // Element ID is not a string on Memgraph, this might cause performance issues with updating relationship properties...
        public override string FnElementId => "toString(Id({0}))";
        public override string TestCompressedString(string alias, string field) => throw new NotSupportedException("CompressedString is not supported on Memgraph, since ByteArray is not supported on Memgraph. See: https://memgraph.com/docs/client-libraries/java");
        public override string FnCollectSubquery => throw new NotSupportedException("Memgraph does not support Collect subqueries as of writing.");
        public override string FnCountSubquery => throw new NotSupportedException("Memgraph does not support Count subqueries as of writing.");
        public override string FnExistsSubquery => throw new NotSupportedException("Memgraph does not support Exists subqueries as of writing.");

        #endregion

        #region Full Text Indexes

        internal override bool HasFullTextSearchIndexes()
        {
            //TODO: memgraph does not support Full-Text search indexes
            return false;
        }

        internal override void ApplyFullTextSearchIndexes(IEnumerable<Entity> entities)
        {
            //TODO: DO NOTHING memgraph does not support Full-Text search indexes
        }

        #endregion

        //internal override void OrderQueryParts(LinkedList<Query.Query> parts)
        //{
        //    var usingIndexParts = parts.Where(p => p.Type == PartType.UsingIndex).ToList();
        //    var otherParts = parts.Where(p => p.Type != PartType.UsingIndex).ToList();

        //    parts.Clear();

        //    if (usingIndexParts.Any())
        //    {
        //        var combinedFields = usingIndexParts.SelectMany(part => part.Fields).Distinct().ToArray();
        //        usingIndexParts[0].SetFields(combinedFields);
        //        parts.AddLast(usingIndexParts[0]);
        //    }

        //    foreach (var part in otherParts)
        //    {
        //        parts.AddLast(part);
        //    }
        //}
        //internal override void Compile(Query.Query query, CompileState state)
        //{
        //    if (query.Type == PartType.UsingIndex)
        //    {
        //        state.Text.Append("USING INDEX ");
        //        query.ForEach(query.Fields, state.Text, ",", item =>
        //        {
        //            if (item.Alias is null || item.Alias.Node is null)
        //                return;

        //            state.Text.Append(string.Format(":{0}({1})", item.Alias.Node.Neo4jLabel, item.FieldName));
        //        });
        //    }
        //    else
        //        base.Compile(query, state);
        //}
    }
}