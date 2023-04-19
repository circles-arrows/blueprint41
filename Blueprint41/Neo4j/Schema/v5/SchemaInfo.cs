using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Blueprint41.Core;

namespace Blueprint41.Neo4j.Schema.v5
{
    public class SchemaInfo_v5 : v4.SchemaInfo_v4
    {
        internal SchemaInfo_v5(DatastoreModel model) : base(model) { }

        protected override void Initialize()
        {
            using (Transaction.Begin())
            {
                bool hasPlugin = Model.PersistenceProvider.Translator.HasBlueprint41FunctionalidFnNext.Value;
                FunctionalIds = hasPlugin ? LoadData("CALL blueprint41.functionalid.list()", record => NewFunctionalIdInfo(record)) : new List<FunctionalIdInfo>(0);
                Constraints = LoadData("show constraints", record => NewConstraintInfo(record)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Indexes = LoadData("show indexes", record => NewIndexInfo(record)).Where(item => item.Entity is not null && item.Field is not null).ToArray();
                Labels = LoadSimpleData("CALL db.labels()", "label");
                PropertyKeys = LoadSimpleData("CALL db.propertyKeys()", "propertyKey");
                RelationshipTypes = LoadSimpleData("CALL db.relationshipTypes()", "relationshipType");
            }
        }

        protected override ConstraintInfo NewConstraintInfo(RawRecord rawRecord) => new ConstraintInfo_v5(rawRecord);
        protected override IndexInfo NewIndexInfo(RawRecord rawRecord)           => new IndexInfo_v5(rawRecord);

    }
}