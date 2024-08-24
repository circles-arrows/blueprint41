using System;
using System.Collections.Generic;

using Blueprint41.Core;
using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Refactoring
{
    public class RefactorTemplates_Neo4jV4 : RefactorTemplates
    {
        internal RefactorTemplates_Neo4jV4(DatastoreModel datastoreModel) : base(datastoreModel) { }
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.Neo4jV4.SetDefaultConstantValue();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
    }
}
