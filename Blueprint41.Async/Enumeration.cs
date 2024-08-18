using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Blueprint41.Async.Neo4j.Refactoring;

namespace Blueprint41.Async
{
    public class Enumeration : IRefactorEnumeration
    {
        internal Enumeration(DatastoreModel parent, string name)
        {
            Parent = parent;
            Name = name;
            Guid = parent?.GenerateGuid(name) ?? Guid.Empty;
            PropertyReference = null!;
        }
        internal Enumeration(Property property, IEnumerable<string> names)
        {
            Parent = null!;
            Name = "Ad-hoc";
            Guid = Guid.Empty;
            PropertyReference = property;
            AddValuesInternal(names.ToArray());
        }

        public Property PropertyReference { get; internal set; }
        public DatastoreModel Parent { get; private set; }
        public string Name { get; private set; }
        public Guid Guid { get; private set; }

        public IReadOnlyList<EnumerationValue> Values { get { return values; } }
        private List<EnumerationValue> values = new List<EnumerationValue>();

        public Enumeration AddValue(string name, int? value = null)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            if (!value.HasValue)
                value = values.Select(item => item.Value).DefaultIfEmpty(-1).Max(item => item) + 1;

            if (values.Any(item => item.Name == name || item.Value == value))
                return this;

            values.Add(new EnumerationValue(name, value.Value));

            return this;
        }
        public Enumeration AddValues(params string[] names)
        {
            //if (Parent is null)
            //    throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            AddValuesInternal(names);

            return this;
        }
        internal void AddValuesInternal(params string[] names)
        {
            int index = values.Select(item => item.Value).DefaultIfEmpty(-1).Max(item => item) + 1;

            foreach (string name in names)
            {
                values.Add(new EnumerationValue(name, index));
                index++;
            }
        }

        public IRefactorEnumeration Refactor { get { return this; } }

        void IRefactorEnumeration.RemoveValue(string name)
        {
            //if (Parent is null)
            //    throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            values.RemoveAll(item => item.Name == name);
        }
        void IRefactorEnumeration.RemoveValues(params string[] names)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            HashSet<string> hasedNames = new HashSet<string>(names);
            values.RemoveAll(item => hasedNames.Contains(item.Name));
        }
        void IRefactorEnumeration.Rename(string newName)
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            Parent.Enumerations.Remove(Name);
            Name = newName;
            Parent.Enumerations.Add(Name, this);
        }
        void IRefactorEnumeration.Deprecate()
        {
            if (Parent is null)
                throw new InvalidOperationException("You cannot change an 'ad-hoc' enumeration.");

            foreach (Entity entity in Parent.Entities)
            {
                foreach (Property property in entity.Properties)
                {
                    if (property.Enumeration is null || property.Enumeration != this)
                        continue;

                    property.ConvertToAdhocEnum();
                }
            }

            Parent.Enumerations.Remove(Name);
        }
    }
    public class EnumerationValue
    {
        internal EnumerationValue(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public int Value { get; private set; }
    }
}
