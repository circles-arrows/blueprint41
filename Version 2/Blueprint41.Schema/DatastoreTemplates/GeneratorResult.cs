using System;
using System.Collections.Generic;
using System.Text;

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

        public void Add(EntityFlavor? flavor, string key, string value)
        {
            switch (flavor ?? EntityFlavor.Both)
            {
                case EntityFlavor.Blocking:
                    _blocking.Add(key, value);
                    break;
                case EntityFlavor.Async:
                    _async.Add(key, value);
                    break;
                case EntityFlavor.Both:
                    _any.Add(key, value);
                    break;
                default:
                    throw new NotSupportedException($"Adding generated code for flavor '{flavor}' is not supported.");
            }
        }
        public bool ContainsFile(EntityFlavor? flavor, string key)
        {
            switch (flavor ?? EntityFlavor.Both)
            {
                case EntityFlavor.Blocking:
                    return _blocking.ContainsKey(key);
                case EntityFlavor.Async:
                    return _async.ContainsKey(key);
                case EntityFlavor.Both:
                    return _any.ContainsKey(key);
                default:
                    throw new NotSupportedException($"Adding generated code for flavor '{flavor}' is not supported.");
            }
        }

        public IReadOnlyDictionary<string, string> Items(EntityFlavor? flavor)
        {
            switch (flavor ?? EntityFlavor.Both)
            {
                case EntityFlavor.Blocking:
                    return _blocking;
                case EntityFlavor.Async:
                    return _async;
                case EntityFlavor.Both:
                    return _any;
                default:
                    throw new NotSupportedException($"Enumerating generated code for flavor '{flavor}' is not supported.");
            }
        }

        private readonly Dictionary<string, string> _blocking;
        private readonly Dictionary<string, string> _async;
        private readonly Dictionary<string, string> _any;
    }
}
