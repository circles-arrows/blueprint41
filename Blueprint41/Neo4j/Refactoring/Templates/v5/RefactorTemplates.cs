using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Core;
using t = Blueprint41.Neo4j.Refactoring.Templates;

namespace Blueprint41.Core
{
    public class RefactorTemplates_v5 : RefactorTemplates_v4
    {
        public override t.CopyPropertyBase CopyProperty()
        {
            t.CopyPropertyBase template = new t.v5.CopyProperty();
            template.CreateInstance = CopyProperty;
            return template;
        }
        public override t.CreateIndexBase CreateIndex()
        {
            t.CreateIndexBase template = new t.v5.CreateIndex();
            template.CreateInstance = CreateIndex;
            return template;
        }
        public override t.CreateUniqueConstraintBase CreateUniqueConstraint()
        {
            t.CreateUniqueConstraintBase template = new t.v5.CreateUniqueConstraint();
            template.CreateInstance = CreateUniqueConstraint;
            return template;
        }
        public override t.DropExistConstraintBase DropExistConstraint()
        {
            t.DropExistConstraintBase template = new t.v5.DropExistConstraint();
            template.CreateInstance = DropExistConstraint;
            return template;
        }
        public override t.MergePropertyBase MergeProperty()
        {
            t.MergePropertyBase template = new t.v5.MergeProperty();
            template.CreateInstance = MergeProperty;
            return template;
        }
        public override t.RemovePropertyBase RemoveProperty()
        {
            t.RemovePropertyBase template = new t.v5.RemoveProperty();
            template.CreateInstance = RemoveProperty;
            return template;
        }
        public override t.RenamePropertyBase RenameProperty()
        {
            t.RenamePropertyBase template = new t.v5.RenameProperty();
            template.CreateInstance = RenameProperty;
            return template;
        }
        public override t.SetCreationDateBase SetCreationDate()
        {
            t.SetCreationDateBase template = new t.v5.SetCreationDate();
            template.CreateInstance = SetCreationDate;
            return template;
        }
        public override t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.v5.SetDefaultConstantValue();
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
        public override t.SetRelationshipPropertyValueBase SetRelationshipPropertyValue()
        {
            t.SetRelationshipPropertyValueBase template = new t.v5.SetRelationshipPropertyValue();
            template.CreateInstance = SetRelationshipPropertyValue;
            return template;
        }
    }
}
