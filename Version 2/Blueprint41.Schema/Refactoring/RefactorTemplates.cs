using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using t = Blueprint41.Refactoring.Templates;

namespace Blueprint41.Core
{
    public class RefactorTemplates
    {
        internal RefactorTemplates(DatastoreModel datastoreModel)
        {
            DatastoreModel = datastoreModel;
        }

        protected readonly DatastoreModel DatastoreModel;
        #region ApplyFunctionalId

        public virtual t.ApplyFunctionalIdBase ApplyFunctionalId()
        {
            t.ApplyFunctionalIdBase template = new t.ApplyFunctionalId();
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = ApplyFunctionalId;
            return template;
        }
        public t.ApplyFunctionalIdBase ApplyFunctionalId(Action<t.ApplyFunctionalIdBase> setup)
        {
            t.ApplyFunctionalIdBase template = ApplyFunctionalId();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = Convert;
            return template;
        }
        public t.ConvertBase Convert(Action<t.ConvertBase> setup)
        {
            t.ConvertBase template = Convert();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = CopyProperty;
            return template;
        }
        public t.CopyPropertyBase CopyProperty(Action<t.CopyPropertyBase> setup)
        {
            t.CopyPropertyBase template = CopyProperty();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = MergeProperty;
            return template;
        }
        public t.MergePropertyBase MergeProperty(Action<t.MergePropertyBase> setup)
        {
            t.MergePropertyBase template = MergeProperty();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RemoveEntity;
            return template;
        }
        public t.RemoveEntityBase RemoveEntity(Action<t.RemoveEntityBase> setup)
        {
            t.RemoveEntityBase template = RemoveEntity();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RemoveProperty;
            return template;
        }
        public t.RemovePropertyBase RemoveProperty(Action<t.RemovePropertyBase> setup)
        {
            t.RemovePropertyBase template = RemoveProperty();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RemoveRelationship;
            return template;
        }
        public t.RemoveRelationshipBase RemoveRelationship(Action<t.RemoveRelationshipBase> setup)
        {
            t.RemoveRelationshipBase template = RemoveRelationship();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RenameEntity;
            return template;
        }
        public t.RenameEntityBase RenameEntity(Action<t.RenameEntityBase> setup)
        {
            t.RenameEntityBase template = RenameEntity();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RenameProperty;
            return template;
        }
        public t.RenamePropertyBase RenameProperty(Action<t.RenamePropertyBase> setup)
        {
            t.RenamePropertyBase template = RenameProperty();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = RenameRelationship;
            return template;
        }
        public t.RenameRelationshipBase RenameRelationship(Action<t.RenameRelationshipBase> setup)
        {
            t.RenameRelationshipBase template = RenameRelationship();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultConstantValue;
            return template;
        }
        public t.SetDefaultConstantValueBase SetDefaultConstantValue(Action<t.SetDefaultConstantValueBase> setup)
        {
            t.SetDefaultConstantValueBase template = SetDefaultConstantValue();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetDefaultLookupValue;
            return template;
        }
        public t.SetDefaultLookupValueBase SetDefaultLookupValue(Action<t.SetDefaultLookupValueBase> setup)
        {
            t.SetDefaultLookupValueBase template = SetDefaultLookupValue();
            template.DatastoreModel = DatastoreModel;
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
            template.DatastoreModel = DatastoreModel;
            template.CreateInstance = SetLabel;
            return template;
        }
        public t.SetLabelBase SetLabel(Action<t.SetLabelBase> setup)
        {
            t.SetLabelBase template = SetLabel();
            template.DatastoreModel = DatastoreModel;
            template.Setup = setup;
            if (setup is not null)
                setup.Invoke(template);
            return template;
        }

        #endregion

    }
}
