using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Refactoring
{
    public class RefactorTemplates_v5 : RefactorTemplates_v4
    {
        internal RefactorTemplates_v5(DatastoreModel datastoreModel) : base(datastoreModel) { }
        public override t.CopyPropertyBase CopyProperty()
        {
            t.CopyPropertyBase template = new t.v5.CopyProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = CopyProperty;
            return template;
        }
        public override t.MergePropertyBase MergeProperty()
        {
            t.MergePropertyBase template = new t.v5.MergeProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = MergeProperty;
            return template;
        }
        public override t.RemovePropertyBase RemoveProperty()
        {
            t.RemovePropertyBase template = new t.v5.RemoveProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RemoveProperty;
            return template;
        }
        public override t.RenamePropertyBase RenameProperty()
        {
            t.RenamePropertyBase template = new t.v5.RenameProperty();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RenameProperty;
            return template;
        }
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.v5.SetDefaultConstantValue();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
    }
}
