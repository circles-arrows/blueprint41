using System;
using System.Collections.Generic;

using Blueprint41.Core;
using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Refactoring
{
    public class RefactorTemplates_Neo4jV5 : RefactorTemplates_Neo4jV4
    {
        internal RefactorTemplates_Neo4jV5(DatastoreModel datastoreModel) : base(datastoreModel) { }
        public override t.CopyPropertyBase CopyProperty()
        {
            t.CopyPropertyBase template = new t.Neo4jV5.CopyProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = CopyProperty;
            return template;
        }
        public override t.MergePropertyBase MergeProperty()
        {
            t.MergePropertyBase template = new t.Neo4jV5.MergeProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = MergeProperty;
            return template;
        }
        public override t.RemovePropertyBase RemoveProperty()
        {
            t.RemovePropertyBase template = new t.Neo4jV5.RemoveProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RemoveProperty;
            return template;
        }
        public override t.RenamePropertyBase RenameProperty()
        {
            t.RenamePropertyBase template = new t.Neo4jV5.RenameProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RenameProperty;
            return template;
        }
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.Neo4jV5.SetDefaultConstantValue();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
    }
}
