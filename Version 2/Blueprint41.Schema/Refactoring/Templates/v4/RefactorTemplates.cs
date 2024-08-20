using System;
using System.Collections.Generic;

using Blueprint41.Core;
using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Refactoring
{
    public class RefactorTemplates_v4 : RefactorTemplates
    {
        internal RefactorTemplates_v4(DatastoreModel datastoreModel) : base(datastoreModel) { }
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.v4.SetDefaultConstantValue();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
    }
}
