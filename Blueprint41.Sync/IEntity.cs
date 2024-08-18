using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Sync
{
    public interface IEntity
    {
        /// <summary>
        /// The parent data-store of this entity or relationship
        /// </summary>
        DatastoreModel Parent { get; }

        /// <summary>
        /// A unique identifier for this entity or relationship
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// The name of this entity or relationship
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The label or relationship-type of this entity or relationship
        /// </summary>
        string Neo4jName { get; }

        /// <summary>
        /// Whether this entity or relationship is abstract
        /// </summary>
        bool IsAbstract { get; }

        /// <summary>
        /// Whether this entity or relationship is virtual
        /// </summary>
        bool IsVirtual { get; }

        /// <summary>
        /// The base type of this entity
        /// </summary>
        IEntity? Inherits { get; }

        /// <summary>
        /// The properties of this entity or relationship
        /// </summary>
        PropertyCollection Properties { get; }

        /// <summary>
        /// The properties of this entity or relationship that are part of the full text index
        /// </summary>
        IReadOnlyList<Property> FullTextIndexProperties { get; }

        /// <summary>
        /// Add a property to the full text index of this entity or relationship
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        IEntity SetFullTextProperty(string propertyName);

        /// <summary>
        /// Remove a property from the full text index of this entity or relationship
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        IEntity RemoveFullTextProperty(string propertyName);

        /// <summary>
        /// Check whether this entity is a subclass of the given base type
        /// </summary>
        /// <param name="baseType">The base type to check</param>
        /// <returns>True if this entity is a subclass of the given base type</returns>
        bool IsSubsclassOf(IEntity baseType);

        /// <summary>
        /// Check whether this entity is a subclass or equal to the given base type
        /// </summary>
        /// <param name="baseType">The base type to check</param>
        /// <returns>True if this entity is a subclass or equal to the given base type</returns>
        bool IsSelfOrSubclassOf(IEntity baseType);

        /// <summary>
        /// Get the base types of this entity
        /// </summary>
        /// <returns>The list of base types</returns>
        IReadOnlyList<IEntity> GetBaseTypes();

        /// <summary>
        /// Get the base types and this entity
        /// </summary>
        /// <returns>The list of base types</returns>
        IReadOnlyList<IEntity> GetBaseTypesAndSelf();

        /// <summary>
        /// Get the subclasses and this entity
        /// </summary>
        /// <returns>The list of subclasses</returns>
        IReadOnlyList<IEntity> GetSubclassesOrSelf();

        /// <summary>
        /// Get the subclasses of this entity
        /// </summary>
        /// <returns>The list of subclasses</returns>
        IReadOnlyList<IEntity> GetSubclasses();

        /// <summary>
        /// Get theThe direct subclasses of this entity
        /// </summary>
        /// <returns>The list of direct subclasses</returns>
        IReadOnlyList<IEntity> GetDirectSubclasses();

        /// <summary>
        /// Get the nearest non-virtual subclass of this entity
        /// </summary>
        /// <returns>The nearest non-virtual subclass</returns>
        IReadOnlyList<IEntity> GetNearestNonVirtualSubclass();

        /// <summary>
        /// Get the properties of this entity and its base types
        /// </summary>
        /// <returns>The list of properties</returns>
        IReadOnlyList<Property> GetPropertiesOfBaseTypesAndSelf();

        /// <summary>
        /// Get the concrete classes of this entity
        /// </summary>
        /// <returns>The list of concrete classes</returns>
        IReadOnlyList<IEntity> GetConcreteClasses();

        void DynamicEntityPropertyAdded(Property property);
        void DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm = MergeAlgorithm.NotApplicable);
        void DynamicEntityPropertyConverted(Property property, Type target);
        void DynamicEntityPropertyRerouted(string oldname, Entity to, Property property);
        void DynamicEntityPropertyRemoved(Property property);
    }

    partial class Entity : IEntity
    {
        /// <summary>
        /// The label of this entity
        /// </summary>
        string IEntity.Neo4jName => Label.Name;

        /// <summary>
        /// The properties of this entity
        /// </summary>
        PropertyCollection IEntity.Properties => _properties;

        /// <summary>
        /// The base type of this entity
        /// </summary>
        IEntity? IEntity.Inherits => Inherits;

        /// <summary>
        /// Add a property to the full text index of this entity
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        IEntity IEntity.SetFullTextProperty(string propertyName) => SetFullTextProperty(propertyName);

        /// <summary>
        /// Remove a property from the full text index of this entity
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        IEntity IEntity.RemoveFullTextProperty(string propertyName) => RemoveFullTextProperty(propertyName);

        /// <summary>
        /// The properties of this entity that are part of the full text index
        /// </summary>
        /// <returns>The list of properties</returns>
        IReadOnlyList<Property> IEntity.FullTextIndexProperties => FullTextIndexProperties;

        /// <summary>
        /// The base types of this entity
        /// </summary>
        /// <returns>The list of base types</returns>
        IReadOnlyList<IEntity> IEntity.GetBaseTypes() => GetBaseTypes();

        /// <summary>
        /// The base types and this entity
        /// </summary>
        /// <returns>The list of base types</returns>
        IReadOnlyList<IEntity> IEntity.GetBaseTypesAndSelf() => GetBaseTypesAndSelf();

        /// <summary>
        /// The subclasses and this entity
        /// </summary>
        /// <returns>The list of subclasses</returns>
        IReadOnlyList<IEntity> IEntity.GetSubclassesOrSelf() => GetSubclassesOrSelf();

        /// <summary>
        /// The subclasses of this entity
        /// </summary>
        /// <returns>The list of subclasses</returns>
        IReadOnlyList<IEntity> IEntity.GetSubclasses() => GetSubclasses();

        /// <summary>
        /// The direct subclasses of this entity
        /// </summary>
        /// <returns>The list of direct subclasses</returns>
        IReadOnlyList<IEntity> IEntity.GetDirectSubclasses() => GetDirectSubclasses();

        /// <summary>
        /// The nearest non-virtual subclass of this entity
        /// </summary>
        /// <returns>The nearest non-virtual subclass</returns>
        IReadOnlyList<IEntity> IEntity.GetNearestNonVirtualSubclass() => GetNearestNonVirtualSubclass();

        /// <summary>
        /// The properties of this entity and its base types
        /// </summary>
        /// <returns>The list of properties</returns>
        IReadOnlyList<Property> IEntity.GetPropertiesOfBaseTypesAndSelf() => GetPropertiesOfBaseTypesAndSelf();

        /// <summary>
        /// The concrete classes of this entity
        /// </summary>
        /// <returns>The list of concrete classes</returns>
        IReadOnlyList<IEntity> IEntity.GetConcreteClasses() => GetConcreteClasses();

        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyAdded(Property property) => DynamicEntityPropertyAdded(property);
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm) => DynamicEntityPropertyRenamed(oldname, property, mergeAlgorithm);
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyConverted(Property property, Type target) => DynamicEntityPropertyConverted(property, target);
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRerouted(string oldname, Entity to, Property property) => DynamicEntityPropertyRerouted(oldname, to, property);
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRemoved(Property property) => DynamicEntityPropertyRemoved(property);
    }
    partial class Relationship : IEntity
    {
        /// <summary>
        /// The relationship-type of this relationship
        /// </summary>
        string IEntity.Neo4jName => Neo4JRelationshipType;

        /// <summary>
        /// The properties of this relationship
        /// </summary>
        PropertyCollection IEntity.Properties => _properties;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IEntity? IEntity.Inherits => null;

        IEntity IEntity.SetFullTextProperty(string propertyName) => SetFullTextProperty(propertyName);
        IEntity IEntity.RemoveFullTextProperty(string propertyName) => RemoveFullTextProperty(propertyName);
        IReadOnlyList<Property> IEntity.FullTextIndexProperties => FullTextIndexProperties;

        #region Simplistic implementations for Relationship

        // Since Relationship doesn't support inheritance, it's either is or isn't itself

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        bool IEntity.IsAbstract => false;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        bool IEntity.IsVirtual => false;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetBaseTypes() => _empty;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetBaseTypesAndSelf() => _self;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetSubclassesOrSelf() => _self;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetSubclasses() => _empty;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetDirectSubclasses() => _empty;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetNearestNonVirtualSubclass() => _empty;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<Property> IEntity.GetPropertiesOfBaseTypesAndSelf() => _properties.ToArray();

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        IReadOnlyList<IEntity> IEntity.GetConcreteClasses() => _self;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        bool IEntity.IsSubsclassOf(IEntity baseType) => false;

        /// <summary>
        /// Unused since Relationships don't support inheritance
        /// </summary>
        bool IEntity.IsSelfOrSubclassOf(IEntity baseType) => (baseType == this);

        private static readonly IReadOnlyList<IEntity> _empty = new IEntity[0];
        private readonly IReadOnlyList<IEntity> _self;

        #endregion

        #region Irrelevant implementations for Relationship

        // Since there is no OGM object for relationships, there is also no dynamic-OGM object that needs to get updated with Property changes.
        // Especially since these updates have to do with static data, and for now we will not support static data on relationships.

        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyAdded(Property property) { }
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRenamed(string oldname, Property property, MergeAlgorithm mergeAlgorithm) { }
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyConverted(Property property, Type target) { }
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRerouted(string oldname, Entity to, Property property) { }
        /// <summary>
        /// For internal use
        /// </summary>
        void IEntity.DynamicEntityPropertyRemoved(Property property) { }

        #endregion
    }
}
