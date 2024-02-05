using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using t = Blueprint41.Neo4j.Refactoring.Templates;

namespace Blueprint41.Core
{
    public class RefactorTemplates
    {

        #region ApplyFunctionalId

        public virtual t.ApplyFunctionalIdBase ApplyFunctionalId()
        {
            t.ApplyFunctionalIdBase template = new t.ApplyFunctionalId();
            template.CreateInstance = ApplyFunctionalId;
            return template;
        }
        public t.ApplyFunctionalIdBase ApplyFunctionalId(Action<t.ApplyFunctionalIdBase> setup)
        {
            t.ApplyFunctionalIdBase template = ApplyFunctionalId();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region Convert

        public virtual t.ConvertBase Convert()
        {
            t.ConvertBase template = new t.Convert();
            template.CreateInstance = Convert;
            return template;
        }
        public t.ConvertBase Convert(Action<t.ConvertBase> setup)
        {
            t.ConvertBase template = Convert();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region CopyProperty

        public virtual t.CopyPropertyBase CopyProperty()
        {
            t.CopyPropertyBase template = new t.CopyProperty();
            template.CreateInstance = CopyProperty;
            return template;
        }
        public t.CopyPropertyBase CopyProperty(Action<t.CopyPropertyBase> setup)
        {
            t.CopyPropertyBase template = CopyProperty();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region MergeProperty

        public virtual t.MergePropertyBase MergeProperty()
        {
            t.MergePropertyBase template = new t.MergeProperty();
            template.CreateInstance = MergeProperty;
            return template;
        }
        public t.MergePropertyBase MergeProperty(Action<t.MergePropertyBase> setup)
        {
            t.MergePropertyBase template = MergeProperty();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RemoveEntity

        public virtual t.RemoveEntityBase RemoveEntity()
        {
            t.RemoveEntityBase template = new t.RemoveEntity();
            template.CreateInstance = RemoveEntity;
            return template;
        }
        public t.RemoveEntityBase RemoveEntity(Action<t.RemoveEntityBase> setup)
        {
            t.RemoveEntityBase template = RemoveEntity();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RemoveProperty

        public virtual t.RemovePropertyBase RemoveProperty()
        {
            t.RemovePropertyBase template = new t.RemoveProperty();
            template.CreateInstance = RemoveProperty;
            return template;
        }
        public t.RemovePropertyBase RemoveProperty(Action<t.RemovePropertyBase> setup)
        {
            t.RemovePropertyBase template = RemoveProperty();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RemoveRelationship

        public virtual t.RemoveRelationshipBase RemoveRelationship()
        {
            t.RemoveRelationshipBase template = new t.RemoveRelationship();
            template.CreateInstance = RemoveRelationship;
            return template;
        }
        public t.RemoveRelationshipBase RemoveRelationship(Action<t.RemoveRelationshipBase> setup)
        {
            t.RemoveRelationshipBase template = RemoveRelationship();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RenameEntity

        public virtual t.RenameEntityBase RenameEntity()
        {
            t.RenameEntityBase template = new t.RenameEntity();
            template.CreateInstance = RenameEntity;
            return template;
        }
        public t.RenameEntityBase RenameEntity(Action<t.RenameEntityBase> setup)
        {
            t.RenameEntityBase template = RenameEntity();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RenameProperty

        public virtual t.RenamePropertyBase RenameProperty()
        {
            t.RenamePropertyBase template = new t.RenameProperty();
            template.CreateInstance = RenameProperty;
            return template;
        }
        public t.RenamePropertyBase RenameProperty(Action<t.RenamePropertyBase> setup)
        {
            t.RenamePropertyBase template = RenameProperty();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region RenameRelationship

        public virtual t.RenameRelationshipBase RenameRelationship()
        {
            t.RenameRelationshipBase template = new t.RenameRelationship();
            template.CreateInstance = RenameRelationship;
            return template;
        }
        public t.RenameRelationshipBase RenameRelationship(Action<t.RenameRelationshipBase> setup)
        {
            t.RenameRelationshipBase template = RenameRelationship();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region SetDefaultConstantValue

        public virtual t.SetDefaultConstantValueBase SetDefaultConstantValue()
        {
            t.SetDefaultConstantValueBase template = new t.SetDefaultConstantValue();
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
        public t.SetDefaultConstantValueBase SetDefaultConstantValue(Action<t.SetDefaultConstantValueBase> setup)
        {
            t.SetDefaultConstantValueBase template = SetDefaultConstantValue();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region SetDefaultLookupValue

        public virtual t.SetDefaultLookupValueBase SetDefaultLookupValue()
        {
            t.SetDefaultLookupValueBase template = new t.SetDefaultLookupValue();
            template.CreateInstance = SetDefaultLookupValue;
            return template;
        }
        public t.SetDefaultLookupValueBase SetDefaultLookupValue(Action<t.SetDefaultLookupValueBase> setup)
        {
            t.SetDefaultLookupValueBase template = SetDefaultLookupValue();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

        #region SetLabel

        public virtual t.SetLabelBase SetLabel()
        {
            t.SetLabelBase template = new t.SetLabel();
            template.CreateInstance = SetLabel;
            return template;
        }
        public t.SetLabelBase SetLabel(Action<t.SetLabelBase> setup)
        {
            t.SetLabelBase template = SetLabel();
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

    }
}