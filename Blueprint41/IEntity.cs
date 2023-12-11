using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41
{
    public interface IEntity
    {
        DatastoreModel Parent { get; }
        Guid Guid { get; }
        string Name { get; }
        bool IsAbstract { get; }

        PropertyCollection Properties { get; }
        IReadOnlyList<Property> FullTextIndexProperties { get; }

        IEntity SetFullTextProperty(string propertyName);
        IEntity RemoveFullTextProperty(string propertyName);

        bool IsSubsclassOf(IEntity baseType);
        bool IsSelfOrSubclassOf(IEntity baseType);
        IReadOnlyList<IEntity> GetBaseTypes();
        IReadOnlyList<IEntity> GetBaseTypesAndSelf();
        IReadOnlyList<IEntity> GetSubclassesOrSelf();
        IReadOnlyList<IEntity> GetSubclasses();
        IReadOnlyList<IEntity> GetDirectSubclasses();
        IReadOnlyList<IEntity> GetNearestNonVirtualSubclass();
        IReadOnlyList<Property> GetPropertiesOfBaseTypesAndSelf();
        IReadOnlyList<IEntity> GetConcreteClasses();

        void DynamicEntityPropertyAdded(Property property);
        void DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm = MergeAlgorithm.NotApplicable);
        void DynamicEntityPropertyConverted(Property property, Type target);
        void DynamicEntityPropertyRerouted(string oldname, Entity to, Property property);
        void DynamicEntityPropertyRemoved(Property property);
    }

    partial class Entity : IEntity
    {
        PropertyCollection IEntity.Properties => _properties;
        
        IEntity IEntity.SetFullTextProperty(string propertyName) => SetFullTextProperty(propertyName);
        IEntity IEntity.RemoveFullTextProperty(string propertyName) => RemoveFullTextProperty(propertyName);
        IReadOnlyList<Property> IEntity.FullTextIndexProperties => FullTextIndexProperties;

        IReadOnlyList<IEntity> IEntity.GetBaseTypes() => GetBaseTypes();
        IReadOnlyList<IEntity> IEntity.GetBaseTypesAndSelf() => GetBaseTypesAndSelf();
        IReadOnlyList<IEntity> IEntity.GetSubclassesOrSelf() => GetSubclassesOrSelf();
        IReadOnlyList<IEntity> IEntity.GetSubclasses() => GetSubclasses();
        IReadOnlyList<IEntity> IEntity.GetDirectSubclasses() => GetDirectSubclasses();
        IReadOnlyList<IEntity> IEntity.GetNearestNonVirtualSubclass() => GetNearestNonVirtualSubclass();
        IReadOnlyList<Property> IEntity.GetPropertiesOfBaseTypesAndSelf() => GetPropertiesOfBaseTypesAndSelf();
        IReadOnlyList<IEntity> IEntity.GetConcreteClasses() => GetConcreteClasses();

        void IEntity.DynamicEntityPropertyAdded(Property property) => DynamicEntityPropertyAdded(property);
        void IEntity.DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm) => DynamicEntityPropertyRenamed(oldname, property, mergeAlgorithm);
        void IEntity.DynamicEntityPropertyConverted(Property property, Type target) => DynamicEntityPropertyConverted(property, target);
        void IEntity.DynamicEntityPropertyRerouted(string oldname, Entity to, Property property) => DynamicEntityPropertyRerouted(oldname, to, property);
        void IEntity.DynamicEntityPropertyRemoved(Property property) => DynamicEntityPropertyRemoved(property);
    }
    partial class Relationship : IEntity
    {
        bool IEntity.IsAbstract => false;
        PropertyCollection IEntity.Properties => _properties;

        IEntity IEntity.SetFullTextProperty(string propertyName) => SetFullTextProperty(propertyName);
        IEntity IEntity.RemoveFullTextProperty(string propertyName) => RemoveFullTextProperty(propertyName);
        IReadOnlyList<Property> IEntity.FullTextIndexProperties => FullTextIndexProperties;

        IReadOnlyList<IEntity> IEntity.GetBaseTypes() => _empty;
        IReadOnlyList<IEntity> IEntity.GetBaseTypesAndSelf() => _self;
        IReadOnlyList<IEntity> IEntity.GetSubclassesOrSelf() => _self;
        IReadOnlyList<IEntity> IEntity.GetSubclasses() => _empty;
        IReadOnlyList<IEntity> IEntity.GetDirectSubclasses() => _empty;
        IReadOnlyList<IEntity> IEntity.GetNearestNonVirtualSubclass() => _empty;
        IReadOnlyList<Property> IEntity.GetPropertiesOfBaseTypesAndSelf() => _properties.ToArray();
        IReadOnlyList<IEntity> IEntity.GetConcreteClasses() => _self;

        bool IEntity.IsSubsclassOf(IEntity baseType) => false;
        bool IEntity.IsSelfOrSubclassOf(IEntity baseType) => (baseType == this);


        private static readonly IReadOnlyList<IEntity> _empty = new IEntity[0];
        private readonly IReadOnlyList<IEntity> _self;

        void IEntity.DynamicEntityPropertyAdded(Property property) => throw new NotImplementedException();
        void IEntity.DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm) => throw new NotImplementedException();
        void IEntity.DynamicEntityPropertyConverted(Property property, Type target) => throw new NotImplementedException();
        void IEntity.DynamicEntityPropertyRerouted(string oldname, Entity to, Property property) => throw new NotImplementedException();
        void IEntity.DynamicEntityPropertyRemoved(Property property) => throw new NotImplementedException();
    }
}
