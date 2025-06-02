using System;
using System.Collections.Generic;
using System.Text;

using Blueprint41.Core;

namespace Blueprint41.DatastoreTemplates
{
    public class GeneratorResult
    {
        public GeneratorResult()
        {
            EntityResult       = new GeneratorResultItems();
            RelationshipResult = new GeneratorResultItems();
            NodeResult         = new GeneratorResultItems();
        }
        
        public GeneratorResultItems EntityResult { get; set; }
        public GeneratorResultItems RelationshipResult { get; set; }
        public GeneratorResultItems NodeResult { get; set; }
    }

    public class GeneratorResultItems
    {
        internal GeneratorResultItems()
        {
            _blocking = new Dictionary<string, string>();
            _async    = new Dictionary<string, string>();
            _any      = new Dictionary<string, string>();
        }

        public void Add(EntityGenerationFlavor? flavor, string key, string value)
        {
            switch (flavor ?? EntityGenerationFlavor.Both)
            {
                case EntityGenerationFlavor.Blocking:
                    _blocking.Add(key, value);
                    break;
                case EntityGenerationFlavor.Async:
                    _async.Add(key, value);
                    break;
                case EntityGenerationFlavor.Both:
                    _any.Add(key, value);
                    break;
                default:
                    throw new NotSupportedException($"Adding generated code for flavor '{flavor}' is not supported.");
            }
        }
        public bool ContainsFile(EntityGenerationFlavor? flavor, string key)
        {
            switch (flavor ?? EntityGenerationFlavor.Both)
            {
                case EntityGenerationFlavor.Blocking:
                    return _blocking.ContainsKey(key);
                case EntityGenerationFlavor.Async:
                    return _async.ContainsKey(key);
                case EntityGenerationFlavor.Both:
                    return _any.ContainsKey(key);
                default:
                    throw new NotSupportedException($"Adding generated code for flavor '{flavor}' is not supported.");
            }
        }

        public IReadOnlyDictionary<string, string> Items(EntityGenerationFlavor? flavor)
        {
            switch (flavor ?? EntityGenerationFlavor.Both)
            {
                case EntityGenerationFlavor.Blocking:
                    return _blocking;
                case EntityGenerationFlavor.Async:
                    return _async;
                case EntityGenerationFlavor.Both:
                    return _any;
                default:
                    throw new NotSupportedException($"Enumerating generated code for flavor '{flavor}' is not supported.");
            }
        }

        private readonly Dictionary<string, string> _blocking;
        private readonly Dictionary<string, string> _async;
        private readonly Dictionary<string, string> _any;
    }
    public class GeneratorResult<T> : GeneratorResult
        where T : DatastoreModel<T>, new()
    {
        public GeneratorResult(T model)
        {
            Model = model;
        }

        public T Model { get; set; }
    }
}
